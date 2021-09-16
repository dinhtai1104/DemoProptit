using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenAspect = screenWidth * 1.0f / screenHeight;
        float milestoneAspect = 9f / 16f;

        if (screenAspect <= milestoneAspect)
        {
            Camera.main.orthographicSize = (1080 / 100.0f) / (2 * screenAspect);
        }
        else
        {
            Camera.main.orthographicSize = 9.6f; //1560f / 200f
        }
    }

   
}
