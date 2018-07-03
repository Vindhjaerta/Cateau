using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapShotChangeSceneTreeObject : SceneTreeObject
{
    public AudioMixerSnapshot audioMixerSnapshot;
    public float transitionTime = 4;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        if (audioMixerSnapshot != null && transitionTime >= 0)
        {
            audioMixerSnapshot.TransitionTo(transitionTime);
        }
    }
}
