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
    public ARAnchorManager _anchorManager;

    public bool useCursor = true;
    public bool isSceneAdded = false;
    public bool arePlanesOn = true;

    public Vector3 offset = new Vector3(0, 0, 10);

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
                    CreateAnchor(hits[0]);//GameObject.Instantiate(placeholder, hits[0].pose.position + offset, hits[0].pose.rotation, null);
                    /*  ListOfObjects list = placeholder.GetComponent<ListOfObjects>();
                      foreach(GameObject model in list.objectsInScene) 
                      {
                          model.transform.SetParent(AR.transform, true);
                      }*/
                    //TextHelper.Instance.SetText(hits[0]. .gameObject.name);
                    isSceneAdded = true;
                }
                              
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isSceneAdded) 
        {
            TapPlane();

           /* List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits);
            if (hits.Count > 0)
            {
                //GameObject.Instantiate(placeholder, hits[0].pose.position, hits[0].pose.rotation, AR.transform);
                /*  ListOfObjects list = placeholder.GetComponent<ListOfObjects>();
                  foreach(GameObject model in list.objectsInScene) 
                  {
                      model.transform.SetParent(AR.transform, true);
                  }
                //isSceneAdded = true;
                if (objectPressed.transform.CompareTag("Plane"))
                {
                    popUps[3].SetActive(true);
                }
            }*/
        }

        /*if (isSceneAdded && arePlanesOn)
        {
            toggle.TogglePlanes(false);
            arePlanesOn = false;
        }*/
    }

    ARAnchor CreateAnchor(in ARRaycastHit hit)
    {
        ARAnchor anchor;

        // If we hit a plane, try to "attach" the anchor to the plane
        if (hit.trackable is ARPlane plane)
        {
            var planeManager = GetComponentInParent<ARPlaneManager>();
            if (planeManager)
            {
                var oldPrefab = _anchorManager.anchorPrefab;
                _anchorManager.anchorPrefab = placeholder;
                anchor = _anchorManager.AttachAnchor(plane, hit.pose);
                _anchorManager.anchorPrefab = oldPrefab;

                Debug.Log($"Created anchor attachment for plane (id: {anchor.nativePtr}).");
                return anchor;
            }
        }
            // ... here, we'll place the plane anchoring code!

            // Otherwise, just create a regular anchor at the hit pose

            // Note: the anchor can be anywhere in the scene hierarchy
            var instantiatedObject = Instantiate(placeholder, hit.pose.position +offset, hit.pose.rotation, null);

        // Make sure the new GameObject has an ARAnchor component
        anchor = instantiatedObject.GetComponent<ARAnchor>();
        if (anchor == null)
        {
            anchor = instantiatedObject.AddComponent<ARAnchor>();
        }
        Debug.Log($"Created regular anchor (id: {anchor.nativePtr}).");

        return anchor;
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

    void TapPlane()
    {
        Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
        Debug.Log("Trying to ray cast");
        RaycastHit objectPressed;
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 999.0f);
        if (Physics.Raycast(ray, out objectPressed))
        {
            Debug.Log(objectPressed.transform.gameObject.name);
            if (objectPressed.transform.CompareTag("Plane"))
            {
                popUps[3].SetActive(true);
            }
        }
    }

    //mac n cheese
}
