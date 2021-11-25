using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCursor : MonoBehaviour
{
    public GameObject cursor;
    public GameObject placeholder;
    public ARRaycastManager raycastManager;
    public ARPlaneToggle toggle;
    [SerializeField]GameObject AR;
    [SerializeField] Camera arCamera;

    public bool useCursor = true;
    public bool isSceneAdded = false;
    public bool arePlanesOn = true;

    [SerializeField] List<GameObject> popUps = new List<GameObject>(4);

    private void Start()
    {
        cursor.SetActive(useCursor);
        
    }
    private void Update()
    {
        if (useCursor)
        {
            updateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isSceneAdded)
        {
            if (useCursor) 
            {
                GameObject.Instantiate(placeholder, transform.position, transform.rotation);
                isSceneAdded = true;
            }
            else 
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);
                if (hits.Count >0) 
                {
                    GameObject.Instantiate(placeholder, hits[0].pose.position, hits[0].pose.rotation, AR.transform);
                    isSceneAdded = true;
                }
                              
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isSceneAdded) 
        { 
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit objectPressed;
            if(Physics.Raycast(ray, out objectPressed)) 
            {
                if (objectPressed.transform.CompareTag("Plane")) 
                {
                    popUps[3].SetActive(true);
                }
            }
        }

        /*if (isSceneAdded && arePlanesOn)
        {
            toggle.TogglePlanes(false);
            arePlanesOn = false;
        }*/
    }

    void updateCursor() 
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, TrackableType.Planes);
        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

   
        // Code to execute after the delay
        //iteration 1:
        //turn off panel #1
        //turn on panel #2
        //iteration 2:
        //tun off panel 2
        //turn on panel 3
        //iteration 3 turn off panel 3
}
