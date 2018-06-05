using System.Collections;
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

    //Audio Sliders
    public Slider masterVolumeSlider;

    public Slider musicVolumeSlider;

    public Slider ambienceVolumeSlider;

    public Slider soundEffectsVolumeSlider;





    //Dropdowns
    public Dropdown resolutionDropdown;

    public Dropdown fontSizeDropdown;

    public Dropdown typingSpeedDropDown;


    //System Slider
    public Slider brightnessSlider;

    public GameObject systemPanel;

    public GameObject audioPanel;

    public Button systemButton;

    public Resolution[] resolutions;

    [SerializeField]
    private Text conversationText;

	// Use this for initialization
	void Start ()
    {

        if (GameStateContainer.Instance != null)
        {
            typingSpeedDropDown.value = GameStateContainer.Instance.settings.typingSpeedIndex;

            fontSizeDropdown.value = GameStateContainer.Instance.settings.fontSizeIndex;
            if (conversationText != null)
            {
                conversationText.fontSize = GameStateContainer.Instance.fontSizes[GameStateContainer.Instance.settings.fontSizeIndex].value;
            }
            else
            {
                Debug.LogWarning("Conversation Text hasn't been set in the inspector. " + gameObject);
            }
        }

        
        
           
        

        //if(GameStateContainer.Instance != null)
        //{
        //    typingSpeedDropDown.value = GameStateContainer.Instance.settings.typingSpeedIndex;
        //}

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
                musicVolumeSlider.value = GameStateContainer.Instance.settings.musicVolume;
            }
        }

        if (ambienceVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                ambienceVolumeSlider.value = GameStateContainer.Instance.settings.ambienceVolume;
            }
        }

        if (soundEffectsVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                soundEffectsVolumeSlider.value = GameStateContainer.Instance.settings.sfxVolume;
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
	    
        
	}


    public void HighlightButton()
    {
        systemButton.Select();
    }

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
                GameStateContainer.Instance.settings.musicVolume = musicVolumeSlider.value;
            }
        }
    }

    public void AmbienceVolumeChange()
    {
        if (ambienceVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                GameStateContainer.Instance.settings.ambienceVolume = ambienceVolumeSlider.value;
            }
        }
    }

    public void SoundEffectsVolumeChange()
    {
        if(soundEffectsVolumeSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                GameStateContainer.Instance.settings.sfxVolume = soundEffectsVolumeSlider.value;
            }
        }
    }

    public void BrightnessChange()
    {
        if (brightnessSlider != null)
        {
            if (GameStateContainer.Instance != null)
            {
                GameStateContainer.Instance.settings.brightness = brightnessSlider.value;
            }
        }
    }


    //Dropdown change functions
    public void ResolutionChange()
    {
        if (resolutionDropdown != null && resolutions != null && GameStateContainer.Instance != null)
        {
            if (resolutions.Length > 0)
            {
                if (resolutionDropdown.value < resolutions.Length)
                {
                    GameStateContainer.Instance.settings.resolutionIndex = resolutionDropdown.value;


                }
            }


        }
    }


    public void FontSizeChange()
    {
        if (fontSizeDropdown != null && GameStateContainer.Instance.fontSizes != null && GameStateContainer.Instance != null)
        {
            if (GameStateContainer.Instance.fontSizes.Length > 0)
            {
                if (fontSizeDropdown.value < GameStateContainer.Instance.fontSizes.Length)
                {
                    GameStateContainer.Instance.settings.fontSizeIndex = fontSizeDropdown.value;

                    if (conversationText != null)
                    {
                        conversationText.fontSize = GameStateContainer.Instance.fontSizes[GameStateContainer.Instance.settings.fontSizeIndex].value;
                    }
                    else
                    {
                        Debug.LogWarning("Conversation Text hasn't been set in the inspector. " + gameObject);
                    }
                }
            }


        }
    }


    public void TypingSpeedChange()
    {
        if(typingSpeedDropDown != null && GameStateContainer.Instance.typingSpeeds != null && GameStateContainer.Instance != null)
        {
            if(GameStateContainer.Instance.typingSpeeds.Length > 0)
            {
                if(typingSpeedDropDown.value < GameStateContainer.Instance.typingSpeeds.Length)
                {
                    GameStateContainer.Instance.settings.typingSpeedIndex = typingSpeedDropDown.value;
                    ConversationController cC = FindObjectOfType<ConversationController>();
                    if(cC != null)
                    {
                        cC.dialoguePrint.standardPrintSpeed = GameStateContainer.Instance.typingSpeeds[GameStateContainer.Instance.settings.typingSpeedIndex];
                    }
                }
            }
            
             
        }
    }

   

    public void OpenSystemPanel()
    {
        if (systemPanel != null && audioPanel != null)
        {
            systemPanel.SetActive(true);
            audioPanel.SetActive(false);
        }

    }

    public void OpenAudioPanel()
    {
        if (systemPanel != null && audioPanel != null)
        {
            audioPanel.SetActive(true);
            systemPanel.SetActive(false);
        }

    }

}
