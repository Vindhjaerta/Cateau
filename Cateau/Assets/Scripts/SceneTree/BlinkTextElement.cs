using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkTextElement : SceneTreeObject
{

    public float fadeSpeedFloat = 1;
    [Range(0, 1)]
    public float lowestAlphaValue = 0f;
    [Range(0, 1)]
    public float maxAlphaValue = 1f;

    private bool blinking;
    private Text text;
    private float targetAlpha;
    private Color imageColor;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        imageColor = text.color;
        imageColor.a = 0f;
        text.color = imageColor;
        targetAlpha = maxAlphaValue;
        blinking = true;
        text.enabled = true;
    }

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (blinking)
        {
            imageColor = text.color;
            float alphaDiff = Mathf.Abs(imageColor.a - targetAlpha);
            if (alphaDiff > 0.0001f)
            {
                imageColor.a = Mathf.Lerp(imageColor.a, targetAlpha, fadeSpeedFloat * Time.deltaTime);
                text.color = imageColor;

                if (targetAlpha == maxAlphaValue)
                {
                    if (alphaDiff < 0.01f)
                    {
                        targetAlpha = lowestAlphaValue;
                    }
                }
                else if (targetAlpha == lowestAlphaValue)
                {
                    if (alphaDiff < 0.01f)
                    {
                        targetAlpha = maxAlphaValue;
                    }
                }
            }
        }
    }
}
