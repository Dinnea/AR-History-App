using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneToggle : MonoBehaviour
{
    private ARPlaneManager _planeManager;

    private void Awake()
    {
        _planeManager = GetComponent<ARPlaneManager>();
    }

    public void TogglePlanes(bool isActive) 
    {
        if (isActive)_planeManager.enabled = true;
        else _planeManager.enabled = false;

        if (_planeManager.enabled) SetAllPlanesActive(true);
        else SetAllPlanesActive(false);
    }

    private void SetAllPlanesActive( bool isActive) 
    {
        foreach (var plane in _planeManager.trackables) plane.gameObject.SetActive(isActive);
    }
}
