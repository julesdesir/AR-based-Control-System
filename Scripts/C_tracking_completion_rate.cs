using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingCompletionRater : MonoBehaviour
{
    public Transform objectTracking; 
    public Transform objectTracked; 
    public float maxDistance = 1.0f; // Maximum distance to respect the tracking
    public Text RealTimeTextCompletionRate; // Text where the completion rate will be displayed
    public Text RealTimeTextCompletionRateAveraged; // Text where the completion rate will be displayed
    private float completionRate; // Instant completion rate
    private float completionRateAveraged; // Average completion rate
    //private int sampleCount = 0; // Count of completion rate calculated so far
    private float[] completionRates; // Array of all the completion rate so far

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectTracking != null && objectTracked != null)
        {
            // Actualise the sample count
            //sampleCount ++;

            // Calculate the vertical distance between the 2 objects
            float distance = Vector3.Distance(new Vector3(0.0f, objectTracking.position.y, 0.0f),
                new Vector3(0.0f, objectTracked.position.y, 0.0f));

            // Calculate the completion rate
            completionRate = 1.0f - (distance / maxDistance);

            // Limit the completion rate between 0 and 1
            completionRate = Mathf.Clamp(completionRate, 0.0f, 1.0f);

            // Transform the completion rate into a percentage
            completionRate = 100 * completionRate;

            // Actualise the text to display the real time completion rate
            RealTimeTextCompletionRate.text = "Completion rate: " + string.Format("{0:F0}", completionRate) + "%";

            // Actualise the averaged completion rate and display it in real time
            //completionRates.Add(completionRate);
            //completionRateAveraged = completionRates.Average();
            //completionRateAveraged = (completionRateAveraged + completionRate) / sampleCount;
            //RealTimeTextCompletionRateAveraged.text = "Averaged completion rate: " + string.Format("{0:F0}", completionRateAveraged) + "%";
        }
    }
}
