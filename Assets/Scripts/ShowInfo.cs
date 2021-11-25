using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] GameObject _infoBox;
    GameObject UI;

    private void Start()
    {
        UI = GameObject.Find("UI");
    }

    private void OnMouseDown()
    {
        //Instantiate(_infoBox, );
    }
}
