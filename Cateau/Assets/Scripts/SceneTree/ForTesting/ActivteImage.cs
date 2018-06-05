using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivteImage : SceneTreeObject
{
    private Image _image;    
    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        _image.enabled = true;
        Continue();
    }

    // Use this for initialization
    void Start ()
    {
        _image = GetComponent<Image>();
        if (_image.enabled == true)
        {
            _image.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
