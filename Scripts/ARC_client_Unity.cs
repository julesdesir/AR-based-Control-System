using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class ARC_client_Unity : MonoBehaviour
{
    UdpClient udpServer;
    public int port = 25002;

    public Text bitRPMdisp;
    public Text resFlowdisp;
    public Text BHCPdisp;
    public Text pressPumpdisp;
    public Text pressChokedisp;

    private float par1;
    private float par2;
    private float par3;
    private float par4;
    private float par5;

    void Start()
    {
        udpServer = new UdpClient(port);
        udpServer.BeginReceive(new AsyncCallback(ReceiveData), null);
    }
    private void ReceiveData(IAsyncResult result)
    {
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] receivedBytes = udpServer.EndReceive(result, ref clientEndPoint);

        // Convert the received bytes to a float
        float bitRPM = BitConverter.ToSingle(receivedBytes, 0);
        float resFlow = BitConverter.ToSingle(receivedBytes, sizeof(float));
        float BHCP = BitConverter.ToSingle(receivedBytes, 2 * sizeof(float));
        float pressPump = BitConverter.ToSingle(receivedBytes, 3 * sizeof(float));
        float pressChoke = BitConverter.ToSingle(receivedBytes, 4 * sizeof(float));

        List<float> parameter = new List<float> {bitRPM, resFlow, BHCP, pressPump, pressChoke};

        // Display each parameter in the Unity console
        /*foreach(var p in parameter)
            { Debug.Log("Receive from server: " + p.ToString()); }
        */

        Debug.Log("resFlow: " + resFlow.ToString());

        //Set parameters to be able to update them in void Update()
        par1 = bitRPM;
        par2 = resFlow;
        par3 = BHCP;
        par4 = pressPump;
        par5 = pressChoke;


        // Continue listening for more data
        udpServer.BeginReceive(new AsyncCallback(ReceiveData), null);
    }
    void Update()
    { 
        // Display real time data
        bitRPMdisp.text = "RPM: " + par1.ToString();
        resFlowdisp.text = "resFlow: " + par2.ToString();
        BHCPdisp.text = "BHCP: " + par3.ToString();
        pressPumpdisp.text = "pressPump: " + par4.ToString();
        pressChokedisp.text = "pressChoke:" + par5.ToString();

        // Continue listening for more data
        udpServer.BeginReceive(new AsyncCallback(ReceiveData), null);
    }
    void OnApplicationQuit()
    {
        udpServer.Close();
    }
}