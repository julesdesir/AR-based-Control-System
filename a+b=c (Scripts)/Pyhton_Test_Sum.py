import socket
import struct
import random

print(random.randint(1,27))
# Server configuration
server_host = '0.0.0.0'  # Listen on all available interfaces
server_port = 12345  # Choose a port number

# Create a socket object
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to a specific address and port
server_socket.bind((server_host, server_port))

# Listen for incoming connections
server_socket.listen(1)
print(f"Server listening on {server_host}:{server_port}")

# Accept incoming connection
client_socket, client_address = server_socket.accept()
print(f"Connection established with {client_address}")

# Send a float number to the client
a = random.randint(0, 100)
b = random.randint(0, 100)
c = a+b
nums = [a,b,c]
print(nums)

for number in nums:
    # Convert the float to bytes using struct.pack
    number_bytes = struct.pack('f', number)
    client_socket.sendall(number_bytes)


# Close the sockets
client_socket.close()
server_socket.close()
