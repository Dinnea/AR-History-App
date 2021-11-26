using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class TapThePOIv1 : MonoBehaviour
{
    [SerializeField] GameObject _ui;
    void OnMouseDown()
    {
        Debug.Log(name);
        _ui.SetActive(true);
    }
}
