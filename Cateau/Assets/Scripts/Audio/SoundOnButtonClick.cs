using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnButtonClick : MonoBehaviour
{
    public string ContainerName;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        if(SoundEffectsManager.Instance != null)
        {
            SoundEffectsManager.Instance.PlaySoundFromContainer(ContainerName);
        }
    }
}
