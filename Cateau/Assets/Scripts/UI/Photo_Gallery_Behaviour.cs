using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo_Gallery_Behaviour : MonoBehaviour {

    public Button[] photos;

    public Photos photoArchive;

    public GameObject bigPhoto;

    public Photos bigPhotoArchive;

    bool photoIsActive;


	// Use this for initialization
	void Start ()
    {
        photoIsActive = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(photoIsActive == true && Input.GetMouseButtonDown(0))
        {
            photoIsActive = false;
            bigPhoto.SetActive(false);
        }
	}


    //Anropa vid öppning av panelen
    public void UpdatePhotos()
    {
        for(int i = 0; i < GameStateContainer.Instance.activePhotos.Length; i++)
        {
           if(GameStateContainer.Instance.activePhotos[i] == true)
            {
                photos[i].GetComponent<Image>().sprite = photoArchive.photoArchive[i];
                photos[i].enabled = true;
            }
            

            if(GameStateContainer.Instance.activePhotos[i] == false)
            {
                photos[i].enabled = false;
            }
        }
    }


    public void DisplayBigPhoto(int photoIndex)
    {
        bigPhoto.GetComponent<Image>().sprite = bigPhotoArchive.photoArchive[photoIndex];
        bigPhoto.SetActive(true);
        photoIsActive = true;
    }


}
