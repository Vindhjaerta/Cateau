using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubScene : SceneTreeObject {

    private Image _img;

    public override void Continue(int nodeIndex)
    {
        if (_img != null)
        {
            _img.enabled = true;
        }
        else
        {
            Debug.Log("Image missing in " + gameObject);
        }
        Continue();
    }

    protected override void Initialize()
    {
        _data.type = ESceneTreeType.SubScene;
        _data.sender = this;
    }

    // Use this for initialization
    void Awake () {
        _img = GetComponent<Image>();
        if(_img != null)
        {
            _img.enabled = false;
        }
        else
        {
            Debug.Log("Image missing in " + gameObject);
        }
	}
	
    public void SetImage(bool isActive)
    {
        if (_img != null)
        {
            _img.enabled = isActive;
        } else
        {
            Debug.Log("Image missing in " + gameObject);
        }
    }
}
