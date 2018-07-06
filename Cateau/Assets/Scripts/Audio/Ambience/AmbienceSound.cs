using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Ambience")]
public class AmbienceSound : ScriptableObject
{
    public AudioMixerGroup outputAudioMixerGroup;
    public string ambienceSoundname;
    public AudioClip audioClip;
    [Range(0f, 1.0f)]
    public float volume = 0.5f;
    [HideInInspector]
    public float pitch = 1;
    public Vector2 randomPitch;
    [Range(1f, 500f)]
    public bool readyToPlay;

    public bool randomStartTimer = false;
    public Vector2 startTimeInterval = new Vector2(0, 50);
    private float startTime;

    public Vector2 playSoundRandomInterval = new Vector2(5,10);
    private float timeToplaySound = 0;

    private bool firstTimePlaying = true;

    [HideInInspector]
    public float time = 0;

    public bool staticPanAudio;
    [Range(-1f, 1f)]
    public float staticPanAudioSlider;

    public bool randomPanAudio;

    public float panDuration = 1;

    private int starterPan;

    [HideInInspector]
    public float clipDonePlaying;

    [HideInInspector]
    public int priority = 10;

    public bool addToQueue = true;


    private AmbienceManager _ambienceManager;

    private AudioSource _audioSource;


    public void Initialize()
    {
        //Debug.Log(name + " has been initialized");
        time = 0;
        _ambienceManager = null;
        _audioSource = null;
        clipDonePlaying = 0;
        starterPan = 0;
        readyToPlay = false;
        startTime = 0;
        timeToplaySound = 0;
        firstTimePlaying = true;

        if (randomStartTimer)
        {
            startTime = Random.Range(startTimeInterval.x, startTimeInterval.y);
            //Debug.Log(name + " randomstart time: " + startTime);
        }
        else
        {
            startTime = 0;
        }
    }

    public void PrepareToSend(int inPriority)
    {
        priority = inPriority;
    }

    public void PlaySound(AmbienceManager ambienceManager, AudioSource audioSource)
    {
        _ambienceManager = ambienceManager;
        _audioSource = audioSource;
        if (randomPitch.x != 0 && randomPitch.y > randomPitch.x)
        {
            pitch = Random.Range(randomPitch.x, randomPitch.y);
        }
        if (randomPanAudio)
        {
            starterPan = Random.Range(-1, 2);
            while (starterPan == 0)
            {
                starterPan = Random.Range(-1, 2);
            }
        }
        if (staticPanAudio)
        {
            audioSource.panStereo = staticPanAudioSlider;
        }
        else
        {
            audioSource.panStereo = starterPan;
        }
        audioSource.clip = audioClip;
        audioSource.pitch = pitch;
        audioSource.volume = ambienceManager.maxVolume * volume;
        audioSource.Play();
        _ambienceManager._playingAmbienceList.Add(this);
        //Debug.Log("Ambience sound playing: " + name);

        if (outputAudioMixerGroup != null)
        {
            _audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
        }
        else
        {
            _audioSource.outputAudioMixerGroup = null;
        }
    }

    public void UpdateSound(float time)
    {
        if (_audioSource != null)
        {
            _audioSource.volume = _ambienceManager.maxVolume * volume;
            clipDonePlaying += Time.deltaTime;
            if (randomPanAudio)
            {
                if (starterPan < 0)
                {
                    _audioSource.panStereo += (60 / (60 * (panDuration)) * time);
                }
                else
                {
                    _audioSource.panStereo -= (60 / (60 * (panDuration)) * time);
                }
            }
            if (staticPanAudio)
            {
                _audioSource.panStereo = staticPanAudioSlider;
            }
            if (clipDonePlaying > audioClip.length)
            {
                clipDonePlaying = 0;
                starterPan = 0;
                _ambienceManager._playingAmbienceList.Remove(this);
            }
            this.time = 0;
        }
    }

    public void UpdateTime(float time)
    {
        //Debug.Log(name + " has been updated " + this.time);
        if (timeToplaySound == 0)
        {
            //Debug.Log("TimeToPLaySound == 0: " + name);
            timeToplaySound = Random.Range(playSoundRandomInterval.x, playSoundRandomInterval.y);
            //Debug.Log(timeToplaySound);
        }
        this.time += time;
        if (randomStartTimer && firstTimePlaying)
        {
            //Debug.Log("randomStarTimer && firstTimePlaying == True");
            if (this.time > startTime)
            {
                //Debug.Log("Time to play first time: " + name);
                readyToPlay = true;
                firstTimePlaying = false;
            }
        }
        else if (this.time > timeToplaySound)
        {
            //Debug.Log("Time to play: " + name);
            readyToPlay = true;
            timeToplaySound = 0;
        }
    }


    public void StopPlaying()
    {
        if (_audioSource != null)
        {
            if (_audioSource.outputAudioMixerGroup != null)
            {
                _audioSource.outputAudioMixerGroup = null;
            }
            _audioSource.Stop();
            _audioSource = null;
        }
    }
}
