using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Zoom : MonoBehaviour
{
    // -------- zooming --------
    [Header("Zooming")]
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float zoomSpeed = 1;


    //--------- tilting ---------
    [Header("Tilting")]
    Quaternion q_startRot;
    Quaternion q_endRot;

    public Vector3 startRot;
    public Vector3 endRot;

    public float min = 50;
    public float max = 90;

    public float offset = -100;

    public GameObject rotateAround;
   
    private float v;
    private float z;

    List<float> angles = new List<float>();
    List<float> differences = new List<float>();

    [Header("Tweak zooming/rotating:")]
    public float maxListCount = 20f;        //amount of movements used to calculate average difference/angle
    public float factor;                    //average difference is multiplied by this factor, so it's closer to angle value

    //-------- booleans ---------
    [Header("Booleans")]
    public bool isTilting;
    public bool isZooming;
    public bool isRotating;

    void Start()
    {
        //convert Euler angles to Quaternions
        q_startRot = Quaternion.Euler(startRot);
        q_endRot = Quaternion.Euler(endRot);
    }

    void Update()
    {

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //zoom stuff
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitude - prevMagnitude;

            //rotate stuff
            Vector3 prevDir = touchOnePrevPos - touchZeroPrevPos;
            Vector3 currDir = touchOne.position - touchZero.position;
            float angle = Vector2.SignedAngle(prevDir, currDir);

            differences.Add(difference);
            angles.Add(angle);

            if (isTilting || isZooming)
            {
                zoom(-difference * zoomSpeed);
            }
            else if (isRotating)
            {
                transform.Rotate(0, 0, -angle);
            }

            if (!isTilting) rotateOrZoom();

            tiltMap();
        }

        if (isTilting && Input.touchCount == 1)
        {
            Debug.Log("one touch");
            Touch touch = Input.GetTouch(0);
            Vector2 touchPrevPos = touch.position - touch.deltaPosition;

            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

            Vector3 prevDir = touchPrevPos - screenCenter;
            Vector3 currDir = touch.position - screenCenter;
            float angle = Vector2.SignedAngle(prevDir, currDir);

            transform.RotateAround(rotateAround.transform.position, rotateAround.transform.up, -angle);
        }  
    }

    void rotateOrZoom()
    {
        if (differences.Count > maxListCount)
        {
            Debug.Log("difference: " + differences.Average() + ", angle: " + angles.Average());

            if (Mathf.Abs(differences.Average()) * factor > Mathf.Abs(angles.Average()))
            {
                isZooming = true;
                isRotating = false;
            }
            else
            {
                isRotating = true;
                isZooming = false;
            }

            differences.Clear();
            angles.Clear();
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

            Vector3 prevPos1 = touchZero.position - touchZero.deltaPosition;
            Vector3 prevPos2 = touchOne.position - touchOne.deltaPosition;

            Vector3 prevDir = prevPos2 - prevPos1;
            Vector3 currDir = touchOne.position - touchZero.position;
            float angle = Vector2.SignedAngle(prevDir, currDir);

            transform.Rotate(0, angle, 0);
        }
    }



    private void tiltMap()
    {
        //StartCoroutine(transformPos());

        if (transform.position.y > min && transform.position.y < max)
        {
            v = map(min, max, 0, 1, transform.position.y);
            transform.rotation = Quaternion.Slerp(q_startRot, q_endRot, v);
            
            z = map(min, max, offset, 0, transform.position.y);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);

            isTilting = true;
        }
        else if (isTilting)
        {
            if (transform.position.y > max)
            {
                transform.rotation = q_endRot;
                transform.position += new Vector3(0, 0, -transform.position.z);
                isTilting = false;
            }

            if (transform.position.y < min)
            {
                transform.rotation = q_startRot;
                transform.position += new Vector3(0, 0, offset - transform.position.z);
                isTilting = true;
            }
        }
    }

    /*
    IEnumerator transformPos()
    {
        if (transform.position.y > min && transform.position.y < max)
        {
            z = map(min, max, offset, 0, transform.position.y);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);

            isTilting = true;
        }
        else if (isTilting)
        {
            if (transform.position.y > max)
            {
                transform.position += new Vector3(0, 0, -transform.position.z);
                isTilting = false;
            }

            if (transform.position.y < min)
            {
                transform.position += new Vector3(0, 0, offset - transform.position.z);
                isTilting = true;
            }
        }

        yield return null;
    }
    */

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

    private float map(float min, float max, float toMin, float toMax, float value)
    {
        if (value <= min) return toMin;
        else if (value >= max) return toMax;
        else
        {
            return ((toMax - toMin) * (value - min) / (max - min)) + toMin;
        }
    }
}

