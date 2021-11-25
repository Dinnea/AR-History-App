using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapThePOI_v2 : MonoBehaviour
{
    private bool _hit;
    private RaycastHit _hitInfo = new RaycastHit();
    private GameObject _tapped;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            _hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out _hitInfo);
            _tapped = _hitInfo.transform.gameObject;
            if () 
            {
                if (_hitInfo.transform.gameObject.name == "Twente Airport")
                {
                }
            }
            
        }
    }
}
