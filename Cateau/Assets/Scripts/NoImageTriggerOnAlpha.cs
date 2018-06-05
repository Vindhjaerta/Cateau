using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoImageTriggerOnAlpha : MonoBehaviour
{

    public Image imageToRemoveAlpha;

    [Range(0f,1f)]
    public float lowestTriggerableAlphaValue = 0.5f;

    // Use this for initialization
    void Start()
    {
        if (imageToRemoveAlpha != null)
        {
            imageToRemoveAlpha.alphaHitTestMinimumThreshold = lowestTriggerableAlphaValue;
        }
        else
        {
            imageToRemoveAlpha = GetComponent<Image>();
            if (imageToRemoveAlpha != null)
            {
                imageToRemoveAlpha.alphaHitTestMinimumThreshold = lowestTriggerableAlphaValue;
                //Debug.Log("The image wasn't set for NoImageTriggerOnAlpha, on gameobject: " + gameObject + " it has chosen the Image component on the gameobject");
            }
            else
            {
                //Debug.LogWarning("(NoImageTriggerOnAlpha) on gameobject: " + gameObject + " doesn't have an image component and Image on script has no reference");
            }
        }
    }
}
