using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteCatScript : MonoBehaviour, ICatBehaviour
{
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
    private CatTagName _catTagName;
    [SerializeField]
    private Affinity _affinity;

    /*[SerializeField]
    private Vector2 _blinkInterval;
    private float _blinkTimer;
    private float blinkAtMoment = 0; */


    private Animator animator;

    private Image _image;

    void Start ()
    {
        _image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        //_affinity.currentAffinity = _affinity.baseAffinity;
    }

	void Update ()
    {
        animator.SetInteger("affinity", _affinity.currentAffinity);
        #region OldReactionThings
        /*
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
        */
#endregion

    }

    public void ReceiveAffinityFromButton(ButtonData catReactionData)
    {
        #region OldReactionThings
        /*
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
        */
#endregion
        _affinity.currentAffinity += catReactionData.affinity;
    }

    //ReceiveAffinity and react to it
    public void ReceiveAffinity(int reactValue, int affinityValue, bool react)
    {
        #region OldReactionThings
        /*
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
        }*/
#endregion
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
