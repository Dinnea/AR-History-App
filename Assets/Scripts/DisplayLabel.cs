using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLabel : MonoBehaviour
{
    public float displayThreshold;
    public Camera cam;
    public GameObject label;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        label = GameObject.Find("Twente Airport Label");
    }

    // Update is called once per frame
    void Update()
    {
        if (label == null)
        {
            label = GameObject.Find("Twente Airport Label");
        }
        else
        {
            Debug.Log("CAM: " + cam.transform.position.y);

            if (cam.transform.position.y < displayThreshold)
            {
                label.SetActive(true);
            }
            else label.gameObject.SetActive(false);
        }
        
    }
}
