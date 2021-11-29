using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLabel : MonoBehaviour
{
    public float displayThreshold;
    public Camera cam;
    public GameObject label;
    public GameObject[] labels;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        label = GameObject.Find("Twente Airport Label");

    }

    // Update is called once per frame
    void Update()
    {
        if (labels.Length == 0)
        {
            labels = GameObject.FindGameObjectsWithTag("Label");
        }
        else
        {
            //Debug.Log("CAM: " + cam.transform.position.y);

            if (cam.transform.position.y < displayThreshold)
            {
                foreach (GameObject label in labels)
                {
                    label.SetActive(true);
                }

            }
            else
            {
                foreach (GameObject label in labels)
                {
                    label.gameObject.SetActive(false);
                }
            }
            
        }
        
    }
}
