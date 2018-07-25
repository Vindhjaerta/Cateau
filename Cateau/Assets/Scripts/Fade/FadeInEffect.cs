using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FadeEffects/FadeIn")]
public class FadeInEffect : FadeEffect
{
    public bool ContinueRightAway;
    [Range(0f, 10f)]
    public float stayDuration;
    [Range(0f, 10f)]
    public float disappearingTime;
    public Texture2D fadeTexture;
    public string soundContainerName;

    public bool fadeInMusicWithFade;

    public bool transitionFadeIn;

    public float soundFadeinTime = 3;

    public override void UpdateEffect(FadeSceneTreeObject fadeSceneTreeObject, float deltaTime)
    {
        if (ContinueRightAway)
            fadeSceneTreeObject.continueToNextNode = true;

        //if (MusicStemsManager.Instance != null && fadeInMusicWithFade && !fadeSceneTreeObject.sentFadeInMusicWithFade)
        //{
        //    MusicStemsManager.Instance.BeginFadeIn(disappearingTime);
        //    fadeSceneTreeObject.sentFadeInMusicWithFade = true;
        //}
        if (AudioController.Instance != null && fadeInMusicWithFade && !fadeSceneTreeObject.sentFadeInMusicWithFade && !transitionFadeIn)
        {
            AudioController.Instance.SceneFadeInAudio(soundFadeinTime);
            fadeSceneTreeObject.sentFadeInMusicWithFade = true;
        }
        else if (AudioController.Instance != null && transitionFadeIn && !fadeSceneTreeObject.sentFadeInMusicWithFade)
        {
            AudioController.Instance.TransitionFadeIn(soundFadeinTime);
            fadeSceneTreeObject.sentFadeInMusicWithFade = true;
        }
        else if (AudioController.Instance != null && !fadeSceneTreeObject.sentFadeInMusicWithFade)
        {
            AudioController.Instance.FadeInAudio(soundFadeinTime);
            fadeSceneTreeObject.sentFadeInMusicWithFade = true;
        }

        if (soundContainerName != null)
        {
            if (fadeSceneTreeObject.playedSoundEffect == true)
            {
                if (SoundEffectsManager.Instance != null)
                {
                    if (soundContainerName.Length > 0)
                    {
                        SoundEffectsManager.Instance.PlaySoundFromContainer(soundContainerName);
                        fadeSceneTreeObject.playedSoundEffect = false;
                    }
                }
                else
                {
                    Debug.Log(fadeSceneTreeObject.gameObject + " tried to play a sound effect but couldn't find a SoundEffectsManager");
                }
            }
        }
        if (fadeSceneTreeObject.timer < stayDuration)
        {
            fadeSceneTreeObject.alpha = 1;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeSceneTreeObject.alpha);
            GUI.depth = fadeSceneTreeObject.drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
            fadeSceneTreeObject.timer += deltaTime;
        }
        else if (fadeSceneTreeObject.timer > stayDuration)
        {
            float fadeTime = deltaTime / disappearingTime;
            fadeSceneTreeObject.alpha += -1 * fadeTime;
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
        fadeSceneTreeObject.continueToNextNode = true;
        fadeSceneTreeObject.draw = false;
        fadeSceneTreeObject.alpha = 0;
        fadeSceneTreeObject.sentFadeInMusicWithFade = false;
    }
}
