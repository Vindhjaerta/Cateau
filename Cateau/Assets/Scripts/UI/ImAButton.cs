using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImAButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.imAButton = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.imAButton = false;
        }
    }
}
