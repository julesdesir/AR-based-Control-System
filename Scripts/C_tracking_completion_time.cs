using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingCompletionTimer : MonoBehaviour
{
    public Transform objectTracking; 
    public Transform objectTracked; 
    public Text RealTimeTextTimer; // Text where the completion time will be displayed
    public Text RealTimeTextTimerAveraged; // Text where the average completion time will be displayed

    private float completionTime; // Instant completion time
    private float completionTimeAveraged; // Average completion time
    private float completionTimeSum = 0; // Sum of all the completion times
    private int completionCount = 0; // Count of completion rate calculated so far
    private bool trackingCompleted = false; // Indicates whether or not the user tracked the new target position

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectTracking != null && objectTracked != null)
        {
            // Start a new tracking phase every time the target moves (every 5s)
            if (Time.time % 5 < 0.1)
            {
                trackingCompleted = false;
            }

            // Calculate the vertical distance between the 2 objects
            float distance = Vector3.Distance(new Vector3(0.0f, objectTracking.position.y, 0.0f),
                new Vector3(0.0f, objectTracked.position.y, 0.0f));

            // Calculate the completion time
            if (!trackingCompleted && distance < 0.01)
            {
                // Actualise the counter of completions
                completionCount++;

                // Actualise the completion time
                completionTime = Time.time % 5;
                trackingCompleted = true;

                // Actualise the text to display the real time completion rate
                RealTimeTextTimer.text = "Last completion time: " + string.Format("{0:F2}", completionTime) + "s";

                // Actualise the averaged completion time and display it in real time
                completionTimeSum = completionTimeSum + completionTime;
                completionTimeAveraged = completionTimeSum / completionCount;
                RealTimeTextTimerAveraged.text = "Averaged completion time: " + string.Format("{0:F2}", completionTimeAveraged) + "s";
            }
        }
    }
}
