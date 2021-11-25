using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
   
    [SerializeField] Material _closeEnough;
    [SerializeField] Material _tooFar;
    public FindDistance findDistance;
    float distance;
    [SerializeField] List<Renderer> _childs;

    private void Start()
    {
        _childs = new List<Renderer>();
        findDistance = GetComponent<FindDistance>();
    }

    private void Update()
    {
        distance = findDistance.Distance();

        if (distance < 10)
        {
            foreach (Renderer skin in _childs)
            {
                skin.material = _closeEnough;
            }
        }
        else
        {
            foreach (Renderer skin in _childs)
            {
                skin.material = _tooFar;
            }
        }
    }

    
}
