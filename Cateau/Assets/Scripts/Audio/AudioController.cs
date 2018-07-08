using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public FloatVariable baseFadeInAudioTime;
    public FloatVariable baseFadeOutAudioTime;

    private float startFadeInTime = 3;
    private float sceneFadeOutTime = 1;    

    private float targetFadeVolym = 0;
    private bool fading;
    private bool sceneFadeOut;
    private float fadeTime = 1;


    private static AudioController _instance;

    public static AudioController Instance
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
        if (baseFadeInAudioTime != null && baseFadeInAudioTime.value > 0)
        {
            startFadeInTime = baseFadeInAudioTime.value;
        }
        if (baseFadeOutAudioTime != null && baseFadeOutAudioTime.value >0)
        {
            sceneFadeOutTime = baseFadeOutAudioTime.value;
        }
        if (GameStateContainer.Instance != null)
        {
            SceneFadeInAudio(startFadeInTime);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameStateContainer.Instance != null)
        {
            if (GameStateContainer.Instance.settings.masterVolume > targetFadeVolym)
            {
                GameStateContainer.Instance.settings.masterVolume = (GameStateContainer.Instance.settings.masterVolume - Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.masterVolume < targetFadeVolym)
                {
                    GameStateContainer.Instance.settings.masterVolume = targetFadeVolym;
                    fading = false;
                }
            }
            else if (GameStateContainer.Instance.settings.masterVolume < targetFadeVolym)
            {
                GameStateContainer.Instance.settings.masterVolume = (GameStateContainer.Instance.settings.masterVolume + Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.masterVolume > targetFadeVolym)
                { 
                    GameStateContainer.Instance.settings.masterVolume = targetFadeVolym;
                    fading = false;
                }
            }
            if (fading == false && sceneFadeOut == false)
            {
                targetFadeVolym = GameStateContainer.Instance.settings.correctMasterVolume;
            }
        }
        else
        {
           Debug.LogWarning ("AudioController couldn't find the GameStateContainer");
        }
    }

    public void FadeOutAudio(float fadeTime)
    {
        targetFadeVolym = 0;
        fading = true;
        if (targetFadeVolym >= 0 && targetFadeVolym <= 1)
        {
            //this.targetFadeVolym = targetFadeVolym;
        }
        this.fadeTime = fadeTime;
    }

    public void SceneFadeOutAudio(float fadeTime)
    {
        targetFadeVolym = 0;
        fading = true;
        sceneFadeOut = true;
        if (fadeTime <= 0)
        {
            this.fadeTime = sceneFadeOutTime;
        }
        else
        {
            this.fadeTime = fadeTime;
        }
    }

    public void SceneFadeInAudio(float fadeTime)
    {
        if (GameStateContainer.Instance != null)
        {
            fading = true;
            targetFadeVolym = GameStateContainer.Instance.settings.correctMasterVolume;
            GameStateContainer.Instance.settings.masterVolume = 0;
        }
        if (fadeTime <= 0)
        {
            this.fadeTime = startFadeInTime;
        }
        else
        {
            this.fadeTime = fadeTime;
        }
    }

    private void OnDisable()
    {
        
    }

}
