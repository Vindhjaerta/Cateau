using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    public AmbienceManager ambienceManager;

    public GameObject[] ambienceContainer;

    private void Start()
    {
        foreach (GameObject ambienceContainer in ambienceContainer)
        {
            if (ambienceContainer.activeSelf)
            {
                ambienceContainer.SetActive(false);
            }
        }
    }
    public void StartAudioController(AmbienceContainer[] ambienceContainers)
    {
        foreach (GameObject ambienceContainer in ambienceContainer)
        {
            ambienceContainer.SetActive(false);
        }
        foreach (AmbienceContainer ambienceContainer in ambienceContainers)
        {
            ambienceContainer.gameObject.SetActive(true);
        }
        ambienceManager.Restart();
    }
}
