using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IPhone : IEventSystemHandler
{
    void OnAddMessage(Sprite sprite);
}

[CreateAssetMenu(fileName = "PhoneMessage", menuName = "Conversation effects/Phone Message")]
public class CE_AddPhoneMessage : ConversationEffect
{
    public Sprite sprite;

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
        ExecuteEvents.Execute<IPhone>(GameController.Instance.gameObject, null, (handler, data) => handler.OnAddMessage(sprite));
    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }
}