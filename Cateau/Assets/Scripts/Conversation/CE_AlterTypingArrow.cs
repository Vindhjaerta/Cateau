using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IArrow : IEventSystemHandler
{
    void OnAlterArrow(Sprite sprite, bool showWhileTyping, Vector2 offset);
}

[CreateAssetMenu(fileName = "Alter Typing Arrow", menuName = "Conversation effects/Alter typing arrow")]
public class CE_AlterTypingArrow : ConversationEffect
{
    public Sprite sprite;
    public bool showWhileTyping;
    public Vector2 offset;

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
        ExecuteEvents.Execute<IArrow>(GameController.Instance.gameObject, null, (handler, data) => handler.OnAlterArrow(sprite,showWhileTyping, offset));
    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }
}