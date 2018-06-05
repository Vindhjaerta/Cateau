using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbienceContainer : MonoBehaviour
{

    public string containerName;
    [SerializeField]
    private int _audioPriority;

    public bool oneShotContainer;
    [SerializeField]
    private AmbienceSound[] _ambienceSounds;

    private AmbienceManager _ambienceManager;


    //Private timers for clip length and bools to make sure we don't spam audioclips
    private float _sentClipLength = 0;
    [HideInInspector]
    public bool clipHasBeenChosen = false;

    private AmbienceSound _ambienceToPlay;

    void Start ()
    {
        _ambienceManager = GetComponentInParent<AmbienceManager>();

        if (containerName.Length <= 0)
        {
            Debug.LogWarning(gameObject + " The container doesn't have a name, name the container so objects can find the audioclips inside it");
        }

        foreach (AmbienceSound ambienceSound in _ambienceSounds)
        {
            if (ambienceSound != null)
            {
                ambienceSound.Initialize();
            }
        }
        if (_audioPriority <= 0)
        {
            Debug.LogError(gameObject + " has priority set to 0, set it to something above that");
        }

       if (_ambienceSounds.Length <= 0)
        {
            Debug.LogWarning(gameObject + " doens't have any audioclips in it!");
        }

	}
	
	void Update ()
    {
        if (oneShotContainer)
        {
            CountDownAndSendAmbienceSound();
        }
        else if (oneShotContainer == false)
        {
            if (_sentClipLength < 0)
            {
                UpdateContinousSound();
            }
            _sentClipLength -= Time.deltaTime;
        }
    }

    private void CountDownAndSendAmbienceSound()
    {
        foreach (AmbienceSound ambienceSound in _ambienceSounds)
        {
            if (ambienceSound != null)
            {
                ambienceSound.UpdateTime(Time.deltaTime);
                if (ambienceSound.readyToPlay)
                {
                    ambienceSound.time = 0;
                    ambienceSound.PrepareToSend(_audioPriority);
                    _ambienceManager.AddToQueue(ambienceSound);
                    ambienceSound.readyToPlay = false;
                }
            }
        }
    }

    private void UpdateContinousSound()
    {
        if (_ambienceSounds.Length > 0)
        {
            if (_sentClipLength < 0.5)
            {
                int randomClipToSend = Random.Range(0, _ambienceSounds.Length);
                if (_ambienceSounds[randomClipToSend] != null)
                {
                    _ambienceManager.AddToQueue(_ambienceSounds[randomClipToSend]);
                    _sentClipLength = _ambienceSounds[randomClipToSend].audioClip.length;
                    //Debug.Log(_ambienceSounds[randomClipToSend].name + "  " + _ambienceSounds[randomClipToSend].audioClip.length);
                }
            }
        }
    }
}
