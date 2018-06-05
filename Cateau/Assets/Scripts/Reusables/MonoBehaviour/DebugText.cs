using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    private Text _text;
    private static DebugText _instance;

    public static DebugText instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DebugText>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void Clear()
    {
        if(_text != null)
        {
            _text.text = "";
        }
    }

    public void AddLine(string text)
    {
        if(_text != null)
        {
            _text.text += System.Environment.NewLine + text;
        }
    }
}
