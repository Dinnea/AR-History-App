using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDistance : MonoBehaviour
{
    GameObject _player;
    private void Start()
    {
        _player = GameObject.Find("Player");
    }
    public float Distance()
    {

        Vector2 userPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 pinPosition = new Vector2(transform.position.x, transform.position.z);

        float distance = Mathf.Abs((userPosition - pinPosition).magnitude);

        return distance;
    }
}
