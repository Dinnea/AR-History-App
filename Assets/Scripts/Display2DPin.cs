using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display2DPin : MonoBehaviour
{
    public GameObject[] flatPins;
    Zoom zoomScript;

    public int amountOfPins;

    // Start is called before the first frame update
    void Start()
    {
        zoomScript = GameObject.Find("Main Camera").GetComponent<Zoom>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flatPins.Length < amountOfPins)
        {
            flatPins = GameObject.FindGameObjectsWithTag("2D pin");
        }
        else
        {
            if (Camera.main.transform.position.y < zoomScript.switchCameraThreshold)
            {
                foreach (GameObject pin in flatPins)
                {
                    pin.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject pin in flatPins)
                {
                    pin.SetActive(true);
                }
            }
            
        }
    }
}
