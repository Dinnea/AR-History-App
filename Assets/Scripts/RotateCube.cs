using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float rotatespeed = 10f;
    private float startingPosition;

    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, rotatespeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.down, rotatespeed * Time.deltaTime);
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startingPosition = touch.position.x;
                    break;
                case TouchPhase.Moved:
                    if (startingPosition > touch.position.x)
                    {
                        transform.Rotate(Vector3.up, rotatespeed * Time.deltaTime);
                    }
                    else if (startingPosition < touch.position.x)
                    {
                        transform.Rotate(Vector3.down, rotatespeed * Time.deltaTime);
                    }
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch Phase Ended.");
                    break;
                case TouchPhase.Stationary:
                    startingPosition = touch.position.x;
                    break;
            }
        }
    }
}
