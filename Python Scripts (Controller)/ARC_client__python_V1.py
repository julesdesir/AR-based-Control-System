from ast import Bytes
from http import server
import socket
import struct
import pygame
from Controller_test import Controller
import time


serverIP = "127.0.0.1" # MATLAB Server IP (MOVIE: 172.16.16.168)

port = 25001 # Port of Matlab Server


buffersize = 1024
serverAddressPort = (serverIP,port)

# Create a UDP socket at client side
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM) # MATLAB Server


# Initialize controller
controller = Controller()
print(f"Controller connected : {controller.joystick}")


# Initial Conditions
ROP = 15  #Fixed
RPM = 150 
WOB = 2.5 
Zc = 0.75 
Qp = 1000

# Min - Max values
RPMmin = 60
WOBmin = 2
Zcmin = 0.1
Qpmin = 500

RPMmax = 300
WOBmax = 25
Zcmax = 1
Qpmax = 3000

# Constants
A = 2 # RPM
B = 1 # WOB
C = 0.075 # Zc
D = 20 # Qp

waitT = 1/60
force = 0.1

# Joystick controls
while True:    
    LeftJoystickHorizontal = controller.get_all_axis()["Axe 0"]     
    LeftJoystickVertical = -controller.get_all_axis()["Axe 1"]
    RightJoystickHorizontal = controller.get_all_axis()["Axe 2"]     
    RightJoystickVertical = -controller.get_all_axis()["Axe 3"]

     # Horizontal axis of the left joystick: RPM
    if abs(LeftJoystickHorizontal) > force and abs(LeftJoystickVertical) < force:
        if RPM>RPMmax:
            RPM = RPMmax
        if RPM < 0:
            RPM = 0   
        else:            
            RPM += A * LeftJoystickHorizontal

    # Vertical axis of the left joystick: WOB
    if abs(LeftJoystickVertical) > force and abs(LeftJoystickHorizontal) < force:
        if WOB > WOBmax:
            WOB=WOBmax
        elif WOB < 0:
            WOB = 0
        else:
            WOB += B * LeftJoystickVertical
    
    # Vertical axis of the right joystick: Zc
    if abs(RightJoystickVertical) > force and abs(RightJoystickHorizontal) < force:
        if Zc > Zcmax:
            Zc = Zcmax
        elif Zc < Zcmin:
            Zc = Zcmin
        else:
            Zc += C * RightJoystickVertical

    # Horizontal axis of the right joystick: Qp
    if abs(RightJoystickHorizontal) > force and abs(RightJoystickVertical) < force:
        if Qp > Qpmax:
            Qp = Qpmax
        elif Qp < Qpmin:
            Qp = Qpmin
        else:
            Qp += D * RightJoystickHorizontal
    
    data_to_send = [ROP, RPM, WOB, Zc, Qp]
    print(data_to_send)
    
    # Convert to bytes the data to send
    bytes_to_send = struct.pack('%sf'%len(data_to_send),*data_to_send)
    
    # Send to server(s) using created UDP socket(s)
    UDPClientSocket.sendto(bytes_to_send, serverAddressPort)

  
    # Time to wait between each send of information
    time.sleep(waitT)
