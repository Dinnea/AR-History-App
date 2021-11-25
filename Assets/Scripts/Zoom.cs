using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Zoom : MonoBehaviour
{
    // -------- zooming --------
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float zoomSpeed = 1;

    public float threshold;
    public GameObject point;

    public bool zooming = true;

    //--------- tilting ---------
    Quaternion q_startRot;
    Quaternion q_endRot;

    public Vector3 startRot;
    public Vector3 endRot;

    public float min = 50;
    public float max = 90;

    public float offset = -100;
    public float buffer = 0.1f;

    public float v;
    public float z;

    public bool isTilting;

    List<float> angles = new List<float>();
    List<float> differences = new List<float>();

    public float maxListCount = 20f;

    public bool isRotating, isZooming;

    float prevPosY;


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

            if (isRotating && !isTilting)
            {
                transform.RotateAround(point.transform.position, point.transform.up, angle);
            }
            else if (isZooming)
            {
                zoom(-difference * zoomSpeed);
            }

            if (differences.Count > maxListCount)
            {
                Debug.Log("difference: " + differences.Average() + ", angle: " + angles.Average());

                if (isTilting)
                {
                    isZooming = true;
                    isRotating = false;
                }
                else if (differences.Average()*5 > angles.Average())
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

        //if (!isRotating) tiltMap();
        tiltMap();
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
            transform.RotateAround(point.transform.position, point.transform.up, angle);
            //transform.Rotate(0, angle, 0);
        }
    }



    private void tiltMap()
    {
        if (transform.position.y > min && transform.position.y < max)
        {
            if (!isTilting)
            {
                if (transform.position.y > prevPosY)
                {
                    transform.rotation = q_startRot;
                    transform.position = new Vector3(0, transform.position.y, offset);
                }
                
                if (transform.position.y < prevPosY)
                {
                    transform.rotation = q_endRot;
                }
            }

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
                isTilting = false;
            }
        }

        prevPosY = transform.position.y;
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

