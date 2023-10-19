using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class NumberReceiver : MonoBehaviour
{
    public string serverHost = "127.0.0.1";  // Server IP address
    public int serverPort = 12345;  // Server port number

    public TextMesh textMesh1;
    public TextMesh textMesh2;
    public TextMesh textMesh3;

    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        try
        {
            // Connect to the server
            client = new TcpClient(serverHost, serverPort);
            stream = client.GetStream();
        }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e.Message);
        }
        //(BELOW) This can go on update for continous flux of data
        try
        {
            // Read the float numbers from the server
            for (int i = 0; i < 3; i++)
            {
                byte[] data = new byte[4];
                int bytesRead = stream.Read(data, 0, data.Length);

                // Convert the received data to a float and display it
                float receivedNumber = BitConverter.ToSingle(data, 0);

                // Update TextMesh components based on the index (i)
                if (i == 0)
                    textMesh1.text = receivedNumber.ToString();
                else if (i == 1)
                    textMesh2.text = receivedNumber.ToString();
                else if (i == 2)
                    textMesh3.text = receivedNumber.ToString();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e.Message);
        }
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        // Close the stream and client when the script is destroyed or the scene changes
        if (client != null)
        {
            client.Close();
        }
    }
}
