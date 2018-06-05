using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TextDelay", menuName = "Conversation effects/Text delay")]
public class CE_TextDelay : ConversationEffect
{
    public float delay;

    public override string AlterText(string source)
    {
        return source;
    }

    public override void Finalize(PrintText controller, Sentence source, Text textRef)
    {
        controller.delayCounter = delay;
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

    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }
}