using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini_Menu_Controller : MonoBehaviour {

    public GameObject infoPanel;

    public GameObject photoGalleryPanel;

    public GameObject settingsPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenInfoPanel()
    {
        infoPanel.gameObject.SetActive(true);
        photoGalleryPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OpenPhotoGalleryPanel()
    {
        photoGalleryPanel.gameObject.SetActive(true);
        infoPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
    }

    public void CloseInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
    }

    public void ClosePhotoGalleryPanel()
    {
        photoGalleryPanel.SetActive(false);
    }
}
