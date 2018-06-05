using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteCatScript : MonoBehaviour, ICatBehaviour
{
    [System.Serializable]
    public class SpriteCatMoods
    {
        [HideInInspector]
        public Sprite reactSprite;
        [HideInInspector]
        public int reactAffinityThreshold;
        [HideInInspector]
        public Sprite moodSprite;
        public int moodThreshold;
    }
    [System.Serializable]
    public class CatTagName
    {
        public string catName = "";
        public StringReference catTag;
    }

    [System.Serializable]
    public class Affinity
    {
        public int baseAffinity;
        public int currentAffinity;
    }

    [SerializeField]
    private StringReference CAT_HAPPY;
    [SerializeField]
    private StringReference CAT_NEUTRAL;
    [SerializeField]
    private StringReference CAT_SAD;


    [SerializeField]
    private CatTagName _catTagName;
    [SerializeField]
    private Affinity _affinity;

    [SerializeField]
    private Vector2 _blinkInterval;
    private float _blinkTimer;
    private float blinkAtMoment = 0;

    [SerializeField]
    private SpriteCatMoods _sadCat;
    [SerializeField]
    private SpriteCatMoods _neutralCat;
    [SerializeField]
    private SpriteCatMoods _happyCat;

    private Animator animator;

    public float _reactionDuration = 3.0f;

    private float _timer;

    private bool _react;

    private Image _image;

    void Start ()
    {
        _image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        if (_blinkInterval == null)
        {
            Debug.LogError("BlinkInterval on: " + gameObject + " doesn't have a value");
        }
        //_affinity.currentAffinity = _affinity.baseAffinity;
        StartCatMood();
    }

    private void StartCatMood()
    {
        if (_affinity.currentAffinity <= _sadCat.moodThreshold)
        {
            //_image.sprite = _sadCat.moodSprite;
        }
        else if (_affinity.currentAffinity >= _happyCat.moodThreshold)
        {
            //_image.sprite = _happyCat.moodSprite;
        }
        else
        {
            //_image.sprite = _neutralCat.moodSprite;
        }
    }

	void Update ()
    {
        _blinkTimer += Time.deltaTime;

        animator.SetInteger("affinity", _affinity.currentAffinity);
        if (_react)
        {
            _timer += Time.deltaTime;
            if (_timer > _reactionDuration)
            {
                _timer = 0;
                if (_affinity.currentAffinity <= _sadCat.moodThreshold)
                {
                    //_image.sprite = _sadCat.moodSprite;
                }
                else if (_affinity.currentAffinity >= _happyCat.moodThreshold)
                {
                    //_image.sprite = _happyCat.moodSprite;
                }
                else
                {
                    //_image.sprite = _neutralCat.moodSprite;
                }
            }
            //Make sure affinity can't go below 0.
            if (_affinity.currentAffinity < 0)
            {
                _affinity.currentAffinity = 0;
            }
        }
        if (_blinkTimer > _blinkInterval.x)
        {
            if(blinkAtMoment != 0)
            {
                if(_blinkTimer > blinkAtMoment)
                {
                    animator.SetTrigger("timeToBlink");
                    blinkAtMoment = 0;
                    _blinkTimer = 0;
                }
            }
            else
            {
                blinkAtMoment = Random.Range(_blinkInterval.x, _blinkInterval.y);
                //Debug.Log(blinkAtMoment);
            }
        }
    }

    public void ReceiveAffinityFromButton(ButtonData catReactionData)
    {
        _react = true;
        if (catReactionData.react)
        {
            if (catReactionData.relativeReaction)
            {
                if (catReactionData.reactValue <= _sadCat.reactAffinityThreshold)
                {
                    if (_affinity.currentAffinity >= _happyCat.moodThreshold)
                    {
                        //_image.sprite = _neutralCat.reactSprite;
                        animator.SetTrigger("react");
                        animator.SetTrigger("reactNeutral");
                    }
                    else
                    {
                        //_image.sprite = _sadCat.moodSprite;
                        animator.SetTrigger("react");
                        animator.SetTrigger("reactSad");
                    }
                    if (SoundEffectsManager.Instance != null)
                    {
                        if (catReactionData.catSoundReaction == ECatSoundReaction.RelativeReaction)
                        {
                            if (_affinity.currentAffinity >= _happyCat.moodThreshold)
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                            else
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_SAD);
                        }
                        else if (catReactionData.catSoundReaction == ECatSoundReaction.FullReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_SAD);
                        }
                    }
                }
                else if (catReactionData.reactValue >= _happyCat.reactAffinityThreshold)
                {
                    if (_affinity.currentAffinity <= _sadCat.moodThreshold)
                    {
                        //_image.sprite = _neutralCat.reactSprite;
                        animator.SetTrigger("react");
                        animator.SetTrigger("reactNeutral");
                    }
                    else
                    {
                        //_image.sprite = _happyCat.moodSprite;
                        animator.SetTrigger("react");
                        animator.SetTrigger("reactHappy");
                    }
                    if (SoundEffectsManager.Instance != null)
                    {
                        if (catReactionData.catSoundReaction == ECatSoundReaction.RelativeReaction)
                        {
                            if (_affinity.currentAffinity <= _sadCat.moodThreshold)
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                            else
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_HAPPY);
                        }
                        else if (catReactionData.catSoundReaction == ECatSoundReaction.FullReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_HAPPY);
                        }
                    }
                }
                else
                {
                    //_image.sprite = _neutralCat.reactSprite;
                    if (SoundEffectsManager.Instance != null)
                    {
                        if (catReactionData.catSoundReaction == ECatSoundReaction.RelativeReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                        }
                        else if (catReactionData.catSoundReaction == ECatSoundReaction.FullReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                        }
                    }
                }
            }
            //No relative reaction
            else if (!catReactionData.relativeReaction)
            {
                //React on if Sad
                if (catReactionData.reactValue <= _sadCat.reactAffinityThreshold && !catReactionData.relativeReaction)
                {
                    //_image.sprite = _sadCat.reactSprite;
                    animator.SetTrigger("react");
                    animator.SetTrigger("reactSad");
                    if (SoundEffectsManager.Instance != null)
                    {
                        if (catReactionData.catSoundReaction == ECatSoundReaction.RelativeReaction)
                        {
                            if (_affinity.currentAffinity >= _happyCat.moodThreshold)
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                            else
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_SAD);
                        }
                        else if (catReactionData.catSoundReaction == ECatSoundReaction.FullReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_SAD);
                        }
                    }
                }
                //React on if Happy
                else if (catReactionData.reactValue >= _happyCat.reactAffinityThreshold)
                {
                    //_image.sprite = _happyCat.reactSprite;
                    animator.SetTrigger("react");
                    animator.SetTrigger("reactHappy");
                    if (SoundEffectsManager.Instance != null)
                    {
                        if (catReactionData.catSoundReaction == ECatSoundReaction.RelativeReaction)
                        {
                            if (_affinity.currentAffinity <= _sadCat.moodThreshold)
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                            else
                                SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_HAPPY);
                        }
                        else if (catReactionData.catSoundReaction == ECatSoundReaction.FullReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_HAPPY);
                        }
                    }
                }
                else
                {
                    //_image.sprite = _neutralCat.reactSprite;
                    animator.SetTrigger("react");
                    animator.SetTrigger("reactNeutral");
                    if (SoundEffectsManager.Instance != null)
                    {
                        if (catReactionData.catSoundReaction == ECatSoundReaction.RelativeReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                        }
                        else if (catReactionData.catSoundReaction == ECatSoundReaction.FullReaction)
                        {
                            SoundEffectsManager.Instance.PlaySoundFromContainer(CAT_NEUTRAL);
                        }
                    }
                }
            }
        }
        if (!catReactionData.react)
        {
            _timer = _reactionDuration;
        }
        else
        {
            _timer = 0;
        }
        _affinity.currentAffinity += catReactionData.affinity;
    }

    //ReceiveAffinity and react to it
    public void ReceiveAffinity(int reactValue, int affinityValue, bool react)
    {
        _react = true;
        if (react)
        {
           if (reactValue <= _sadCat.reactAffinityThreshold)
            {
                //_image.sprite = _sadCat.reactSprite;
                animator.SetTrigger("react");
                animator.SetTrigger("reactSad");
                //SoundEffectsController.GetInstance().PlaySoundEffect(_sadCat.reactionSound);
            }
           else if (reactValue >= _happyCat.reactAffinityThreshold)
            {
                //_image.sprite = _happyCat.reactSprite;
                animator.SetTrigger("react");
                animator.SetTrigger("reactHappy");
                //SoundEffectsController.GetInstance().PlaySoundEffect(_happyCat.reactionSound);
            }
           else
           {
                //_image.sprite = _neutralCat.reactSprite;
                animator.SetTrigger("react");
                animator.SetTrigger("reactNeutral");
                //SoundEffectsController.GetInstance().PlaySoundEffect(_neutralCat.reactionSound);
           }
        }
        if (react)
        {
            _timer = _reactionDuration;
        }
        else
        {
            _timer = 0;
        }
        _affinity.currentAffinity = affinityValue;
    }
    public int SendAffinity()
    {
        return _affinity.currentAffinity;
    }

    public string SendTag()
    {
        return _catTagName.catTag;
    }

    public string CatName
    {
        get { return _catTagName.catName; }
        set { _catTagName.catName = value; }
    }

}
