using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltLabel : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
