using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    UdpClient client;
    IPEndPoint serverEndpoint;
    public string serverIp = "10.21.22.185";
    public int port = 25001;
    public GameObject Sphere;
    public float movInt;

    private float[] transformPosition = new float[3];


    void Start()
    {
        client = new UdpClient(serverIp, port); // Replace "localhost" with the server IP address and 25001 with the port number
        serverEndpoint = new IPEndPoint(IPAddress.Parse(serverIp), port);

        string message = "Connected"; // Example message
        byte[] data = Encoding.UTF8.GetBytes(message);
        client.Send(data, data.Length);
    }
    void ReceiveCallback(IAsyncResult result)
    {
        // Get the received data and sender's endpoint
        IPEndPoint senderEndpoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] receivedBytes = client.EndReceive(result, ref senderEndpoint);

        // Convert the received bytes to a float
        float receivedNum1 = BitConverter.ToSingle(receivedBytes, 0);
        //float receivedNum2 = BitConverter.ToSingle(receivedBytes, sizeof(float));
        //float receivedNum3 = BitConverter.ToSingle(receivedBytes, 2 * sizeof(float));

        // Process the received message (e.g., display it in the Unity console)
        Debug.Log("Received from server: " + receivedNum1);
        //Debug.Log("Received from server: " + receivedNum2);
        //Debug.Log("Received from server: " + receivedNum3);

        transformPosition[0] = receivedNum1;
        transformPosition[1] = receivedNum1;
        transformPosition[2] = receivedNum1;

        // Continue listening for more data
        client.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }
    void Update()
    {
        Sphere.transform.position = new Vector3(
            Sphere.transform.localScale.x + movInt * transformPosition[0], 
            Sphere.transform.localScale.y + movInt * transformPosition[1], 
            Sphere.transform.localScale.z + movInt * transformPosition[2]);

        // Continue listening for more data
        client.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }
    void OnDestroy()
    {
        client.Close();
    }
}

