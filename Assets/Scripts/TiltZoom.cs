using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltZoom : MonoBehaviour
{
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

    public GameObject playerSphere;

    // Start is called before the first frame update
    void Start()
    {
        //convert Euler angles to Quaternions
        q_startRot = Quaternion.Euler(startRot);
        q_endRot = Quaternion.Euler(endRot);
    }

    // Update is called once per frame
    void Update()
    {
        tiltMap();

        
    }


    private float map(float min, float max, float toMin, float toMax, float value)
    {
        Debug.Log("Min: " + min + ", Max: " + max + ", Value: " + value);
        if (value <= min)
        {
            Debug.Log(value + " <= " + min+", return 0");
            return toMin;
        }
        else if (value >= max)
        {
            Debug.Log(value + " >= " + max + ", return 1");
            return toMax;
        }
        else
        {
            Debug.Log("else");
            return ((toMax - toMin) * (value - min) / (max - min)) + toMin;
        }
        
    }

    private void tiltMap()
    {
        if (transform.position.y > min + buffer && transform.position.y < max - buffer)
        {
            v = map(min, max, 0, 1, transform.position.y);
            transform.rotation = Quaternion.Slerp(q_startRot, q_endRot, v);
        }


        z = map(min, max, offset, 0, transform.position.y);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}
