using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextHelper : MonoBehaviour
{
    private static TextHelper instance;

    public static TextHelper Instance { get => instance; }
    private Text me;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        me = GetComponent<Text>();
    }

    public void SetText(string text)
    {
        me.text = text;
    }
}
