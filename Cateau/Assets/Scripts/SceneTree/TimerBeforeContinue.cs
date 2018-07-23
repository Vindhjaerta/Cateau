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

    }

    protected override void Initialize()
    {
        timerTicking = true;
        time = 0;
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
