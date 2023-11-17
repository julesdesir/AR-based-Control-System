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
    private bool started = false;
    float timeStart;

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
                if ((!trackingCompleted) && started)
                {
                    // Actualise the text to display the real time completion rate
                    RealTimeTextTimer.text = "Last completion time: " + string.Format("{0:F2}", completionTime) + "s";

                    // Actualise the averaged completion time and display it in real time
                    completionTimeSum = completionTimeSum + 5;
                    completionTimeAveraged = completionTimeSum / completionCount;
                    RealTimeTextTimerAveraged.text = "Averaged completion time: " + string.Format("{0:F2}", completionTimeAveraged) + "s";
                }
                started = true;
                trackingCompleted = false;
                timeStart = Time.time;

                // Actualise the counter of completions
                completionCount++;
            }

            // Calculate the vertical distance between the 2 objects
            float distance = Math.Abs(objectTracking.position.y-objectTracked.position.y);

            // Calculate the completion time
            if (!trackingCompleted)
            {
                if (distance < 0.01)
                {
                    // Actualise the completion time
                    completionTime = Time.time - timeStart;
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
}
