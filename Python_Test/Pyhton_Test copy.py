
import socket

localIP = "127.0.0.1"
localPort = 25001
bufferSize = 1024

msgFromServer = "Hello UDP Client"
bytesToSend = str.encode(msgFromServer)

# Create a datagram socket
UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)

# Bind to address and ip
UDPServerSocket.bind((localIP, localPort))


print("UDP server up and listening")


# Listen for incoming datagrams
stay = True
bytesAddressPair = UDPServerSocket.recvfrom(bufferSize)
clientIP = bytesAddressPair[1]

while (stay):

    a = int(input("a = "))
    b = int(input("b = "))
    c = a+b
    data = str(a)+","+str(b)+","+str(c)
    print(data)

    # Connect to the server and send the data
    UDPServerSocket.sendto(data.encode(), clientIP)

    exit = input("exit ? y/n ")
    if exit == "y":
        stay = False
