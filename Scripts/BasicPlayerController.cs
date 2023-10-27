
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;
using System.Text;

public class BasicPlayerController : MonoBehaviour
{
    /*
    UdpClient client;
    IPEndPoint serverEndpoint;
    public string serverIp = "10.21.22.185";
    public int port = 25001;
    */

    /* 
    Create a variable called 'rb' that will represent the 
    rigid body of this object.
    */
    private Rigidbody rb;

    // Create a public variable for the cameraTarget object
    public GameObject cameraTarget;
    /* 
    You will need to set the cameraTarget object in Unity. 
    The direction this object is facing will be used to determine
    the direction of forces we will apply to our player.
    */
    public float movementIntensity;
    /* 
    Creates a public variable that will be used to set 
    the movement intensity (from within Unity)
    */

    void Start()
    {
        // make our rb variable equal the rigid body component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /* 
    	Establish some directions 
    	based on the cameraTarget object's orientation 
    	*/
        var ForwardDirection = cameraTarget.transform.forward;
        var RightDirection = cameraTarget.transform.right;
        var UpwardDirection = cameraTarget.transform.up;

        //Debug.Log(Input.GetAxis("XboxV"));
        // Move Forwards

        rb.velocity = ForwardDirection * movementIntensity * Input.GetAxis("XboxV") + RightDirection * movementIntensity * Input.GetAxis("XboxH");
        
        if (Input.GetKey(KeyCode.Space)) 
        {
            rb.velocity = UpwardDirection * movementIntensity;
        }
    }
}
