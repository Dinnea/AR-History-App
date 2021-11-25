using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_UI : MonoBehaviour
{
    [SerializeField] List<GameObject> popUps = new List<GameObject>(4);

    private void Start()
    {
        StartCoroutine(Box2());
    }
    IEnumerator Box2(float time = 7)
    {
        yield return new WaitForSeconds(time);

        popUps[0].SetActive(false);
        popUps[1].SetActive(true);

        StartCoroutine(Box3());
    }

    IEnumerator Box3(float time = 5)
    {
        yield return new WaitForSeconds(time);

        popUps[1].SetActive(false);
        popUps[2].SetActive(true);

        StartCoroutine(Box4());
    }

    IEnumerator Box4(float time = 5)
    {
        yield return new WaitForSeconds(time);

        popUps[2].SetActive(false);
    }
}
