using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingClouds : SceneTreeObject
{
    public FloatVariable slideSpeed;

    private Image image;

    private bool slide;

    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        image.enabled = true;
        Continue();
    }

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        image.enabled = false;
	}
}
