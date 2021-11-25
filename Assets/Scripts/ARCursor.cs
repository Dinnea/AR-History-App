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

    public bool useCursor = true;
    public bool isSceneAdded = false;
    public bool arePlanesOn = true;

    [SerializeField] List<GameObject> popUps = new List<GameObject>(4);

    private void Start()
    {
        cursor.SetActive(useCursor);
        StartCoroutine(Box2());
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
                    GameObject.Instantiate(placeholder, hits[0].pose.position, hits[0].pose.rotation);
                }
                isSceneAdded = true;                
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

    IEnumerator Box2(float time = 7)
    {
        yield return new WaitForSeconds(time);

        popUps[0].SetActive(false);
        popUps[1].SetActive(true);

        StartCoroutine(Box3());
    }

    IEnumerator Box3(float time = 5) 
    {
        yield return new WaitForSeconds(time);

        popUps[1].SetActive(false);
        popUps[2].SetActive(true);

        StartCoroutine(Box4());
    }

    IEnumerator Box4(float time = 5) 
    {
        yield return new WaitForSeconds(time);

        popUps[2].SetActive(false);
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
