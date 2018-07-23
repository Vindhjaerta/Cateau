using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideShowImage : SceneTreeObject
{
    private Image image;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        image.enabled = true;

    }

    // Use this for initialization
    void Start ()
    {
        image = GetComponent<Image>();
        image.enabled = false;
	}
	
}
