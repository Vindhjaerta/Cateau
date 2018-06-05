using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxVolume = 1;

    [SerializeField]
    private int _maxSimultaneousSounds = 1;
    private int _numberOfCurrentClipsPlaying;


    private List<SoundEffectsContainer> _soundEffectContainer = new List<SoundEffectsContainer>();

    private List<AudioSource> _audioSources = new List<AudioSource>();

    [HideInInspector]
    public List<SoundEffect> _playingSoundEffectList = new List<SoundEffect>();

    private static SoundEffectsManager _instance;

    public static SoundEffectsManager Instance
    {
        get
        {
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (GameStateContainer.Instance != null)
        {
            maxVolume = (GameStateContainer.Instance.settings.sfxVolume * GameStateContainer.Instance.settings.masterVolume);
        }
        else
        {
            Debug.LogError("No GamestateContainer was found. According to " + gameObject + ". But lets be honest, what does it know right?");
        }

        foreach (Transform child in transform)
        {
            if (child.GetComponent<SoundEffectsContainer>())
            {
                _soundEffectContainer.Add(child.GetComponent<SoundEffectsContainer>());
            }
        }
        //Create audioSources
        for (int i = 0; i < _maxSimultaneousSounds; i++)
        {
            _audioSources.Add(gameObject.AddComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateContainer.Instance != null)
        {
            maxVolume = GameStateContainer.Instance.settings.sfxVolume * GameStateContainer.Instance.settings.masterVolume;
        }


        if (_playingSoundEffectList.Count > 0)
        {
            for (int i = _playingSoundEffectList.Count - 1; i >= 0; i--)
            {
                _playingSoundEffectList[i].UpdateSound(Time.deltaTime);
            }
        }

    }

    public void PlaySoundFromContainer(string name)
    {
        bool foundName = false;
        //Debug.Log("PlaySoundFromContainer on: " + gameObject + " called on string name: " + name);
        if (_soundEffectContainer != null)
        {
            foreach (SoundEffectsContainer soundEffectsContainer in _soundEffectContainer)
            {
                //Debug.Log(soundEffectsContainer._containerName + " was the name of a container");
                if (soundEffectsContainer.containerName == name)
                {
                    //if active
                    SoundEffect soundEffect = soundEffectsContainer.SendClip();
                    if (soundEffect != null)
                    {
                        PlaySound(soundEffect);
                    }
                    foundName = true;
                    return;
                }
            }
        }
        if (!foundName)
        {
            Debug.LogWarning("OH NO!, the name sent to the container doesn't match any of the container names, the name sent was: " + name + ".");
        }
    }

    private void PlaySound(SoundEffect soundEffect)
    {
        if (soundEffect != null)
        {
            if (OpenAudioSources())
            {
                soundEffect.PlaySound(this, ReturnOpenAudioSource());
            }
        }
    }

    private bool OpenAudioSources()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            if (!audioSource.isPlaying)
            {
                return true;
            }
        }
        return false;
    }

    private AudioSource ReturnOpenAudioSource()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }
        return null;
    }
}
