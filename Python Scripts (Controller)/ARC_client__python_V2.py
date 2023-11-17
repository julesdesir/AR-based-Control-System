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
ROP = 15  # Fixed
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

# Total time required by each parameter to move from min to max at full throttle, in seconds
dRPM = 5
dWOB = 8
dZc = 3
dQp = 10

waitT = 1/60

eps_rpm = waitT * (RPMmax - RPMmin) / dRPM
eps_wob = waitT * (WOBmax - WOBmin) / dWOB
eps_qp = waitT * (Qpmax - Qpmin) / dQp
eps_zc = waitT * (Zcmax - Zcmin) / dZc

# Joystick controls
while True:    
    Lx = controller.get_all_axis()["Axe 0"]
    Ly = controller.get_all_axis()["Axe 1"]
    Rx = controller.get_all_axis()["Axe 2"]
    Ry = controller.get_all_axis()["Axe 3"]
    
    # Determine which axis of the left joystick is used by the user
    # (We chose not to use the 2 axis simultaneously)
    if abs(Lx) <= abs(Ly):
        Ly = 0
    else:
        Lx = 0

    # Determine which axis of the right joystick is used by the user
    # (We chose not to use the 2 axis simultaneously)
    if abs(Rx) <= abs(Ry):
        Ry = 0
    else:
        Rx = 0
    
    # Control RPM value without exceeding the limits
    if Lx != 0 :
        RPM += eps_rpm * Lx
        if RPM >= RPMmax:
                RPM = RPMmax
        if RPM <= RPMmin:
                RPM = RPMmin

    # Control WOB value without exceeding the limits         
    if Ly != 0:
        WOB += eps_wob * Ly
        if WOB >= WOBmax:
                WOB = WOBmax
        if WOB <= WOBmin:
                WOB = WOBmin

    # Control Qp value without exceeding the limits            
    if Rx != 0:
        Qp += eps_qp * Rx
        if Qp >= Qpmax:
                Qp = Qpmax
        if Qp <= Qpmin:
                Qp = Qpmin
    
    # Control Zc value without exceeding the limits
    if Ry != 0 :
        Zc += eps_zc * Ry
        if Zc >= Zcmax:
                Zc = Zcmax
        if Zc <= Zcmin:
                Zc = Zcmin

    data_to_send = [ROP, RPM, WOB, Zc, Qp]
    
    # Convert to bytes de data to send
    bytes_to_send = struct.pack('%sf'%len(data_to_send),*data_to_send)
    
    # Send to server(s) using created UDP socket(s)
    UDPClientSocket.sendto(bytes_to_send, serverAddressPort)

    # Time to wait between each send of information
    time.sleep(waitT)
