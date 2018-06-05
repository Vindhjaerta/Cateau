using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/SoundEffect")]
public class SoundEffect : ScriptableObject
{
    public string soundEffectname;
    public AudioClip audioClip;
    [Range(0f, 1.0f)]
    public float volume = 0.5f;
    [HideInInspector]
    public float pitch = 1;
    public Vector2 randomPitch;
    /*[Range(1f, 500f)]
    public float playSoundInterval = 10;*/
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


    private SoundEffectsManager _soundEffectsManager;

    private AudioSource _audioSource;


    public void Initialize()
    {
        time = 0;
        _soundEffectsManager = null;
        _audioSource = null;
        clipDonePlaying = 0;
        starterPan = 0;
    }

    public void PrepareToSend(int inPriority)
    {
        priority = inPriority;
    }

    public void PlaySound(SoundEffectsManager soundEffectsManager, AudioSource audioSource)
    {
        _soundEffectsManager = soundEffectsManager;
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
        audioSource.volume = _soundEffectsManager.maxVolume * volume;
        audioSource.Play();
        _soundEffectsManager._playingSoundEffectList.Add(this);
    }

    public void UpdateSound(float time)
    {
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
            _soundEffectsManager._playingSoundEffectList.Remove(this);
        }
        this.time = 0;
    }
}
