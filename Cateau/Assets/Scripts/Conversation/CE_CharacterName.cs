using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterName", menuName = "Conversation effects/Character name")]
public class CE_CharacterName : ConversationEffect
{
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
        if (GameStateContainer.Instance != null)
        {
            if (source.text == null) source.text = "";
            if (GameStateContainer.Instance.names.ContainsKey(source.text))
            {
                source.text = GameStateContainer.Instance.names[source.text];
            }

        }
    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {

    }
}
