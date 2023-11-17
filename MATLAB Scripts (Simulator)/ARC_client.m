clear
%% Create the client 
SERVER_PORT = 25001;
% SERVER_PORT = 63532;
client = udpport("datagram");

%% Send Data and wait for a response
i=1;
ROP = 10;
RPM = 250;
WOB = 2.5;
Zc = 0.75;
Qp = 0.025;

while true

x = [ROP, RPM, WOB, Zc, Qp];

% send data to server
client.write(x,'single','127.0.0.1',25001);
% disp(['Client sent: ', num2str(x)])

% Recieve data from server
y = read(client,1,"single");

% Display values

bitRPM = y.Data(1);
resFlow = y.Data(2);
BHCP = y.Data(3);
pressPump = y.Data(4);
pressChoke = y.Data(5);

if y.Data(10) > 0
    RPM = y.Data(10);
    WOB = y.Data(11);
    % Zc = y.Data(12);
    Qp = y.Data(13);
end


% disp(['Client received: ', num2str(y.Data)])

i = i+1;
if i==7200
    break
end
end
clear client