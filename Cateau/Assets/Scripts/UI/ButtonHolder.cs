using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHolder : MonoBehaviour {

   
    public bool enterSecondButtonState;

    
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {


	}


    public void EnterSecondButtonState()
    {
        enterSecondButtonState = true;
    }

    public void ExitSecondButtonState()
    {
        enterSecondButtonState = false;
    }
}
