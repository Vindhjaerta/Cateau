using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IShake : IEventSystemHandler
{
    void OnShake(float duration, float magnitude);
}


[CreateAssetMenu(fileName = "ScreenShake", menuName = "Conversation effects/Screen shake")]
public class CE_ScreenShake : ConversationEffect
{
    public int magnitude;
    public float duration;

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
        ExecuteEvents.Execute<IShake>(GameController.Instance.gameObject, null, (handler, data) => handler.OnShake(duration, magnitude));
    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }
}
