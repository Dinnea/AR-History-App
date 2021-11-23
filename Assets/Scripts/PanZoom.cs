using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float zoomSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Debug.Log("touchCount 2");
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * zoomSpeed);
        }
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
}
