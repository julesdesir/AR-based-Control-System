function xdot = drillstring(t,x,u,ff, P )

% global ud uss xss js jb ks kb ds db 
% 
% xss = uss;
% %Scaling
% u = diag(uss)*u;
% x = diag(xss).*x;

% xdot = f(x,u) 

ks = P.ks;
kb = P.kb;
js = P.js;
jb = P.jb;
db = P.db;
ds = P.ds;

x1=x(1);
x2=x(2);
x3=x(3);
x4=x(4);
x5=x(5);
x6=x(6);
x7=x(7);
x8=x(8);
x9=x(9);
x10=x(10);
x11=x(11);
x12=x(12);
x13=x(13);
x14=x(14);
x15=x(15);
x16=x(16);
x17=x(17);
x18=x(18);
x19=x(19);
x20=x(20);
x21=x(21);
x22=x(22);
x23=x(23);
x24=x(24);
x25=x(25);
x26=x(26);
x27=x(27);
x28=x(28);
x29=x(29);
x30=x(30);
x31=x(31);
x32=x(32);
x33=x(33);
x34=x(34);
x35=x(35);
x36=x(36);
x37=x(37);
x38=x(38);
x39=x(39);
x40=x(40);
x41=x(41);
x42=x(42);
x43=x(43);
x44=x(44);
x45=x(45);
x46=x(46);
x47=x(47);
x48=x(48);
x49=x(49);
x50=x(50);
x51=x(51);
x52=x(52);
x53=x(53);
x54=x(54);
x55=x(55);
x56=x(56);
x57=x(57);
x58=x(58);
x59=x(59);
x60=x(60);
x61=x(61);
x62=x(62);
x63=x(63);
x64=x(64);
x65=x(65);
x66=x(66);
x67=x(67);
x68=x(68);
x69=x(69);
x70=x(70);
x71=x(71);
x72=x(72);
x73=x(73);
x74=x(74);
x75=x(75);
x76=x(76);
x77=x(77);
x78=x(78);
x79=x(79);
x80=x(80);
x81=x(81);
x82=x(82);
x83=x(83);
x84=x(84);
x85=x(85);
x86=x(86);


x1dot=(1/js)*(ds*ks*(u-x1)+ks*x44-ds*ks*(x1-x2)-ks*x45);
x2dot=(1/js)*(ds*ks*(x1-x2)+ks*x45-ds*ks*(x2-x3)-ks*x46);
x3dot=(1/js)*(ds*ks*(x2-x3)+ks*x46-ds*ks*(x3-x4)-ks*x47);
x4dot=(1/js)*(ds*ks*(x3-x4)+ks*x47-ds*ks*(x4-x5)-ks*x48);
x5dot=(1/js)*(ds*ks*(x4-x5)+ks*x48-ds*ks*(x5-x6)-ks*x49);
x6dot=(1/js)*(ds*ks*(x5-x6)+ks*x49-ds*ks*(x6-x7)-ks*x50);
x7dot=(1/js)*(ds*ks*(x6-x7)+ks*x50-ds*ks*(x7-x8)-ks*x51);
x8dot=(1/js)*(ds*ks*(x7-x8)+ks*x51-ds*ks*(x8-x9)-ks*x52);
x9dot=(1/js)*(ds*ks*(x8-x9)+ks*x52-ds*ks*(x9-x10)-ks*x53);
x10dot=(1/js)*(ds*ks*(x9-x10)+ks*x53-ds*ks*(x10-x11)-ks*x54);
x11dot=(1/js)*(ds*ks*(x10-x11)+ks*x54-ds*ks*(x11-x12)-ks*x55);
x12dot=(1/js)*(ds*ks*(x11-x12)+ks*x55-ds*ks*(x12-x13)-ks*x56);
x13dot=(1/js)*(ds*ks*(x12-x13)+ks*x56-ds*ks*(x13-x14)-ks*x57);
x14dot=(1/js)*(ds*ks*(x13-x14)+ks*x57-ds*ks*(x14-x15)-ks*x58);
x15dot=(1/js)*(ds*ks*(x14-x15)+ks*x58-ds*ks*(x15-x16)-ks*x59);
x16dot=(1/js)*(ds*ks*(x15-x16)+ks*x59-ds*ks*(x16-x17)-ks*x60);
x17dot=(1/js)*(ds*ks*(x16-x17)+ks*x60-ds*ks*(x17-x18)-ks*x61);
x18dot=(1/js)*(ds*ks*(x17-x18)+ks*x61-ds*ks*(x18-x19)-ks*x62);
x19dot=(1/js)*(ds*ks*(x18-x19)+ks*x62-ds*ks*(x19-x20)-ks*x63);
x20dot=(1/js)*(ds*ks*(x19-x20)+ks*x63-ds*ks*(x20-x21)-ks*x64);
x21dot=(1/js)*(ds*ks*(x20-x21)+ks*x64-ds*ks*(x21-x22)-ks*x65);
x22dot=(1/js)*(ds*ks*(x21-x22)+ks*x65-ds*ks*(x22-x23)-ks*x66);
x23dot=(1/js)*(ds*ks*(x22-x23)+ks*x66-ds*ks*(x23-x24)-ks*x67);
x24dot=(1/js)*(ds*ks*(x23-x24)+ks*x67-ds*ks*(x24-x25)-ks*x68);
x25dot=(1/js)*(ds*ks*(x24-x25)+ks*x68-ds*ks*(x25-x26)-ks*x69);
x26dot=(1/js)*(ds*ks*(x25-x26)+ks*x69-ds*ks*(x26-x27)-ks*x70);
x27dot=(1/js)*(ds*ks*(x26-x27)+ks*x70-ds*ks*(x27-x28)-ks*x71);
x28dot=(1/js)*(ds*ks*(x27-x28)+ks*x71-ds*ks*(x28-x29)-ks*x72);
x29dot=(1/js)*(ds*ks*(x28-x29)+ks*x72-ds*ks*(x29-x30)-ks*x73);
x30dot=(1/js)*(ds*ks*(x29-x30)+ks*x73-ds*ks*(x30-x31)-ks*x74);
x31dot=(1/js)*(ds*ks*(x30-x31)+ks*x74-ds*ks*(x31-x32)-ks*x75);
x32dot=(1/js)*(ds*ks*(x31-x32)+ks*x75-ds*ks*(x32-x33)-ks*x76);
x33dot=(1/js)*(ds*ks*(x32-x33)+ks*x76-ds*ks*(x33-x34)-ks*x77);
x34dot=(1/js)*(ds*ks*(x33-x34)+ks*x77-ds*ks*(x34-x35)-ks*x78);
x35dot=(1/js)*(ds*ks*(x34-x35)+ks*x78-db*kb*(x35-x36)-kb*x79);
x36dot=(1/jb)*(db*kb*(x35-x36)+kb*x79-db*kb*(x36-x37)-kb*x80);
x37dot=(1/jb)*(db*kb*(x36-x37)+kb*x80-db*kb*(x37-x38)-kb*x81);
x38dot=(1/jb)*(db*kb*(x37-x38)+kb*x81-db*kb*(x38-x39)-kb*x82);
x39dot=(1/jb)*(db*kb*(x38-x39)+kb*x82-db*kb*(x39-x40)-kb*x83);
x40dot=(1/jb)*(db*kb*(x39-x40)+kb*x83-db*kb*(x40-x41)-kb*x84);
x41dot=(1/jb)*(db*kb*(x40-x41)+kb*x84-db*kb*(x41-x42)-kb*x85);
x42dot=(1/jb)*(db*kb*(x41-x42)+kb*x85-db*kb*(x42-x43)-kb*x86);
x43dot=(1/jb)*(db*kb*(x42-x43)+kb*x86)-(1/jb)*ff*500*(x43/sqrt(x43^2+0.2^2)...
    +(1.5*0.2*x43)/(x43^2+0.2^2))-(0.28*x43/jb)*(x43/31.4-1);
%1764
%3035
x44dot=u-x1;
x45dot=x1-x2;
x46dot=x2-x3;
x47dot=x3-x4;
x48dot=x4-x5;
x49dot=x5-x6;
x50dot=x6-x7;
x51dot=x7-x8;
x52dot=x8-x9;
x53dot=x9-x10;
x54dot=x10-x11;
x55dot=x11-x12;
x56dot=x12-x13;
x57dot=x13-x14;
x58dot=x14-x15;
x59dot=x15-x16;
x60dot=x16-x17;
x61dot=x17-x18;
x62dot=x18-x19;
x63dot=x19-x20;
x64dot=x20-x21;
x65dot=x21-x22;
x66dot=x22-x23;
x67dot=x23-x24;
x68dot=x24-x25;
x69dot=x25-x26;
x70dot=x26-x27;
x71dot=x27-x28;
x72dot=x28-x29;
x73dot=x29-x30;
x74dot=x30-x31;
x75dot=x31-x32;
x76dot=x32-x33;
x77dot=x33-x34;
x78dot=x34-x35;
x79dot=x35-x36;
x80dot=x36-x37;
x81dot=x37-x38;
x82dot=x38-x39;
x83dot=x39-x40;
x84dot=x40-x41;
x85dot=x41-x42;
x86dot=x42-x43;



xdot = [x1dot x2dot x3dot x4dot x5dot x6dot x7dot x8dot x9dot x10dot...
    x11dot x12dot x13dot x14dot x15dot x16dot x17dot x18dot x19dot x20dot...
    x21dot x22dot x23dot x24dot x25dot x26dot x27dot x28dot x29dot x30dot...
    x31dot x32dot x33dot x34dot x35dot x36dot x37dot x38dot x39dot x40dot...
    x41dot x42dot x43dot x44dot x45dot x46dot x47dot x48dot x49dot x50dot...
    x51dot x52dot x53dot x54dot x55dot x56dot x57dot x58dot x59dot x60dot...
    x61dot x62dot x63dot x64dot x65dot x66dot x67dot x68dot x69dot x70dot...
    x71dot x72dot x73dot x74dot x75dot x76dot x77dot x78dot x79dot x80dot...
    x81dot x82dot x83dot x84dot x85dot x86dot]';
% xdot = diag(1./xss)*xdot;