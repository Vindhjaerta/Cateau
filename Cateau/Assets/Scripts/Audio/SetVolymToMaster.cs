using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolymToMaster : MonoBehaviour
{
    private AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameStateContainer.Instance != null)
        {
            audioSource.volume = GameStateContainer.Instance.settings.masterVolume;
        }
	}
	
}
