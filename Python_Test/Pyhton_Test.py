import socket

host, port = "127.0.0.1", 25001
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

stay = True


try:
    sock.connect((host, port))
    while (stay):
        a = int(input())
        b = int(input())
        c = a+b
        data = str(a)+","+str(b)+","+str(c)
        print(data)

        # Connect to the server and send the data
        sock.sendall(data.encode("utf-8"))
        response = sock.recv(1024).decode("utf-8")
        print(response)

        exit = input("exit ? y/n ")
        if exit == "y":
            stay = False

finally:
    sock.close()
