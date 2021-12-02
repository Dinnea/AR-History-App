using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

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

    //public float min = 50;
    //public float max = 90;

    public float switchCameraThreshold = 90f;

    public float offset = -100;

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

    CinemachineSwitcher cs;
    
    [SerializeField]
    private CinemachineVirtualCamera vcam2;

    [SerializeField]
    private GameObject player;

    [SerializeField] 
    private GameObject pin;

    void Start()
    {
        cs = GameObject.Find("CinemachineManager").GetComponent<CinemachineSwitcher>();

        //convert Euler angles to Quaternions
        q_startRot = Quaternion.Euler(startRot);
        q_endRot = Quaternion.Euler(endRot);
    }

    void Update()
    {

        

        if (Input.touchCount == 2 && cs.overworldCamera)
        {
            pin.transform.rotation = transform.rotation;

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
            if (isRotating)
            {
                transform.Rotate(0, 0, -angle);
                vcam2.transform.RotateAround(player.transform.position, player.transform.up, angle);
            }

            //determine if it should be zooming or rotating
            //if (!isTilting) rotateOrZoom();

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

    void zoom(float increment)
    {
        if (transform.position.y + increment > zoomOutMin && transform.position.y + increment < zoomOutMax)
        {
            if (Input.touchCount == 2)
            {
                transform.position += new Vector3(0, increment, 0);
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

