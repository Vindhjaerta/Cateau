using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     Author: Daniel Vindhjärta
     Last updated: 2018-01-26 
     Purpose:
     Allows for a quick way to quit the application
     
     Usage:
     Put on any object in the scene
     
     Dependencies:
     Unity
     
*/

public class QuitOnEsc : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
