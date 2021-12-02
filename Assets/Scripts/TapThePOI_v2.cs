using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapThePOI_v2 : MonoBehaviour
{
    private bool _hit;
    private RaycastHit _hitInfo = new RaycastHit();
    private GameObject _tapped;
    [SerializeField] GameObject _ui;
    [SerializeField] GameObject _map;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            _hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out _hitInfo);
            if(_hit) 

            {
                _tapped = _hitInfo.transform.gameObject;

                float distance = _tapped.GetComponent<FindDistance>().Distance();
                //Vector2 userPosition = new Vector2(transform.position.x, transform.position.z);
                //Vector2 tappedPosiiton = new Vector2(_tapped.transform.position.x, _tapped.transform.position.z);
               // float distance = tappedPosiiton.magnitude - userPosition.magnitude;

                Debug.Log(distance);
                if (Mathf.Abs(distance) < 100)
                {
                    if (_hitInfo.transform.gameObject.name == "Twente Airport")
                    {
                        _ui.SetActive(true);
                        _map.SetActive(false);

                    }
                }
            }
            
            
        }
    }
}
