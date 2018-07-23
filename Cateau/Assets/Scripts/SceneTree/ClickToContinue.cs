using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToContinue : SceneTreeObject
{
    private bool _continue = false;
    // Use this for initialization
    void Start()
    {
        _continue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKey)
        {
            if (_continue)
            {
                _continue = false;
                Continue();
            }
        }
    }

    public override void Continue(int nodeIndex)
    {

    }

    protected override void Initialize()
    {
        _continue = true;

    }
}
