using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSceneData : SceneTreeData
{
}

public class FadeSceneTreeObject : SceneTreeObject
{
    public FadeEffect fadeEffect;
    public int drawDepth = 20;
    [HideInInspector]
    public float alpha = 0;

    [HideInInspector]
    public int fadeDir = -1;
    [HideInInspector]
    public float timer;
    [HideInInspector]
    public bool draw;
    [HideInInspector]
    public bool continueToNextNode;
    [HideInInspector]
    public bool playedSoundEffect;

    [HideInInspector]
    public bool sentFadeOutMusicWithFade;
    [HideInInspector]
    public bool sentFadeInMusicWithFade;

    private FadeSceneData _fadeSceneData;

    protected override void Initialize()
    {
        continueToNextNode = false;
        draw = true;
        playedSoundEffect = true;
        _fadeSceneData = new FadeSceneData();
        _fadeSceneData.sender = this;
        _fadeSceneData.type = ESceneTreeType.FadeEffect;

        _data = _fadeSceneData;
    }

    public override void Continue(int nodeIndex)
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.buttonsClickable = false;
        }
        StartCoroutine(IEContinue());
    }

    IEnumerator IEContinue()
    {
        yield return new WaitUntil(() => continueToNextNode != false);
        if (GameController.Instance != null)
        {
            GameController.Instance.buttonsClickable = true;
        }
        Continue();
    }

    private void Awake()
    {
        continueToNextNode = false;
        draw = false;
    }
    // Use this for initialization
    void Start ()
    {
        if (fadeEffect == null)
        {
            Debug.LogError("There is no FadeEffect assigned to: " + gameObject);
        }
	}

    private void OnGUI()
    {
        if (draw)
        {
            fadeEffect.UpdateEffect(this, Time.deltaTime);
        }
    }
}
