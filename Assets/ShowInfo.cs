using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] GameObject _infoBox;
    private void OnMouseDown()
    {
        _infoBox.SetActive(true);
    }
}
