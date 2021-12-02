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

    [SerializeField] GameObject pin;

    Zoom zoomScript1;
    Zoom3D zoomScript2;

    

    public bool overworldCamera = true;

    void Start()
    {
        zoomScript1 = vcam1.GetComponent<Zoom>();
        zoomScript2 = vcam2.GetComponent<Zoom3D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            switchPriority();
        }

        if (overworldCamera)
        {
            if (vcam1.transform.position.y < zoomScript1.switchCameraThreshold)
            {
                switchPriority();
                while (vcam1.transform.position.y < zoomScript1.switchCameraThreshold)
                {
                    vcam1.transform.position += new Vector3(0, 1, 0);
                }

            }
        }
        else if (vcam2.transform.position.y > zoomScript2.switchCameraThreshold)
        {
            switchPriority();
            vcam1.transform.rotation = pin.transform.rotation;
            while (vcam2.transform.position.y > zoomScript2.switchCameraThreshold)
            {
                vcam2.transform.position -= new Vector3(0, 1, 0);
            }
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
