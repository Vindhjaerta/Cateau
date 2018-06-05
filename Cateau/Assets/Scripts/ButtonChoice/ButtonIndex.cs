using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIndex : MonoBehaviour
{
    private int _index;

    public void Index(int index)
    {
        _index = index;
    }

    public void SendIndex()
    {
        ExecuteEvents.ExecuteHierarchy<IButtonChoiceReciever>(gameObject, null, (x, y) => x.ButtonClicked(_index));
    }
}
