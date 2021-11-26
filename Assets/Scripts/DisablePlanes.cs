using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlanes : MonoBehaviour
{
    [SerializeField] ARPlaneToggle toggle;
    private void OnMouseDown()
    {
        toggle.TogglePlanes(false);
    }
}
