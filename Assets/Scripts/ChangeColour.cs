using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    GameObject _player;
    [SerializeField] Material _closeEnough;
    [SerializeField] Material _tooFar;
    public float distance;

    private void Start()
    {
        _player = GameObject.Find("Player");

    }

    private void Update()
    {
        distance = ReturnDistance();

        if (distance < 10) 
        {
            
        }
    }

    public float ReturnDistance() 
    {
        
        Vector2 userPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 pinPosition = new Vector2(transform.position.x, transform.position.z);

        float distance = Mathf.Abs((userPosition - pinPosition).magnitude);

        return distance;
    }
}
