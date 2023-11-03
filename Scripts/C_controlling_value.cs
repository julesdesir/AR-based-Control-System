using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectController : MonoBehaviour
{
    public float moveSpeed = 0.1f; // Adjust the speed as needed
    private Rigidbody ControlledObject;

    private void Start()
    {
        ControlledObject = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0.0f, verticalInput, 0.0f);
        ControlledObject.AddForce(movement * moveSpeed);
    }
}