using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StemsHolder
{
    [SerializeField]
    public string name;
    [SerializeField]
    public MusicAsset[] stems;
}

public class MusicStemsManager : MonoBehaviour
{
    [SerializeField]
    private bool _startPlayingAllClips;

    [SerializeField]
    private bool _onlyPlayClip0WithAllOthers;

    [SerializeField]
    private MusicAsset[] baseStems;
    [SerializeField]
    private MusicAsset[] happyStems;
    [SerializeField]
    private MusicAsset[] sadStems;
    // Use this for initialization

    [Range(0f,1f)]
    public float maxVolume = 0;

    private List<AudioSource> _audioSources = new List<AudioSource>();

    private bool fadeOut = false;
    private float fadeOutDuration = 0;
    private float startFadeVolume = 0;

    private float goalFadeVolume;
    private float fadeInDuration = 5;
    private bool fadeIn = true;

    float timer;
    #region Singelton
    private static MusicStemsManager _instance;

    public static MusicStemsManager Instance
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
    #endregion

    void Start()
    {
        if (GameStateContainer.Instance != null)
        {
            maxVolume = 0;
        }
        else
        {
            Debug.LogError("No GamestateContainer was found. According to " + gameObject + ". But lets be honest, what does it know right?");
        }

        SetUpMusicSources();
        RandomStems(_startPlayingAllClips);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        UpdateSoundVolume();
        for (int i = 0; i < _audioSources.Count; i++)
        {
            if (i < baseStems.Length)
            {
                _audioSources[i].volume = maxVolume * baseStems[i].volume;
            }
            else if (i >= baseStems.Length && i < _audioSources.Count - sadStems.Length)
            {
                if (happyStems != null)
                {
                    _audioSources[i].volume = maxVolume * happyStems[i - baseStems.Length].volume;
                }
            }
            else if (i <= baseStems.Length + happyStems.Length)
            {
                if (sadStems != null)
                {
                    _audioSources[i].volume = maxVolume * sadStems[i - (baseStems.Length + happyStems.Length)].volume;
                }
            }
        }

        bool sourcePlaying = false;
        for (int i = 0; i < baseStems.Length; i++)
        {
            if (_audioSources[i].isPlaying)
            {
                sourcePlaying = true;
                break;
            }
        }
        if (!sourcePlaying)
        {
            //Debug.Log(timer + "Source isn't playing");
            RandomStems(false);
        }
    }

    private void UpdateSoundVolume()
    {
        if (fadeOut)
        {
            float fractionToRemove = Time.deltaTime / fadeOutDuration;
            maxVolume -= startFadeVolume * fractionToRemove;
            //Debug.Log("Fraction: " + fractionToRemove);
            //Debug.Log(maxVolume);
            if (maxVolume <= 0)
            {
                //Debug.Log("FadeOutMusic: False");
                fadeOut = false;
            }
        }
        else if (fadeIn)
        {
            if (GameStateContainer.Instance != null)
            {
                if(maxVolume < (GameStateContainer.Instance.settings.musicVolume * GameStateContainer.Instance.settings.masterVolume))
                {
                    float fractionToAdd = Time.deltaTime / fadeInDuration;
                    maxVolume += 1 * fractionToAdd;
                    //Debug.Log("FractionToAdd: " + fractionToAdd);
                }
                else
                {
                    //Debug.Log("fadeIn False");
                    fadeIn = false;
                    //Debug.Log("MaxVolume was: " + maxVolume);
                }
            }
        }
        else if (GameStateContainer.Instance != null && !fadeOut && !fadeIn)
        {
            maxVolume = (GameStateContainer.Instance.settings.musicVolume * GameStateContainer.Instance.settings.masterVolume);
        }
    }

    private void SetUpMusicSources()
    {

        //Add AudioSources for all base stems
        for (int i = 0; i < baseStems.Length; i++)
        {
            _audioSources.Add((AudioSource)gameObject.AddComponent<AudioSource>());
        }
        for (int i = 0; i < happyStems.Length; i++)
        {
            _audioSources.Add((AudioSource)gameObject.AddComponent<AudioSource>());
        }

        for (int i = 0; i < sadStems.Length; i++)
        {
            _audioSources.Add((AudioSource)gameObject.AddComponent<AudioSource>());
        }

        for (int i = 0; i < baseStems.Length; i++)
        {
            if (baseStems[i] != null)
            {

                if (baseStems[i].audioClip != null)
                {
                    _audioSources[i].clip = baseStems[i].audioClip;
                }
                else
                {
                    Debug.LogWarning("MusicStemsManager tried to load a Audioclip from: " + baseStems[i].name + ". But it had none :(");
                }
                if (baseStems[i].staticPanAudio)
                {
                    _audioSources[i].panStereo = baseStems[i].staticPanAudioSlider;
                }
                //_audioSources[i].volume = 0;
                _audioSources[i].Pause();
            }
            else
            {
                Debug.LogError("MusicStemsManager tried to load a MusicAsset from BaseStems. But it had none :(");
            }
        }

        for (int i = 0; i < happyStems.Length; i++)
        {
            if (happyStems[i] != null)
            {
                if (happyStems[i].audioClip != null)
                {
                    _audioSources[i + baseStems.Length].clip = happyStems[i].audioClip;
                }
                else
                {
                    Debug.LogWarning("MusicStemsManager tried to load a Audioclip from: " + happyStems[i].name + ". But it had none :(");
                }
                if (happyStems[i].staticPanAudio)
                {
                    _audioSources[i + baseStems.Length].panStereo = happyStems[i].staticPanAudioSlider;
                }
                //_audioSources[i + baseStems.Length].volume = maxVolume * happyStems[i].volume;
                _audioSources[i + baseStems.Length].Pause();
            }
            else
            {
                Debug.LogError("MusicStemsManager tried to load a MusicAsset from HappyStems. But it had none :(");
            }
        }

        for (int i = 0; i < sadStems.Length; i++)
        {
            if (sadStems[i] != null)
            {
                if (sadStems[i].audioClip != null)
                {
                    _audioSources[+baseStems.Length + happyStems.Length].clip = sadStems[i].audioClip;
                }
                else
                {
                    Debug.LogWarning("MusicStemsManager tried to load a Audioclip from: " + sadStems[i].name + ". But it had none :(");
                }
                if (sadStems[i].staticPanAudio)
                {
                    _audioSources[i + baseStems.Length + happyStems.Length].panStereo = sadStems[i].staticPanAudioSlider;
                }
                //_audioSources[i + baseStems.Length + happyStems.Length].volume = maxVolume * sadStems[i].volume;
                _audioSources[i + baseStems.Length + happyStems.Length].Pause();
            }
            else
            {
                Debug.LogError("MusicStemsManager tried to load a MusicAsset from SadStems. But it had none :(");
            }
        }

    }

    private void RandomStems(bool startPlayingAllClips)
    {
        int amountOfClipsToPlay;
        if (startPlayingAllClips)
        {
            amountOfClipsToPlay = baseStems.Length;
        }
        else
        {
            amountOfClipsToPlay = Random.Range(0, baseStems.Length) + 1;
        }
        //Debug.Log(amountOfClipsToPlay + " clips will play  Time: " + timer);

        List<int> clipsToPlay = new List<int>();
        
        for (int i = 0; i < amountOfClipsToPlay; i++)
        {
            //Debug.Log("i: " + i);
            bool continueLoop = true;
            while (continueLoop)
            {
                bool openNumber = true;
                int randomNumber = Random.Range(0, baseStems.Length);

                if (amountOfClipsToPlay < baseStems.Length)
                {
                    while (randomNumber == 0 && _onlyPlayClip0WithAllOthers == true)
                    {
                        randomNumber = Random.Range(0, baseStems.Length);
                        //Debug.Log("Number 0 can't be played alone");
                    }
                }

                if (clipsToPlay.Count > 0)
                {
                    for (int j = 0; j < clipsToPlay.Count; j++)
                    {
                        if (randomNumber == clipsToPlay[j])
                        {
                            openNumber = false;
                            //Debug.Log("False: i: " + clipsToPlay[j] + "  Time: " + timer);
                        }
                    }
                }

                if (openNumber == true)
                {
                    clipsToPlay.Add(randomNumber);
                    continueLoop = false;
                    //Debug.Log("Number to add: " + randomNumber + "  Time: " + timer);
                }

            }
        }
        for(int i = 0; i < clipsToPlay.Count; i++)
        {
            //Debug.Log("Starting AudioSource: " + clipsToPlay[i] + " Time: " + timer);
           _audioSources[clipsToPlay[i]].Play();
        }
    }

    public void StartOtherStems(string name)
    {
        float currentTimeOfClip = 0;
        foreach (AudioSource audioSource in _audioSources)
        {
            if (audioSource.isPlaying)
            {
                //Debug.Log(audioSource.time);
                currentTimeOfClip = audioSource.time;
                break;
            }
        }

        if (name == "happyStems" || name == "HappyStems")
        {
            for (int i = baseStems.Length; i < _audioSources.Count; i++)
            {
                _audioSources[i].Stop();
            }
            if (happyStems != null)
            {
                for (int i = 0; i < happyStems.Length; i++)
                {
                    if (_audioSources[i + happyStems.Length] != null)
                    {
                        _audioSources[i + baseStems.Length].volume = 0;
                        _audioSources[i + baseStems.Length].time = currentTimeOfClip;
                        _audioSources[i + baseStems.Length].loop = true;
                        _audioSources[i + baseStems.Length].Play();
                    }
                }
            }
        }
        if (name == "sadStems" || name == "SadStems")
        {
            for (int i = baseStems.Length; i < _audioSources.Count; i++)
            {
                _audioSources[i].Stop();
            }
            if (sadStems != null)
            {
                for (int i = 0; i < sadStems.Length; i++)
                {
                    if (_audioSources[i + sadStems.Length] != null)
                    {
                        _audioSources[i + baseStems.Length + happyStems.Length].volume = 0;
                        _audioSources[i + baseStems.Length + happyStems.Length].time = currentTimeOfClip;
                        _audioSources[i + baseStems.Length + happyStems.Length].loop = true;
                        _audioSources[i + baseStems.Length + happyStems.Length].Play();
                    }
                }
            }
        }
    }

    public void StopOtherStems()
    {
        for (int i = baseStems.Length; i < _audioSources.Count; i++)
        {
            _audioSources[i].Stop();
        }
    }

    public void BeginFadeOut(float fadeOutTime)
    {
        //Debug.Log("Begin audioFadeOut");
        startFadeVolume = maxVolume;
        fadeOutDuration = fadeOutTime;
        fadeOut = true;
    }

    public void BeginFadeIn(float fadeInTime)
    {
        //Debug.Log("Begin audioFadeIn");
        maxVolume = 0;
        fadeInDuration = fadeInTime;
        fadeIn = true;
    }

}