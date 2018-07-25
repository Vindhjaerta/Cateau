using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public FloatVariable baseFadeInAudioTime;
    public FloatVariable baseFadeOutAudioTime;

    private float startFadeInTime = 3;
    private float sceneFadeOutTime = 1;    

    private float targetFadeMasterVolym = 0;

    private float targetFadeMusicVolym = 0;

    private float targetFadeAmbienceVolym = 0;

    private float targetFadeSFXVolym = 0;

    private bool fadingMusic = false;

    private bool fadingAmbience = false;

    private bool fadingSFX = false;

    private bool sceneFadeOut = false;
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
            //Music
            if (GameStateContainer.Instance.settings.musicVolume >= targetFadeMusicVolym)
            {
                GameStateContainer.Instance.settings.musicVolume = (GameStateContainer.Instance.settings.musicVolume - Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.musicVolume < targetFadeMusicVolym)
                {
                    GameStateContainer.Instance.settings.musicVolume = targetFadeMusicVolym;
                    fadingMusic = false;
                }
            }
            else if (GameStateContainer.Instance.settings.musicVolume <= targetFadeMusicVolym)
            {
                GameStateContainer.Instance.settings.musicVolume = (GameStateContainer.Instance.settings.musicVolume + Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.musicVolume > targetFadeMusicVolym)
                { 
                    GameStateContainer.Instance.settings.musicVolume = targetFadeMusicVolym;
                    fadingMusic = false;
                }
            }
            if (fadingMusic == false && sceneFadeOut == false)
            {
                targetFadeMusicVolym = GameStateContainer.Instance.settings.correctMusicVolume;
                GameStateContainer.Instance.settings.musicVolume = GameStateContainer.Instance.settings.correctMusicVolume;
            }

            //Ambience
            if (GameStateContainer.Instance.settings.ambienceVolume >= targetFadeAmbienceVolym)
            {
                GameStateContainer.Instance.settings.ambienceVolume = (GameStateContainer.Instance.settings.ambienceVolume - Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.ambienceVolume < targetFadeAmbienceVolym)
                {
                    GameStateContainer.Instance.settings.ambienceVolume = targetFadeAmbienceVolym;
                    fadingAmbience = false;
                }
            }
            else if (GameStateContainer.Instance.settings.ambienceVolume <= targetFadeAmbienceVolym)
            {
                GameStateContainer.Instance.settings.ambienceVolume = (GameStateContainer.Instance.settings.ambienceVolume + Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.ambienceVolume > targetFadeAmbienceVolym)
                {
                    GameStateContainer.Instance.settings.ambienceVolume = targetFadeAmbienceVolym;
                    fadingAmbience = false;
                }
            }
            if (fadingAmbience == false && sceneFadeOut == false)
            {
                targetFadeAmbienceVolym = GameStateContainer.Instance.settings.correctAmbienceVolume;
                GameStateContainer.Instance.settings.ambienceVolume = GameStateContainer.Instance.settings.correctAmbienceVolume;
            }

            //SFX
            if (GameStateContainer.Instance.settings.sfxVolume >= targetFadeSFXVolym)
            {
                GameStateContainer.Instance.settings.sfxVolume = (GameStateContainer.Instance.settings.sfxVolume - Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.sfxVolume < targetFadeSFXVolym)
                {
                    GameStateContainer.Instance.settings.sfxVolume = targetFadeSFXVolym;
                    fadingSFX = false;
                }
            }
            else if (GameStateContainer.Instance.settings.sfxVolume <= targetFadeSFXVolym)
            {
                GameStateContainer.Instance.settings.sfxVolume = (GameStateContainer.Instance.settings.sfxVolume + Time.deltaTime / fadeTime);
                if (GameStateContainer.Instance.settings.sfxVolume > targetFadeSFXVolym)
                {
                    GameStateContainer.Instance.settings.sfxVolume = targetFadeSFXVolym;
                    fadingSFX = false;
                }
            }
            if (fadingSFX == false && sceneFadeOut == false)
            {
                targetFadeSFXVolym = GameStateContainer.Instance.settings.correctSFXVolume;
                GameStateContainer.Instance.settings.sfxVolume = GameStateContainer.Instance.settings.correctSFXVolume;
            }
        }
        else
        {
           Debug.LogWarning ("AudioController couldn't find the GameStateContainer");
        }
    }

    public void FadeOutAudio(float fadeTime)
    {
        sceneFadeOut = true;

        targetFadeAmbienceVolym = 0;
        fadingAmbience = true;

        targetFadeSFXVolym = 0;
        fadingSFX = true;

        this.fadeTime = fadeTime;
    }

    public void FadeInAudio(float fadeTime)
    {
        sceneFadeOut = false;

        fadingAmbience = true;
        targetFadeAmbienceVolym = GameStateContainer.Instance.settings.correctAmbienceVolume;
        GameStateContainer.Instance.settings.ambienceVolume = 0;

        fadingSFX = true;
        targetFadeSFXVolym = GameStateContainer.Instance.settings.correctSFXVolume;
        GameStateContainer.Instance.settings.sfxVolume = 0;
    }

    public void SceneFadeOutAudio(float fadeTime)
    {
        sceneFadeOut = true;

        targetFadeMusicVolym = 0;
        fadingMusic = true;

        targetFadeAmbienceVolym = 0;
        fadingAmbience = true;

        targetFadeSFXVolym = 0;
        fadingSFX = true;

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
            sceneFadeOut = false;

            fadingMusic = true;
            targetFadeMusicVolym = GameStateContainer.Instance.settings.correctMusicVolume;
            GameStateContainer.Instance.settings.musicVolume = 0;

            fadingAmbience = true;
            targetFadeAmbienceVolym = GameStateContainer.Instance.settings.correctAmbienceVolume;
            GameStateContainer.Instance.settings.ambienceVolume = 0;

            fadingSFX = true;
            targetFadeSFXVolym = GameStateContainer.Instance.settings.correctSFXVolume;
            GameStateContainer.Instance.settings.sfxVolume = 0;
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

    public void TransitionFadeIn(float fadeTime)
    {

        fadingSFX = true;
        targetFadeSFXVolym = GameStateContainer.Instance.settings.correctSFXVolume;
        GameStateContainer.Instance.settings.sfxVolume = 0;

        fadingAmbience = true;
        targetFadeAmbienceVolym = GameStateContainer.Instance.settings.correctAmbienceVolume;
        GameStateContainer.Instance.settings.ambienceVolume = 0;

        this.fadeTime = fadeTime;
    }

    private void OnDisable()
    {
        
    }

}
