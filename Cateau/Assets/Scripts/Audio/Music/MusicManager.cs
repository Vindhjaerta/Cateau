using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    // Use this for initialization

    private static MusicManager _instance;

    public static MusicManager Instance
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
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameStateContainer.Instance != null)
        {
            audioSource.volume = (GameStateContainer.Instance.settings.musicVolume * GameStateContainer.Instance.settings.masterVolume);
        }
        else
        {
            Debug.LogError("No GamestateContainer was found. According to " + gameObject + ". But lets be honest, what does it know right?");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameStateContainer.Instance != null)
        {
            audioSource.volume = (GameStateContainer.Instance.settings.musicVolume * GameStateContainer.Instance.settings.masterVolume);
        }
    }
}
