using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PageBreak", menuName = "Conversation effects/Page break")]
public class CE_PageBreak : ConversationEffect
{
    public override string AlterText(string source)
    {
        return source;
    }

    public override void Finalize(PrintText controller, Sentence source, Text textRef)
    {
        controller.PageBreak();
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