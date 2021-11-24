using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float zoomSpeed = 1;

    public float threshold;

    public GameObject playerSphere;

    public bool zooming = true;

    // Update is called once per frame
    void Update()
    {
        if (zooming)
        {
            ZoomMap();
        }
        else RotateMap();

        transform.LookAt(playerSphere.transform);
    }

    void zoom(float increment)
    {
        if (Camera.main.transform.position.y + increment > zoomOutMin && Camera.main.transform.position.y + increment < zoomOutMax)
        {
            if (Input.touchCount == 2)
            {
                Camera.main.transform.position += new Vector3(0, increment, 0);
            }

        }
    }

    void ZoomMap() 
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(-difference * zoomSpeed);
        }
    }

    void RotateMap()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector3 prevPos1 = touchZero.position - touchZero.deltaPosition;  // Generate previous frame's finger positions
            Vector3 prevPos2 = touchOne.position - touchOne.deltaPosition;

            Vector3 prevDir = prevPos2 - prevPos1;
            Vector3 currDir = touchOne.position - touchZero.position;
            float angle = Vector2.SignedAngle(prevDir, currDir);
            
            // Rotate by the deltaAngle between the two vectors
            transform.RotateAround(playerSphere.transform.position, playerSphere.transform.up, angle);
        }
    }
}
