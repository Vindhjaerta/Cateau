using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SoundOnMouseOver : MonoBehaviour, IPointerEnterHandler
{
    public string ContainerName;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SoundEffectsManager.Instance != null)
        {
            SoundEffectsManager.Instance.PlaySoundFromContainer(ContainerName);
        }
    }
}
