using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Without panning.
 */
public class AmbienceManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxVolume = 1;

    [SerializeField]
    private int _maxSimultaneousSounds = 1;
    private int _numberOfCurrentClipsPlaying;

    [HideInInspector]
    public List<AmbienceSound> _playingAmbienceList = new List<AmbienceSound>();

    private List<AmbienceSound> _ambienceSoundQueue = new List<AmbienceSound>();
    //Remove and use profile istead
    private List<AmbienceContainer> _ambienceContainer = new List<AmbienceContainer>();


    private List<AmbienceProfile> _ambienceProfile = new List<AmbienceProfile>();

    private List<AudioSource> _audioSources = new List<AudioSource>();

    private static AmbienceManager _instance;

    public static AmbienceManager Instance
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

    public void Restart()
    {
        foreach (AmbienceSound ambienceSound in _playingAmbienceList)
        {
            ambienceSound.StopPlaying();
            ambienceSound.Initialize();
        }
        _playingAmbienceList.Clear();
        _ambienceSoundQueue.Clear();
        _ambienceContainer.Clear();
        Start();
    }

    // Use this for initialization
    void Start()
    {
        if (GameStateContainer.Instance != null)
        {
            maxVolume = (GameStateContainer.Instance.settings.ambienceVolume * GameStateContainer.Instance.settings.masterVolume);
        }
        else
        {
            Debug.LogError("No GamestateContainer was found. According to " + gameObject + ". But lets be honest, what does it know right?");
        }

        //Remove this and use profiles instead
        foreach (Transform child in transform)
        {
            if(child.GetComponent<AmbienceContainer>())
            {
                _ambienceContainer.Add(child.GetComponent<AmbienceContainer>());
            }
        }

        foreach (Transform child in transform)
        {
            if (child.GetComponent<AmbienceProfile>())
            {
                _ambienceProfile.Add(child.GetComponent<AmbienceProfile>());
            }
        }

        //Create audioSources
        for (int i = 0; i < _maxSimultaneousSounds; i++)
        {
            if (_audioSources.Count < _maxSimultaneousSounds)
            {
                _audioSources.Add(gameObject.AddComponent<AudioSource>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateContainer.Instance != null)
        {
            maxVolume = (GameStateContainer.Instance.settings.ambienceVolume * GameStateContainer.Instance.settings.masterVolume);
        }

        if (OpenAudioSources() && _ambienceSoundQueue.Count > 0)
        {
            AmbienceSound ambienceSound = GetFromQueue();

            ambienceSound.PlaySound(this, ReturnOpenAudioSource());
            //StartPlaying(ambienceSound);
            _ambienceSoundQueue.Remove(ambienceSound);
        }

       if (_playingAmbienceList.Count > 0)
        {
            for (int i = _playingAmbienceList.Count-1; i >= 0; i--)
            {
                _playingAmbienceList[i].UpdateSound(Time.deltaTime);
            }
        }

    }

    //Add to Queue
    public void AddToQueue(AmbienceSound ambienceSound)
    {
        if (OpenAudioSources())
        {
            ambienceSound.PlaySound(this, ReturnOpenAudioSource());
            //StartPlaying(ambienceSound);
        }
        else if(ambienceSound.addToQueue)
        {
            _ambienceSoundQueue.Add(ambienceSound);
        }
    }

    //Return the AmbienceSound with highest priority
    private AmbienceSound GetFromQueue()
    {
        int highestPriority = 100;
        int chosenClip = 0;
        for(int i = 0; i < _ambienceSoundQueue.Count-1; i++)
        {
            if (_ambienceSoundQueue[i].priority < highestPriority)
            {
                chosenClip = i;
                highestPriority = _ambienceSoundQueue[i].priority;
            }
        }
        return _ambienceSoundQueue[chosenClip];
    }

    //Return true if an AudioSource is available for use.
    private bool OpenAudioSources()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            if(!audioSource.isPlaying)
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


    public void StartPlayingAmbienceProfile(AmbienceProfile ambienceProfile)
    {
        foreach (AmbienceProfile profile in _ambienceProfile)
        {
            profile.Stop();
        }
        Restart();
        ambienceProfile.Play();
    }

    //Remove this

    //Stop playing a certain block
    public void StopPlayingContainer(string name)
    {
        foreach (AmbienceContainer ambienceContainer in _ambienceContainer)
        {
            if (ambienceContainer.containerName == name)
            {
                ambienceContainer.gameObject.SetActive(false);
            }
        }
    }

    //Start playing a certain block
    public void StartPlayingContainer(string name)
    {
        foreach (AmbienceContainer ambienceContainer in _ambienceContainer)
        {
            if (ambienceContainer.containerName == name)
            {
                ambienceContainer.gameObject.SetActive(true);
            }
        }
    }
}
