using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterInMenuState : MonoBehaviour
{

    private Button _button;

	// Use this for initialization
	void Start ()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(InMenuState);
	}

    private void InMenuState()
    {
        if (GameStateContainer.Instance != null)
            GameStateContainer.Instance.inMenu = true;
    }

}
