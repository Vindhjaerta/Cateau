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
    private float currLerpTime = 0f;

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
            /*imageColor = text.color;
            imageColor.a = (Mathf.Sin(Time.time * fadeSpeedFloat) + 1.0f) / 2.0f;
            text.color = imageColor;*/

            currLerpTime += Time.deltaTime;
            if(currLerpTime > fadeSpeedFloat)
            {
                currLerpTime = fadeSpeedFloat;
            }

            float ratio = currLerpTime / fadeSpeedFloat;

            float alphaDiff = Mathf.Abs(imageColor.a - targetAlpha);
            if (alphaDiff > 0.0001f)
            {
                imageColor.a = Mathf.Lerp(imageColor.a, targetAlpha, ratio * ratio * ratio * (ratio * (6.0f * ratio - 15.0f) + 10.0f));
                //imageColor.a = Mathf.Lerp(imageColor.a, targetAlpha, fadeSpeedFloat * Time.deltaTime);
                text.color = imageColor;

                if (targetAlpha == maxAlphaValue)
                {
                    if (alphaDiff < 0.01f)
                    {
                        targetAlpha = lowestAlphaValue;
                        currLerpTime = 0;
                    }
                }
                else if (targetAlpha == lowestAlphaValue)
                {
                    if (alphaDiff < 0.01f)
                    {
                        targetAlpha = maxAlphaValue;
                        currLerpTime = 0;
                    }
                }
            }
        }
    }
}
