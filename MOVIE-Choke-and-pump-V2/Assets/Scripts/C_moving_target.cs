using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;



public class TargetMover : MonoBehaviour
{
    public Transform MovedObject;
    private float newPosition;
    float lastChangeTime = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        // Create a new instance of the Random class
        System.Random random = new System.Random();

        // Change randomly the position of the target every 5 seconds
        if (Time.time - lastChangeTime > 1.0 && (int)(Time.time % 5) == 0)
        {
            // Generate a random integer between -0.5 and 0.5 (inclusive)
            // random.NextDouble(): generate a random float between 0 and 1 (inclusive)
            float randomFloat = (float)(random.NextDouble() - 0.5);

            MovedObject.position = new Vector3(0.75f, randomFloat, 2.5f);

            lastChangeTime = Time.time;
        }
    }
}