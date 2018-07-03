using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotChange : MonoBehaviour
{
    public AudioMixerSnapshot audioMixerSnapshot;
    public float transitionTime = 4;

	void Start ()
    {
		if (audioMixerSnapshot != null)
        {
            audioMixerSnapshot.TransitionTo(transitionTime);
        }
	}
	
}
