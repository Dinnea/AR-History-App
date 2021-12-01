using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera vcam1;
    [SerializeField]
    private CinemachineVirtualCamera vcam2;

    private bool overworldCamera = true;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            switchPriority();
        }
    }

    private void switchPriority()
    {
        if (overworldCamera)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        overworldCamera = !overworldCamera;
    }
}
