using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_UI : MonoBehaviour
{
    [SerializeField] List<GameObject> popUps;
    private int _number = 0;
    private int _oldNumber;

    private void Start()
    {
        popUps = new List<GameObject>();
        _oldNumber = _number;
    }

    private void Update()
    {
        if (_oldNumber != _number) 
        {
            switch (_number) 
            {
                case 1:
                    popUps[0].SetActive(false);
                    popUps[1].SetActive(true);
                    break;
                case 2:
                    popUps[1].SetActive(false);
                    popUps[2].SetActive(true);
                    break;
            }
        }
    }

    private void OnMouseDown()
    {
        _number++;
    }
}
