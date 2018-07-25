using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPressedLoseFocus : MonoBehaviour {

    private EventSystem _events;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoseFocus);
        _events = EventSystem.current;
    }

    public void LoseFocus()
    {
        _events.SetSelectedGameObject(null);
    }
}
