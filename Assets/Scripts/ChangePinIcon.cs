using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePinIcon : MonoBehaviour
{
    Image image;
    [SerializeField] Sprite available;
    [SerializeField] Sprite visited;
    [SerializeField] Sprite locked;

    private void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            image.sprite = available;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            image.sprite = visited;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            image.sprite = locked;
        }
    }
}
