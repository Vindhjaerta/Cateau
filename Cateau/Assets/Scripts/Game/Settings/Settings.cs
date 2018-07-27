using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class SettingsContainer
{
    //Audio Settings
    
    [Range(0, 1)]
    public float musicVolume = 0.5f;

    [HideInInspector]
    public float correctMusicVolume = 0.5f;

    [Range(0, 1)]
    public float sfxVolume = 0.5f;

    [HideInInspector]
    public float correctSFXVolume = 0.5f;

    [Range(0, 1)]
    public float ambienceVolume = 0.5f;

    [HideInInspector]
    public float correctAmbienceVolume = 0.5f;

    [Range(0, 1)]
    public float masterVolume = 0.5f;

    //[HideInInspector]
    //public float correctMasterVolume = 0.5f;

    //System
    public int typingSpeedIndex;

}