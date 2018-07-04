using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="FadeEffects/Flash")]
public class FlashEffect : FadeEffect
{
    public bool ContinueRightAway;
    [Range(0f,100f)]
    public float stayDuration;
    [Range(0f, 100f)]
    public float appearingTime;
    [Range(0f, 100f)]
    public float disappearingTime;
    public Texture2D fadeTexture;

    public string soundContainerName;

    public override void UpdateEffect(FadeSceneTreeObject fadeSceneTreeObject, float deltaTime)
    {
        if (soundContainerName != null)
        {
            if (fadeSceneTreeObject.playedSoundEffect == true)
            {
                if (SoundEffectsManager.Instance != null && soundContainerName.Length > 0)
                {
                    SoundEffectsManager.Instance.PlaySoundFromContainer(soundContainerName);
                    fadeSceneTreeObject.playedSoundEffect = false;
                }
                else
                {
                    Debug.Log(fadeSceneTreeObject.gameObject + " tried to play a sound effect but couldn't find a SoundEffectsManager");
                }
            }
        }
        if (fadeSceneTreeObject.alpha < 1 && fadeSceneTreeObject.timer < stayDuration)
        {
            fadeSceneTreeObject.alpha += 1 * appearingTime * deltaTime;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeSceneTreeObject.alpha);
            GUI.depth = fadeSceneTreeObject.drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
        else if (fadeSceneTreeObject.timer < stayDuration)
        {
            if (ContinueRightAway)
            {
                fadeSceneTreeObject.continueToNextNode = true;
            }
            fadeSceneTreeObject.timer += deltaTime;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeSceneTreeObject.alpha);
            GUI.depth = fadeSceneTreeObject.drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
        else if (fadeSceneTreeObject.timer >= stayDuration)
        {
            fadeSceneTreeObject.alpha += -1 * disappearingTime * deltaTime;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeSceneTreeObject.alpha);
            GUI.depth = fadeSceneTreeObject.drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
            if (fadeSceneTreeObject.alpha <= 0)
                CleanUp(fadeSceneTreeObject);
        }
    }

    private void CleanUp(FadeSceneTreeObject fadeSceneTreeObject)
    {
        fadeSceneTreeObject.timer = 0;
        fadeSceneTreeObject.draw = false;
        fadeSceneTreeObject.continueToNextNode = true;
        fadeSceneTreeObject.alpha = 0;
    }
}
