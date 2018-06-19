using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ConversationSoundeffect", menuName = "Conversation effects/Play soundeffect")]
public class CE_PlaySoundeffect : ConversationEffect
{

    public string nameOfSoundContainer;

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
        if (SoundEffectsManager.Instance != null)
        {
            SoundEffectsManager.Instance.PlaySoundFromContainer(nameOfSoundContainer);
        }
        else
        {
            Debug.Log("SoundeffectsManager wasn't found");
        }
    }

    public override void Tick(PrintText controller, Sentence source, Text textRef, float deltatime)
    {
        
    }

}
