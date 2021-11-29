using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapThePOI_v3 : MonoBehaviour
{
    [SerializeField] FindDistance _distance;
    [SerializeField] GameObject _pin;
    [SerializeField] GameObject _ui;

    public void Awake()
    {
        //_ui = GameObject.Find("UI");
       // _ui.SetActive(false);
    }
    public void TapThePOI() 
    {
        if (_distance.Distance() < 100) 
        {
            if(_pin.name == "Twente Airport") 
            {
                _ui.SetActive(true);
            }
        }
    }


}
