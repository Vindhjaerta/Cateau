using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeReactionSceneTree : SceneTreeObject
{

    public CE_CharacterReaction cE_CharacterReaction;

    public override void Continue(int nodeIndex)
    {

    }

    protected override void Initialize()
    {
        ReactionPackage reactionPackag = new ReactionPackage();
        if (cE_CharacterReaction.characterIdentifier != null)
        {
            reactionPackag.characterIdentifier = cE_CharacterReaction.characterIdentifier;
        }
        else
        {
            Debug.LogWarning("A characterIdentifier wasn't set");
        }
        if (cE_CharacterReaction.characterReaction != null)
        {
            reactionPackag.characterReaction = cE_CharacterReaction.characterReaction;
        }
        else
        {
            Debug.LogWarning("A characterReaction wasn't set");
        }
        if (cE_CharacterReaction.buppData != null)
        {
            reactionPackag.buppData = cE_CharacterReaction.buppData;
        }
        else
        {
            Debug.LogWarning("ButtonData was null");
        }
        if (CharacterController.Instance != null)
        {
            CharacterController.Instance.DelegateReaction(reactionPackag);
        }
        else
        {
            Debug.LogWarning("CharacterController doesn't exist");
        }
        Continue();
    }

}
