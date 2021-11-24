using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_UI : MonoBehaviour
{
    [SerializeField] List<GameObject> popUps = new List<GameObject>(4);
    private int _number = 0;
    private int _oldNumber = 0;

    private void Start()
    {
        _oldNumber = _number;
    }

    private void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (_number<3 && _number != -1) _number++;
            Debug.Log(_number);
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("Plane"))
                {
                    popUps[3].SetActive(true);
                }
            }
           ;

        }

        if (_oldNumber != _number) 
        {
            _oldNumber = _number;
            switch (_number) 
            {
                case -1:
                    for (int i = 0; i < 4; i++)
                    {
                        popUps[i].SetActive(false);
                    }
                    break;
                case 1:
                    popUps[0].SetActive(false);
                    popUps[1].SetActive(true);
                    break;
                case 2:
                    popUps[1].SetActive(false);
                    popUps[2].SetActive(true);
                    break;
                case 3:
                    popUps[2].SetActive(false);
                    break;
                case 4:
                    for (int i = 0; i<3; i++) 
                    {
                        popUps[i].SetActive(false);
                    }
                    
                    break;
            }
        }
    }
}
