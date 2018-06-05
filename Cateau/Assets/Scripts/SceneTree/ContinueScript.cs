using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : SceneTreeObject {
    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        Continue();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
