using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterReaction", menuName = "Conversation effects/Character Reaction")]
public class CE_CharacterReaction : ConversationEffect
{
 
    public StringVariable characterIdentifier;
    public string characterReaction;
    public BuppData buppData;

    public override string AlterText(string source)
    {

        return source;
    }

    public override void Finalize(PrintText controller, Sentence source, Text textRef)
    {

    }

    public override string GetPostTag()
    {
        return "";
    }

    public override string GetPreTag()
    {
        return "";
    }

    public override void Initialize(PrintText controller, Sentence source, Text textRef)
    {
        //Create the ReactionPackage
        ReactionPackage reactionPackag = new ReactionPackage();
        if (characterIdentifier != null)
        {
            reactionPackag.characterIdentifier = characterIdentifier;
        }
        else
        {
            Debug.LogWarning("A characterIdentifier wasn't set");
        }
        if (characterReaction != null)
        {
            reactionPackag.characterReaction = characterReaction;
        }
        else
        {
            Debug.LogWarning("A characterReaction wasn't set");
        }
        if (buppData != null)
        {
            reactionPackag.buppData = buppData;
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
    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {
        
    }

}
