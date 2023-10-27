import socket
import struct

#Server config
ServerIp = "0.0.0.0"
port = 25001

# Create a UDP socket
server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Bind the socket to a specific address and port
server_address = (ServerIp, port)
server_socket.bind(server_address)

print('UDP server is listening on {}:{}'.format(server_address[0], server_address[1]))

try:
    data, client_address = server_socket.recvfrom(1024)
    while True:
        # Get a float value from the user (for demonstration purposes)
        try:
            float_value = float(input('Enter x-coordinate (float): '))
        except ValueError:
            print('Invalid input. Please enter a valid float.')
            continue

        # Convert the float to bytes
        float_bytes = struct.pack('f', float_value)

        # Send the float bytes to all clients
        server_socket.sendto(float_bytes, client_address)

except KeyboardInterrupt:
    print('Server stopped.')
    server_socket.close()



   