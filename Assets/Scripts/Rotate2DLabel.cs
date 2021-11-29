using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2DLabel : MonoBehaviour
{
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.rotation = Camera.main.transform.rotation;
    }
}
