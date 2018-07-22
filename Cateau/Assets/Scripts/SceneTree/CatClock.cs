using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatClock : SceneTreeObject
{
    private Animator animator;
    private Image image;

    public GameObject clock;
    public List<RotateRectTransform> objectsToRotateList;
    public override void Continue(int nodeIndex)
    {

    }

    protected override void Initialize()
    {
        clock.SetActive(true);
        image.enabled = true;
        if (objectsToRotateList != null)
        {
            foreach (RotateRectTransform rotateRectTransform in objectsToRotateList)
            {
                rotateRectTransform.StartRotate();
            }
        }
        animator.SetTrigger("play");
        Continue();
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
        image.enabled = false;
        clock.SetActive(false);
    }

    public void SendTick()
    {
        if (SoundEffectsManager.Instance != null)
        {
            SoundEffectsManager.Instance.PlaySoundFromContainer("Tick");
        }
    }

    public void SendTack()
    {
        if (SoundEffectsManager.Instance != null)
        {
            SoundEffectsManager.Instance.PlaySoundFromContainer("Tack");
        }
    }
}
