using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsContainer : MonoBehaviour
{

    public string containerName;

    [SerializeField]
    private SoundEffect[] _soundEffects;

    // Use this for initialization
    void Start ()
    {

        if (containerName.Length <= 0)
        {
            Debug.LogWarning(gameObject + " The container doesn't have a name, name the container so objects can find the audioclips inside it");
        }

        foreach (SoundEffect soundEffect in _soundEffects)
        {
            if (soundEffect != null)
            {
                soundEffect.Initialize();
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public SoundEffect SendClip()
    {
        bool clipExsists = false;
        int clipToSend = Random.Range(0, _soundEffects.Length);
        for (int i = 0; i < _soundEffects.Length; i++)
        {
            if (_soundEffects[i] != null)
            {
                clipExsists = true;
                break;
            }
        }
        if (clipExsists)
        {
            while (_soundEffects[clipToSend] == null)
            {
                clipToSend = Random.Range(0, _soundEffects.Length);
            }
            return _soundEffects[clipToSend];
        }
        else
            return null;
    }
}
