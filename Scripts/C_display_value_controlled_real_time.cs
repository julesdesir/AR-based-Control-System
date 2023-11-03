using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Text_value_controlled_real_time: MonoBehaviour
{
    public Transform ObjectFollowed; // Object with the position we want
    public Text RealTimeText; // Text where the position will be displayed

    void Update()
    {
        // Check whether ObjectFollowed and RealTimeText do exist
        if (ObjectFollowed != null && RealTimeText != null)
        {
            // Obtain the position of ObjectFollowed
            Vector3 position = ObjectFollowed.position;

            // Actualise the text to display the real time position of ObjectFollowed
            RealTimeText.text = "Controlled value: " + string.Format("{0:F2}", position.y);
        }
    }
}
