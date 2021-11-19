using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipePages : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private int amountOfPanels = 3;
    private Vector3 panelLocation;

    private bool swipingEnabled;

    public Vector3 startLocation;
    public Vector3 endLocation;

    public List<float> panelLocations;

    public float percentThreshold = 0.2f;
    public float easing = 0.5f;

    public Image dot1;
    public Image dot2;
    public Image dot3;

    Color white;
    Color grey;

    public GameObject cube;

    void Start()
    {
        swipingEnabled = true;

        white = Color.white;
        grey = Color.grey;

        startLocation = transform.position;
        endLocation = startLocation - new Vector3(amountOfPanels * Screen.width, 0, 0);
        panelLocation = transform.position;

        panelLocations = new List<float>();
        for (int i = 0; i < amountOfPanels; i++)
        {
            panelLocations.Add(transform.position.x - (Screen.width * i));
        }
        
        Debug.Log("panels: " + amountOfPanels);
        Debug.Log("Position: " + transform.position);
        Debug.Log("startLoc: " + startLocation);
        Debug.Log("endLoc: " + endLocation);

        dot1.color = grey;

        cube.SetActive(false);
    }


    public void OnDrag(PointerEventData data)
    {
        if (swipingEnabled)
        {
            float difference = data.pressPosition.x - data.position.x;

            //set position of panelHolder by subtracting the difference from the original position
            transform.position = panelLocation - new Vector3(difference, 0, 0);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (swipingEnabled)
        {
            Debug.Log("ONENDDRAG");
            //calculate what the percentage of the length of the drag relative to the screenwidth is
            float percentage = (data.pressPosition.x - data.position.x) / Screen.width;

            //Debug.Log("Percentage: " + percentage);
            //check if it is bigger than the threshold
            if (Mathf.Abs(percentage) >= percentThreshold)
            {
                //Debug.Log("Drag is bigger than Threshold, continue.");
                //set newLocation to original position of panelHolder
                Vector3 newLocation = panelLocation;

                //if percentage is positive, subtract screenWidth (next screen)
                //if percentage is negative, add screenWidth (previous screen)
                if (percentage > 0)
                {
                    Debug.Log("Going to next screen");
                    newLocation += new Vector3(-Screen.width, 0, 0);
                }
                if (percentage < 0)
                {
                    Debug.Log("Going to previous screen");
                    newLocation += new Vector3(Screen.width, 0, 0);
                }

                //set position and original position to new screen
                if (newLocation.x <= startLocation.x && newLocation.x > endLocation.x)
                {
                    Debug.Log(endLocation.x + " < " + newLocation.x + " <= " + startLocation.x);
                    Debug.Log("newLocation: " + newLocation);
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing));
                    panelLocation = newLocation;

                    changeDot();
                }
                else
                {
                    Debug.Log("This is the end");
                    StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
                }
            }
            else StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }  
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    private void changeDot()
    {
        Debug.Log("change dot");

        if (panelLocation.x == panelLocations[0])
        {
            Debug.Log("dot1");
            dot1.color = grey;
            dot2.color = white;
            dot3.color = white;
        }
        if (panelLocation.x == panelLocations[1])
        {
            Debug.Log("dot2");
            dot1.color = white;
            dot2.color = grey;
            dot3.color = white;
        }
        if (panelLocation.x == panelLocations[2])
        {
            Debug.Log("dot3");
            dot1.color = white;
            dot2.color = white;
            dot3.color = grey;
        }
    }

    public void DisableSwiping()
    {
        swipingEnabled = false;
        cube.SetActive(true);
    }

    public void EnableSwiping()
    {
        swipingEnabled = true;
        cube.SetActive(false);
    }
}
