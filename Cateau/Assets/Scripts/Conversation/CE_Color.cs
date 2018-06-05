using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Color",menuName ="Conversation effects/Color")]
public class CE_Color : ConversationEffect
{
    public Color32 color;

    public override string AlterText(string source)
    {
        return source;
    }

    public override string GetPostTag()
    {
        string temp = "";
        temp += "</color>";
        return temp;
    }

    public override string GetPreTag()
    {
        string temp = "";
        temp += "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">";
        return temp;
    }

    public override void Initialize(PrintText controller, Sentence source, Text textRef)
    {

    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }

    public override void Finalize(PrintText controller, Sentence source, Text textRef)
    {

    }
}
