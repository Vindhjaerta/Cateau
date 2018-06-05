using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Music")]
public class MusicAsset : ScriptableObject
{
    public AudioClip audioClip;
    [Range(0f, 1.0f)]
    public float volume = 1f;
    [HideInInspector]
    public float pitch = 1;
    public Vector2 randomPitch;

    public bool staticPanAudio;
    [Range(-1f, 1f)]
    public float staticPanAudioSlider;

    [HideInInspector]
    public bool play;

    MusicStemsManager _musicStemsManager;

    AudioSource _audioSource;

    public void SetupSound(MusicStemsManager musicStemsManager, AudioSource audioSource)
    {
        _musicStemsManager = musicStemsManager;
        _audioSource = audioSource;
        if (randomPitch.x != 0 && randomPitch.y > randomPitch.x)
        {
            pitch = Random.Range(randomPitch.x, randomPitch.y);
        }
        
        if (staticPanAudio)
        {
            audioSource.panStereo = staticPanAudioSlider;
        }
        else
        {
            audioSource.panStereo = 0;
        }
        audioSource.clip = audioClip;
        audioSource.pitch = pitch;
        audioSource.volume = _musicStemsManager.maxVolume * volume;
        audioSource.loop = false;
        play = false;
    }

    public void StartPlaying()
    {
        _audioSource.Play();
    }
}
