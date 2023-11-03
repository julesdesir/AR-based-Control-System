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

    private float inp1;
    private float inp2;
    private float inp3;
    private float inp4;
    
    private float out1;
    private float out2;
    private float out3;
    private float out4;
    private float out5;

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
        float RPM = BitConverter.ToSingle(receivedBytes, 0);
        float WOB = BitConverter.ToSingle(receivedBytes, sizeof(float));
        float Zc = BitConverter.ToSingle(receivedBytes, 2 * sizeof(float)); 
        float Qp = BitConverter.ToSingle(receivedBytes, 3 * sizeof(float));
        float bitRPM = BitConverter.ToSingle(receivedBytes, 4 * sizeof(float));
        float resFlow = BitConverter.ToSingle(receivedBytes, 5 * sizeof(float));
        float BHCP = BitConverter.ToSingle(receivedBytes, 6 * sizeof(float));
        float pressPump = BitConverter.ToSingle(receivedBytes, 7 * sizeof(float));
        float pressChoke = BitConverter.ToSingle(receivedBytes, 8 * sizeof(float));
        
        List<float> input = new List<float> {RPM, WOB, Zc, Qp};
        List<float> output = new List<float> {bitRPM, resFlow, BHCP, pressPump, pressChoke};
        
        // Display each parameter in the Unity console
        foreach(var p in input)
            { Debug.Log("Receive from server: " + p.ToString()); }
        
        
        //Debug.Log("resFlow: " + resFlow.ToString());
        
        //Set inputs and outputs to be able to update them in void Update()
        
        inp1 = RPM;
        inp2 = WOB;
        inp3 = Zc;
        inp4 = Qp;
        
        out1 = bitRPM;
        out2 = resFlow;
        out3 = BHCP;
        out4 = pressPump;
        out5 = pressChoke;
        
        
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
