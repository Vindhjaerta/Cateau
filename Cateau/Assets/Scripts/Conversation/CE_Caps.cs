using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CAPS", menuName = "Conversation effects/CAPS")]
public class CE_Caps : ConversationEffect
{
    public override string AlterText(string source)
    {
         return source.ToUpperInvariant();
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

    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }
}
