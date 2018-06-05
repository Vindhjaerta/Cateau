using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Format",menuName ="Conversation effects/Format")]
public class CE_Format : ConversationEffect
{
    public int size = 0;
    public bool bold;
    public bool italic;

    public override string AlterText(string source)
    {
        return source;
    }

    public override string GetPostTag()
    {
        string temp = "";
        if (size > 0) temp += "</size>";
        if (italic) temp += "</i>";
        if (bold) temp += "</b>";

        return temp;
    }

    public override string GetPreTag()
    {
        string temp = "";
        if (bold) temp += "<b>";
        if (italic) temp += "<i>";
        if (size > 0) temp += "<size=" + size + ">";
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
