using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuState : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        if(GameStateContainer.Instance != null)
            GameStateContainer.Instance.inMenu = false;
	}

    public void EnterInMenuState()
    {
        if (GameStateContainer.Instance != null)
            GameStateContainer.Instance.inMenu = true;
    }

    public void ExitInMenuState()
    {
        if (GameStateContainer.Instance != null)
            GameStateContainer.Instance.inMenu = false;
    }

}
