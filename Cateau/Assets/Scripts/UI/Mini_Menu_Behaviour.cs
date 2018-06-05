using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Menu_Behaviour : MonoBehaviour {

    public GameObject tutorialPanel;
    public GameObject settingsPanel;
    public GameObject homeWarningPanel;
    public GameObject pauseOverlay;

    ButtonHolder bH;

    // Use this for initialization
    void Start()
    {
        bH = FindObjectOfType<ButtonHolder>();

        tutorialPanel.SetActive(false);
        settingsPanel.SetActive(false);
        homeWarningPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenTutorialPanel()
    {
        tutorialPanel.SetActive(true);
        settingsPanel.SetActive(false);
        homeWarningPanel.SetActive(false);
        bH.enterSecondButtonState = true;
    }

    public void OpenSettingsPanel()
    {
        tutorialPanel.SetActive(false);
        settingsPanel.SetActive(true);
        homeWarningPanel.SetActive(false);
        bH.enterSecondButtonState = true;
    }

    public void OpenHomeWarningPanel()
    {
        tutorialPanel.SetActive(false);
        settingsPanel.SetActive(false);
        homeWarningPanel.SetActive(true);
        bH.enterSecondButtonState = true;
    }

    public void CloseTutorialPanel()
    {
        tutorialPanel.SetActive(false);
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void CloseHomeWarningPanel()
    {
        homeWarningPanel.SetActive(false);
    }

    public void Resume()
    {
        tutorialPanel.SetActive(false);
        settingsPanel.SetActive(false);
        homeWarningPanel.SetActive(false);
        bH.enterSecondButtonState = false;
        gameObject.SetActive(false);
    }

    public void OpenMiniMenu()
    {
        if (GameController.Instance != null)
        {
            if(GameController.Instance.buttonsClickable)
            {
                gameObject.SetActive(true);
                //bH.enterSecondButtonState = true;
            }
        }
    }

    public void OpenPauseOverlay()
    {
        if (GameController.Instance != null)
        {
            if(GameController.Instance.buttonsClickable)
                pauseOverlay.SetActive(true);
        }

    }

    public void ClosePauseOverlay()
    {
        pauseOverlay.SetActive(false);
    }
    
}
