using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IEmoji : IEventSystemHandler
{
    void OnEmoji(EmojiType emojiType);
}

[CreateAssetMenu(fileName = "Emoji", menuName = "Conversation effects/Emoji")]
public class CE_Emoji : ConversationEffect
{
    public EmojiType emojiType;

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
        ExecuteEvents.Execute<IEmoji>(GameController.Instance.gameObject, null, (handler, data) => handler.OnEmoji(emojiType));
    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }
}