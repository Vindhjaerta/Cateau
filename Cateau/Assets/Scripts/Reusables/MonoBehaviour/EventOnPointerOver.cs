using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventOnPointerOver : MonoBehaviour, IPointerEnterHandler {

    public UnityEvent OnPointerOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerOver.Invoke();
    }

    private void OnMouseEnter()
    {
        OnPointerOver.Invoke();
    }
}
