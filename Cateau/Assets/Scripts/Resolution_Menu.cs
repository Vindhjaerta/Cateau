using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution_Menu : MonoBehaviour {

    public Dropdown resolutionDropdown;

    UnityEngine.Resolution[] resolutions;
    private List<Resolution> resList;

    private void Awake()
    {

        resolutions = Screen.resolutions;

        //Clear the existing options in the dropdown menu
        resolutionDropdown.ClearOptions();

        //Create strings of the resolutions
        List<string> options = new List<string>();
        resList = new List<Resolution>();

        Resolution curRes = new Resolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.currentResolution.refreshRate);
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == 60)
            {
                options.Add(resolutions[i].width + "x" + resolutions[i].height);
                resList.Add(new Resolution(resolutions[i].width, resolutions[i].height, resolutions[i].refreshRate));
            }
        }
        //Add the new options to the dropdown
        resolutionDropdown.AddOptions(options);

        //loop to find current

        for (int i = 0; i < resList.Count; i++)
        {
            if (curRes.width == resList[i].width && curRes.height == resList[i].height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }
	
    
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resList[resolutionIndex].width, resList[resolutionIndex].height, Screen.fullScreen);
        //save resolution in gamestate?
    }

}

[System.Serializable]
public class Resolution
{
    public int width;
    public int height;
    public int refreshRate;

    public Resolution(int width, int height, int refreshRate)
    {
        this.width = width;
        this.height = height;
        this.refreshRate = refreshRate;
    }
}