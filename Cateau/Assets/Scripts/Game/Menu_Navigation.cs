using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Navigation : MonoBehaviour {

    [SerializeField]
    private GameObject mainMenuHolder;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject creditsPanel;

    public string mainString = "Main";


    // Use this for initialization
    void Start () {
		if(GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.Initialize();
            if (!GameStateContainer.Instance.LoadGameState())
            {
                GameStateContainer.Instance.ClearGallery();
            }
        }
	}
	
    public void ContinueGame(string sceneToLoad)
    {
        if (GameStateContainer.Instance != null)
        {
            if (GameStateContainer.Instance.savepointIndex != 0)
            {
                if (MusicManager.Instance != null)
                {
                    Destroy(MusicManager.Instance.gameObject);
                }


                GameStateContainer.Instance.useSavepointContinuePath = true;
                GameStateContainer.Instance.scene = sceneToLoad;

                SceneManager.LoadScene(sceneToLoad);
            }
        }

    }

    public void NewGame(string sceneToLoad)
    {
        if (MusicManager.Instance != null)
        {
            Destroy(MusicManager.Instance.gameObject);
        }

        if(GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.Initialize();
            GameStateContainer.Instance.scene = sceneToLoad;
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OpenSettingsPanel()
    {
        mainMenuHolder.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OpenCreditsPanel()
    {
        mainMenuHolder.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OpenMainMenuHolder()
    {
        mainMenuHolder.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        if(GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.SaveGameState();
        }
        Application.Quit();
    }
}
