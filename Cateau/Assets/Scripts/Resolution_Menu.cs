using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution_Menu : MonoBehaviour {

    public Dropdown resolutionDropdown;

    UnityEngine.Resolution[] resolutions;


	// Use this for initialization
	void Start ()
    {
        resolutions = Screen.resolutions;

        //Clear the existing options in the dropdown menu
        resolutionDropdown.ClearOptions();

        //Create strings of the resolutions
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //Add the new options to the dropdown
        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
	}
	
    
    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
