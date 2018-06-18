using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour
{
    public StringVariable characterIdentifier;

    public int setAnimationLayer = 0;

    private Animator animator;
    private List<ReactionPackage> _reactionPackages = new List<ReactionPackage>();
    private ReactionPackage currentReactionPackages;

    private bool currentlyUpdatingReaction = false;

    private Vector3 startLerpPosition;
    private Vector3 endLerpPosition;

    private float currentTime;

    private bool firstBuppDone;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        if (CharacterController.Instance != null)
        {
            CharacterController.Instance.animatedCharacter.Add(this);
        }
        else
        {
            Debug.LogWarning("No CharacterController was found");
        }

        int layerCount = animator.layerCount;
        if (layerCount -1 >= setAnimationLayer)
        {
            animator.SetLayerWeight(setAnimationLayer, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UnpackReaction();
        if (currentlyUpdatingReaction)
        {
            UpdateBupp();
        }
    }

    public void ReceiveReaction(ReactionPackage reactionPackage)
    {
        _reactionPackages.Add(reactionPackage);
    }

    private void UnpackReaction()
    {
        if (_reactionPackages.Count >= 1 && !currentlyUpdatingReaction)
        {
            if (_reactionPackages[0].reactionDone != true)
            {
                startLerpPosition = gameObject.transform.position;
                if (_reactionPackages[0].buppData != null)
                {
                    endLerpPosition.x = gameObject.transform.position.x + _reactionPackages[0].buppData.distination.x;
                    endLerpPosition.y = gameObject.transform.position.y + _reactionPackages[0].buppData.distination.y;
                    endLerpPosition.z = gameObject.transform.position.z;
                }
                currentTime = 0f;
                firstBuppDone = false;
                currentlyUpdatingReaction = true;
                if (_reactionPackages[0].buppData == null)
                {
                    animator.SetTrigger(_reactionPackages[0].characterReaction);
                    currentlyUpdatingReaction = false;
                    _reactionPackages[0].reactionDone = true;
                }
            }
            else if (_reactionPackages[0].reactionDone)
            {
                _reactionPackages.RemoveAt(0);
            }
        }
    }

    private void UpdateBupp()
    {
        if (currentlyUpdatingReaction == true && !firstBuppDone)
        {
            currentTime += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endLerpPosition, currentTime / _reactionPackages[0].buppData.duration);

            if (gameObject.transform.position == endLerpPosition && firstBuppDone == false)
            {
                animator.SetTrigger(_reactionPackages[0].characterReaction);
                currentTime = 0f;
                firstBuppDone = true;
            }
        }

        else if (firstBuppDone == true)
        {
            currentTime += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startLerpPosition, currentTime / _reactionPackages[0].buppData.duration);

            if (gameObject.transform.position == startLerpPosition)
            {
                currentlyUpdatingReaction = false;
                _reactionPackages[0].reactionDone = true;
            }
        }
    }

}
