clear 
clc

%% Scenarios
% A - left slow
% B - left fast
% C - right slow
% D - right fast
% Scenario 1: only A 
% Scenario 2: only D
% Scenario 3: A and D
% Scenario 4: A,B,C,D

SCENARIO = 1; % Only change the number and run the server again.
targetUpdateTime = 5; % in seconds
fps = 60; % Frames per second, i.e, 60 data points per s

rpm_min = 60;
rpm_max = 300;
wob_min = 2; % tonf
wob_max = 25;
pump_min = 500; %litres/min
pump_max = 3000;

%% Initialisation for mechanical model
P.ks = 35950;
P.kb = 204020;
P.js = 3.2021;
P.jb = 15.8480;
P.ds = 0.0271;
P.db = 0.0266;
P.D = 0.28;
P.p = 1.5;
P.omg0 = 0.2;
P.omg1 = 31.4;

g = 9.8066;                   % acceleration of gravity (m/s2)
tonf = 1000*g;                % ton force (N)       
rad2rpm = 30/pi;
rpm2rad = pi/30;
in2m = 0.0254;

ssRPM = 150;
u_ss = ssRPM*rpm2rad;  % rad/s, steady state value
x_ss = zeros(86,1).*u_ss;
x_n = x_ss;

%% Initialisation for hydraulics model
% Steady state with Kaasa model + QSS reservoir 

dia_dp_in = 4.5 *0.0254;
dia_dp_out = 5.5 *0.0254;
dia_bit = 8.5 *0.0254;
area_nozzle = 1.1562 *0.0254^2;
dia_hyd = sqrt(dia_bit^2 - dia_dp_out^2);
area_ann = (pi*dia_hyd^2)/4;
area_dp = (pi*dia_dp_in^2)/4;
Cd = 0.8;

r_w = dia_bit/2;
r_e = 100;
perm = 5e-12;
mu_oil = 5e-3;
compress_oil = 1.45e-9;
compress_res = 8.7e-10;
bulk_oil = 1/compress_oil;
mu_mud = 40e-2;
MD = 2000;
rho0 = 780;
Pi = 280e5;
p0 = 1e5;

Kc = 2.85e-3;

muBit = 0.25; % to change for each res
t_compute = 0:0.1:1;


%% Create the server
SERVER_PORT = 25001;
server = udpport("datagram","LocalPort",SERVER_PORT);
server.Timeout = 100;

% Create the client
UNITY_PORT = 25002;
client = udpport("datagram");
% Hololens_ip = "172.16.16.48"; %When connected to clotilde unity
Hololens_ip = "172.16.16.144"; %WheN CONNECTED TO movie

% Receive data from the client 
depth = linspace(1000,2800,7200);
% 7200 frames (2 mins)
% 0.25 m/frameo k at 60 fps
% ROP = 15 m/s 

bitVelocity = NaN(1,7200);
reservoirFlow = NaN(1,7200);
press_bottomHole = NaN(1,7200);
press_pump = NaN(1,7200);
press_choke = NaN(1,7200);
userData = NaN(7200,13);


targetRPM = randi([rpm_min, rpm_max]);
targetWOB = randi([wob_min, wob_max]);
targetPump = randi([pump_min, pump_max]);
targetChoke = 0.1 + 0.9 * rand;

di = 240;
i = 1;
n = 1;

while true
    disp(' ')
    disp('Server waiting...')
    data = read(server,1,'single');
    disp('Server received... ')
    disp(data)

    % Read input from client
    ROP = data.Data(1);
    RPM = data.Data(2)*rpm2rad;
    WOB = data.Data(3)*tonf;
    Zc = data.Data(4);
    Qp = data.Data(5);

    % ip_RPM = data.Data

    % Hydraulics
    choke_position_ss = Zc;
    h_res = 20;
    q_pump_ss = Qp / 60000; % m3/s

    qd_ss = q_pump_ss;

    m = - 2*pi*perm*h_res / ( mu_oil*( log(r_e/r_w) - 0.5 ) );
    d1 = 32*mu_mud*MD /(area_dp*dia_dp_in^2);
    d2 = (rho0/(2*Cd^2*area_nozzle^2));
    d3 = rho0*g*MD - Pi;
    d4 = 32*mu_mud*MD /(area_ann*dia_hyd^2);

    a1 = qd_ss + m*( d1*qd_ss - d2*qd_ss^2 + d3 );
    a2 = Kc*choke_position_ss;
    a3 = 2/rho0;
    a4 = -a3*p0;
    a5 = a1/a2;

    b1 = m*d4;
    b2 = -d4*qd_ss - m*d4*( d1*qd_ss - d2*qd_ss^2 + d3 ) - d1*qd_ss - d2*qd_ss^2;

    m1 = m/a2;
    c1 = a3*( 1-b1 );
    c2 = a3*(b2 + a4);

    f = @(x)( (m1*x + a5)^2 - c1*x - c2 );

    pp = fzero(f,0);
    pc = (1-b1)*pp + b2;
    qres  = m*(pp + d1*qd_ss - d2*qd_ss^2 + d3 ) ;
    BHCP = (pp + d1*qd_ss - d2*qd_ss^2 + d3 + Pi )/1e5 ;

    % Mechanics
    [t, x_np1] = ode15s(@(t,x)drillstring(t,x,RPM,P), t_compute, x_n);
    x_n = x_np1(end,:);
    bitRPM = x_np1(end,43)*rad2rpm;

    if rem(i,n*targetUpdateTime*fps) == 0

        targetRPM = randi([rpm_min, rpm_max]);
        targetWOB = randi([wob_min, wob_max]);
        targetChoke = 0.1 + 0.9 * rand;
        targetPump = randi([pump_min, pump_max]);

        n = n+1;
    end

    %y = [data.Data(2),data.Data(3),data.Data(4),data.Data(5),bitRPM, qres, BHCP, pp, pc,targetRPM,targetWOB,targetChoke,targetPump];    
    %disp(y)

    switch SCENARIO
        case 1
            y = [data.Data(2),data.Data(3)*0,data.Data(4)*0,data.Data(5)*0,...
                bitRPM, qres, BHCP, pp, pc, ...
                targetRPM, targetWOB*0, targetChoke*0, targetPump*0];
        case 2
            y = [data.Data(2)*0,data.Data(3)*0,data.Data(4)*0,data.Data(5),...
                bitRPM, qres, BHCP, pp, pc,...
                targetRPM*0, targetWOB*0, targetChoke*0, targetPump];
        case 3
            y = [data.Data(2),data.Data(3)*0,data.Data(4)*0,data.Data(5),...
                bitRPM, qres, BHCP, pp, pc,...
                targetRPM, targetWOB*0, targetChoke*0, targetPump];
        case 4
            y = [data.Data(2),data.Data(3),data.Data(4),data.Data(5),...
                bitRPM, qres, BHCP, pp, pc,...
                targetRPM, targetWOB, targetChoke, targetPump];
    end

    userData(i,:) = y;


    bitVelocity(i) = bitRPM;
    reservoirFlow(i) = qres;
    press_bottomHole(i) = BHCP;
    press_pump(i) = pp/1e5;
    press_choke(i) = pc/1e5;

    % set(gca,'XAxisLocation','top','YAxisLocation','left','ydir','reverse');
    % hold on

    %y = [data.Data(2),data.Data(3),data.Data(4),data.Data(5),bitRPM, qres, BHCP, pp, pc];
    %disp(y);

    % Reply to Unity Server
    client.write(y,'single',Hololens_ip,UNITY_PORT); %%TODO check ip address
    disp('Server replied')


    i = i+1;
    if i == 3600 * 1
        break
    end

end

save("user_1_scenario_1.txt","userData","-ascii")


