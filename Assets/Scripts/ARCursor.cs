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
    [SerializeField] MeshRenderer _plane;

    public bool useCursor = true;
    public bool isSceneAdded = false;

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
                _plane.enabled = false;
            }
            else 
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);
                if (hits.Count >0) 
                {
                    GameObject.Instantiate(placeholder, hits[0].pose.position, hits[0].pose.rotation);
                }
                isSceneAdded = true;
                _plane.enabled = false;
            }
            
        }
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

}
