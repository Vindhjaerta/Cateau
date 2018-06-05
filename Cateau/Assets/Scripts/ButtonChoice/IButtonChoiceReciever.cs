using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IButtonChoiceReciever : IEventSystemHandler
{
    void ButtonClicked(int index);
}
