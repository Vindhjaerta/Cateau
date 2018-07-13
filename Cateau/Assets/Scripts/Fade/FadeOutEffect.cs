using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FadeEffects/FadeOut")]
public class FadeOutEffect : FadeEffect
{

    [Range(0.1f, 10f)]
    public float stayDuration;
    [Range(0f, 10f)]
    public float disappearingTime;
    public Texture2D fadeTexture;

    public string soundContainerName;

    public bool fadeOutMusicWithFade;

    public float soundFadeoutTime = 1f;

    public override void UpdateEffect(FadeSceneTreeObject fadeSceneTreeObject, float deltaTime)
    {
        //if (MusicStemsManager.Instance != null && fadeOutMusicWithFade && !fadeSceneTreeObject.sentFadeOutMusicWithFade)
        //{
        //    MusicStemsManager.Instance.BeginFadeOut(disappearingTime);
        //    fadeSceneTreeObject.sentFadeOutMusicWithFade = true;
        //}
        if (AudioController.Instance != null && fadeOutMusicWithFade && !fadeSceneTreeObject.sentFadeOutMusicWithFade)
        {
            AudioController.Instance.SceneFadeOutAudio(soundFadeoutTime);
            fadeSceneTreeObject.sentFadeOutMusicWithFade = true;
        }
        else if (AudioController.Instance != null && !fadeSceneTreeObject.sentFadeOutMusicWithFade)
        {
            AudioController.Instance.FadeOutAudio(soundFadeoutTime);
            fadeSceneTreeObject.sentFadeOutMusicWithFade = true;
        }
        if (soundContainerName != null)
        {
            if (fadeSceneTreeObject.playedSoundEffect == true && soundContainerName.Length > 0)
            {
                if (SoundEffectsManager.Instance != null)
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
        float fadeTimer =  deltaTime / disappearingTime;
        if (fadeSceneTreeObject.alpha < 1)
        {
            fadeSceneTreeObject.alpha += 1 * fadeTimer;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeSceneTreeObject.alpha);
            GUI.depth = fadeSceneTreeObject.drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
            if (fadeSceneTreeObject.alpha >= 1)
                ContinueToNextNode(fadeSceneTreeObject);
        }
        else if (fadeSceneTreeObject.alpha >= 1)
        {
            fadeSceneTreeObject.alpha = 1;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeSceneTreeObject.alpha);
            GUI.depth = fadeSceneTreeObject.drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
            fadeSceneTreeObject.timer += deltaTime;
            if(fadeSceneTreeObject.timer > stayDuration)
            {
                CleanUp(fadeSceneTreeObject);
            }
        }
    }

    private void ContinueToNextNode(FadeSceneTreeObject fadeSceneTreeObject)
    {
        fadeSceneTreeObject.continueToNextNode = true;
    }

    private void CleanUp(FadeSceneTreeObject fadeSceneTreeObject)
    {
        fadeSceneTreeObject.timer = 0;
        fadeSceneTreeObject.draw = false;
        fadeSceneTreeObject.alpha = 0;
        fadeSceneTreeObject.playedSoundEffect = true;
        fadeSceneTreeObject.sentFadeOutMusicWithFade = true;
    }
}
