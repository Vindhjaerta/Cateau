using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="TextPrintSpeed",menuName ="Conversation effects/Text print speed")]
public class CE_TextPrintSpeed : ConversationEffect {

    public float lettersPerSecond;

    public override string AlterText(string source)
    {
        return source;
    }

    public override void Finalize(PrintText controller, Sentence source, Text textRef)
    {
        controller.currentSentencePrintSpeed = lettersPerSecond;
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
