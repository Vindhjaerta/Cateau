using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonBehaviour : MonoBehaviour {

    public static ButtonBehaviour ButtonBehaviourInstance { get; private set; }

    ButtonHolder bH;

    private ColorBlock cb;

    [SerializeField]
    private Color _disabledButtonColor;

    [SerializeField]
    private Color _activeButtonColor;

    public EButtonState secondButtonState;

    //public EFunctionality functionality;



   

    [SerializeField]
    private Button _button;

    public enum EButtonState
    {
        GreyedOut,
        Visible,
        Invisible
    };

   
    //public enum EFunctionality
    //{
    //    Return,
    //    Settings,
    //    Home,
    //    Autoplay,
    //    Skip
    //};

   

	// Use this for initialization
	void Start ()
    {
        //functionality = EFunctionality.Skip;

        bH = FindObjectOfType<ButtonHolder>();

	}
	

    // Update is called once per frame
    void Update ()
    {


        if (bH.enterSecondButtonState == true)
        {
            switch (secondButtonState)
            {
                case EButtonState.GreyedOut:

                    _button.enabled = false;
                    cb = _button.colors;
                    cb.normalColor = _disabledButtonColor;
                    _button.colors = cb;

                    break;



                case EButtonState.Invisible:

                    gameObject.SetActive(false);

                    break;

                case EButtonState.Visible:

                    gameObject.SetActive(true);
                    _button.enabled = true;
                    cb = _button.colors;
                    cb.normalColor = _activeButtonColor;
                    _button.colors = cb;



                    break;

            }
        }

        else if(bH.enterSecondButtonState == false)
        {
            gameObject.SetActive(true);
            _button.enabled = true;
            cb = _button.colors;
            cb.normalColor = _activeButtonColor;
            _button.colors = cb;
        }

        

        //switch (functionality)
        //{
        //    case EFunctionality.Skip:

        //        break;


        //    case EFunctionality.Settings:

        //        break;

        //    case EFunctionality.Autoplay:

        //        break;

        //    case EFunctionality.Home:

        //        break;

        //    case EFunctionality.Return:

        //        break;
        //}

        

	}
}
