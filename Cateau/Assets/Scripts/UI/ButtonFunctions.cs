using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

    [SerializeField]
    private GameObject warningPanel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private GameObject galleryPanel;

	ButtonHolder bH;

	// Use this for initialization
	void Start () 
	{
		bH = FindObjectOfType<ButtonHolder>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OpenHomeWarning()
    {
        warningPanel.SetActive(true);
		bH.enterSecondButtonState = true;
		settingsPanel.SetActive (false);
        galleryPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
		bH.enterSecondButtonState = true;
		warningPanel.SetActive (false);
        infoPanel.SetActive(false);
        galleryPanel.SetActive(false);
    }

    public void OpenGalleryPanel()
    {
        settingsPanel.SetActive(false);
        bH.enterSecondButtonState = true;
        warningPanel.SetActive(false);
        infoPanel.SetActive(false);
        galleryPanel.SetActive(true);
    }

    public void OpenInfoPanel()
    {
        settingsPanel.SetActive(false);
        bH.enterSecondButtonState = true;
        warningPanel.SetActive(false);
        infoPanel.SetActive(true);
        galleryPanel.SetActive(false);
    }

    public void ReturnToPreviousChoice()
    {

    }


    public void CloseHomeWarning()
    {
        warningPanel.SetActive(false);
		bH.enterSecondButtonState = false;
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
		bH.enterSecondButtonState = false;
    }


    public void CloseInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
        bH.enterSecondButtonState = false;
    }

    public void ClosePhotoGalleryPanel()
    {
        galleryPanel.SetActive(false);
        bH.enterSecondButtonState = false;
    }

    public void ReturnToMainMenu()
    {
        if(GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.SaveGameState();
        }
        SceneManager.LoadScene("Menu");
    }

}
