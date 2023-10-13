import socket
import struct

localIP = "0.0.0.0"
localPort = 25001
bufferSize = 1024

# Create a TCP socket
TCPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_STREAM)

# Bind to address and port
TCPServerSocket.bind((localIP, localPort))


def function(a, b):
    return a + b


on = True

print("TCP server up and listening")
TCPServerSocket.listen(1)  # Listen for incoming connections
clientSocket, clientAddress = TCPServerSocket.accept()

while on:

    data = clientSocket.recv(bufferSize)
    if not data:
        continue

    received_floats = struct.unpack('ff', data)
    a = received_floats[0]
    b = received_floats[1]
    c = function(a, b)

    floats_to_send = c  # Deux nombres flottants à envoyer
    message = struct.pack('f', *floats_to_send)
    clientSocket.send(message)

    exit = input("exit? y/n ")
    if exit == "y":
        on = False

TCPServerSocket.close()
