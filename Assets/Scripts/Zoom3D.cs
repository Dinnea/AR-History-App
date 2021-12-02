using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom3D : MonoBehaviour
{
    // -------- zooming --------
    [Header("Zooming")]
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float zoomSpeed = 1;


    //--------- tilting ---------
    [Header("Tilting")]

    public Vector3 startRot;
    public Vector3 endRot;

    //public float min = 50;
    //public float max = 90;

    public float switchCameraThreshold = 90f;

    public float offset = -100;

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

    CinemachineSwitcher cs;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pin;

    private Vector2 screenCenter;

    void Start()
    {
        cs = GameObject.Find("CinemachineManager").GetComponent<CinemachineSwitcher>();

        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        transform.LookAt(player.transform);

        if (Input.touchCount == 2 && !cs.overworldCamera)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //zoom stuff
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitude - prevMagnitude;

            zoom(-difference * zoomSpeed);
        }

        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;

            //rotate stuff
            Vector3 prevDir = screenCenter - touchZeroPrevPos;
            Vector3 currDir = screenCenter - touchZero.position;
            float angle = Vector2.SignedAngle(prevDir, currDir);

            transform.RotateAround(player.transform.position, player.transform.up, angle);
            pin.transform.Rotate(0, 0, -angle);
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
