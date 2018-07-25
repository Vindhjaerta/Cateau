using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleColor : MonoBehaviour
{
    public Color toggleColor;
    public bool isAutoturnPage;
    private Button _button;

    private bool _isToggle = false;

    private Image _image;

    private Color _imageBaseColor;
	// Use this for initialization
	//void Awake ()
 //   {
 //       _button = GetComponent<Button>();
 //       _button.onClick.AddListener(OwnToggleButton);
 //       _image = GetComponent<Image>();
 //       _imageBaseColor = _image.color;
 //       if(isAutoturnPage)
 //       {
 //           if(GameStateContainer.Instance != null)
 //           {
 //               if (GameStateContainer.Instance.autoTurnPage)
 //               {
 //                   ToggleButtonColor();
 //               }
 //           }
 //           else
 //           {
 //               Debug.Log("GameStateContainer doesn't exist: " + gameObject);
 //           }

 //       }
 //   }



 //   private void OwnToggleButton()
 //   {
 //       if (_isToggle == false)
 //       {
 //           _image.color = toggleColor;
 //           //Debug.Log("Set ToggleColor");
 //           _isToggle = true;
 //       }
 //       else
 //       {
 //           _image.color = _imageBaseColor;
 //           //Debug.Log("Reset ToggleColor");
 //           _isToggle = false;
 //       }
 //   }

 //   public void ToggleButtonColor()
 //   {
 //       if (_isToggle == false)
 //       {
 //           _image.color = toggleColor;
 //           //Debug.Log("Set ToggleColor");
 //           _isToggle = true;
 //       }
 //   }

 //   public void ResetTobbleColorButton()
 //   {
 //       {
 //           _image.color = _imageBaseColor;
 //           //Debug.Log("Reset ToggleColor");
 //           _isToggle = false;
 //       }
 //   }

}
