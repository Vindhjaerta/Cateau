using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IInputField : IEventSystemHandler
{
    void OnInputReturn(string text);
}

public class InputController : MonoBehaviour {

    public InputField inputObject;
    public Text captionText;
    public Button button;

    private bool _active = false;
    private Image _image;

	// Use this for initialization
	void Awake () {
        _image = GetComponent<Image>();
	}

    public void Activate(string caption)
    {
        if(inputObject != null && caption != null && button != null && _image != null)
        {
            inputObject.gameObject.SetActive(true);
            captionText.gameObject.SetActive(true);
            captionText.text = caption;
            button.gameObject.SetActive(true);
            _image.enabled = true;
        }
    }

    public void DeActivate()
    {
        if (inputObject != null && captionText != null && button != null && _image != null)
        {
            inputObject.gameObject.SetActive(false);
            captionText.gameObject.SetActive(false);
            captionText.text = "";
            button.gameObject.SetActive(false);
            _image.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            ReturnText();
        }
    }


    public void ReturnText()
    {
        if (inputObject != null)
        {
            if (inputObject.text != null && inputObject.text != "")
            {
                string temp = inputObject.text;
                inputObject.text = "";
                ExecuteEvents.ExecuteHierarchy<IInputField>(gameObject, null, (handler, data) => handler.OnInputReturn(temp));
                gameObject.SetActive(false);
            }
            else
            {

            }
        }
        else
        {
            Debug.LogError("can't find inputfield");
        }

    }

}
