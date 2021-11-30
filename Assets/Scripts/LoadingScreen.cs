using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField] Image dot1;
    [SerializeField] Image dot2;
    [SerializeField] Image dot3;

    [SerializeField] Sprite selected;
    [SerializeField] Sprite notSelected;

    public float timeBetweenDots;
    public float minLoadingTime;
    float prevTime;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());

        prevTime = Time.time;
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone || Time.realtimeSinceStartup < minLoadingTime)
        {
            changeDot();

            if (operation.progress >= 0.9f && Time.realtimeSinceStartup > minLoadingTime)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    private void changeDot()
    {

        float passedTime = Time.fixedTime - prevTime;

        if (passedTime > timeBetweenDots && passedTime < (timeBetweenDots * 2))
        {
            Debug.Log("dot1");
            dot1.sprite = selected;
            dot2.sprite = notSelected;
            dot3.sprite = notSelected;
        }
        if (passedTime > (timeBetweenDots*2) && passedTime < (timeBetweenDots * 3))
        {
            Debug.Log("dot2");
            dot1.sprite = notSelected;
            dot2.sprite = selected;
            dot3.sprite = notSelected;
        }
        if (passedTime > (timeBetweenDots*3) && passedTime < (timeBetweenDots * 4))
        {
            Debug.Log("dot3");
            dot1.sprite = notSelected;
            dot2.sprite = notSelected;
            dot3.sprite = selected;

            prevTime = Time.fixedTime;
        }
        /*
        if (passedTime > timeBetweenDots * 4)
        {
            p
        }
        */
    }

}
