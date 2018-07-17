using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBeforeContinue : SceneTreeObject
{
    public float timeToWait;

    private float time;

    private bool timerTicking;

    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        timerTicking = true;
        time = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (timerTicking)
        {
            time += Time.deltaTime;
            if (time > timeToWait)
            {
                timerTicking = false;
                Continue();
            }
        }
	}
}
