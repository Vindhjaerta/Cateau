﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsController : MonoBehaviour
{

    private static SettingsController _instance;

    public static SettingsController Instance
    {
        get
        {
            return _instance;
        }
    }

    //Audio Sliders
    public Slider masterVolumeSlider;

    public Slider musicVolumeSlider;

    public Slider ambienceVolumeSlider;

    public Slider soundEffectsVolumeSlider;


    //Dropdowns
    public Dropdown resolutionDropdown;

    public Dropdown typingSpeedDropDown;


    //System Slider

    //public GameObject systemPanel;

    //public GameObject audioPanel;

    //public Button systemButton;

    private ConversationController _cC;

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

        if (GameController.Instance != null)
        {
            _cC = GameController.Instance.gameObject.GetComponentInChildren<ConversationController>();

        }
    }
    // Use this for initialization
    void Start ()
    {

        if (GameStateContainer.Instance != null)
        {
            typingSpeedDropDown.value = GameStateContainer.Instance.settings.typingSpeedIndex;

        }


        if (masterVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                masterVolumeSlider.value = GameStateContainer.Instance.settings.masterVolume;
            }
        }

        if (musicVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                musicVolumeSlider.value = GameStateContainer.Instance.settings.correctMusicVolume;
            }
        }

        if (ambienceVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                ambienceVolumeSlider.value = GameStateContainer.Instance.settings.correctAmbienceVolume;
            }
        }

        if (soundEffectsVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                soundEffectsVolumeSlider.value = GameStateContainer.Instance.settings.correctSFXVolume;
            }
        }

    }

    //public void HighlightButton()
    //{
    //    systemButton.Select();
    //}

    //Slider change functions
    public void MasterVolumeChange()
    {
        if (masterVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                GameStateContainer.Instance.settings.masterVolume = masterVolumeSlider.value;
            }
        }
    }

    public void MusicVolumeChange()
    {
        if(musicVolumeSlider != null)
        {
            if(GameStateContainer.Instance != null)
            {
                GameStateContainer.Instance.settings.correctMusicVolume = musicVolumeSlider.value;
            }
        }
    }

    public void AmbienceVolumeChange()
    {
        if (ambienceVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                GameStateContainer.Instance.settings.correctAmbienceVolume = ambienceVolumeSlider.value;
            }
        }
    }

    public void SoundEffectsVolumeChange()
    {
        if(soundEffectsVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                GameStateContainer.Instance.settings.correctSFXVolume = soundEffectsVolumeSlider.value;
            }
        }
    }


    //Dropdown change functions
    //public void ResolutionChange()
    //{
    //    if (resolutionDropdown != null && resolutions != null && GameStateContainer.Instance != null)
    //    {
    //        if (resolutions.Length > 0)
    //        {
    //            if (resolutionDropdown.value < resolutions.Length)
    //            {
    //                GameStateContainer.Instance.settings.resolutionIndex = resolutionDropdown.value;


    //            }
    //        }


    //    }
    //}


    public void TypingSpeedChange()
    {
        if(typingSpeedDropDown != null && GameStateContainer.Instance.typingSpeeds != null && GameStateContainer.Instance != null)
        {
            if(GameStateContainer.Instance.typingSpeeds.Length > 0)
            {
                if(typingSpeedDropDown.value < GameStateContainer.Instance.typingSpeeds.Length)
                {
                    GameStateContainer.Instance.settings.typingSpeedIndex = typingSpeedDropDown.value;
                    if(_cC != null)
                    {
                        _cC.dialoguePrint.standardPrintSpeed = GameStateContainer.Instance.typingSpeeds[GameStateContainer.Instance.settings.typingSpeedIndex];
                    }
                }
            }
            
             
        }
    }

    //public void OpenSystemPanel()
    //{
    //    if (systemPanel != null && audioPanel != null)
    //    {
    //        systemPanel.SetActive(true);
    //        audioPanel.SetActive(false);
    //    }

    //}

    //public void OpenAudioPanel()
    //{
    //    if (systemPanel != null && audioPanel != null)
    //    {
    //        audioPanel.SetActive(true);
    //        systemPanel.SetActive(false);
    //    }

    //}

}
