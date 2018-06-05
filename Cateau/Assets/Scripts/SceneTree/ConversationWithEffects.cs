using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationWithEffectsData : SceneTreeData
{
    public string characterNameTag;
    public List<ConversationEffect> nameEffects;
    public List<Sentence> sentences;
}

public class ConversationWithEffects : SceneTreeObject {

    public StringReference characterNameTag;
    public List<ConversationEffect> nameEffects;

    private ConversationWithEffectsData _convData;

    public List<GUISentence> sentences;

    [System.Serializable]
    public class GUISentence
    {
        public StringReference text;

        public List<ConversationEffect> effects;

    }


    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        _convData = new ConversationWithEffectsData();

        _convData.characterNameTag = characterNameTag;
        if (sentences != null)
        {
            _convData.sentences = new List<Sentence>();

            for (int i = 0; i < sentences.Count; i++)
            {
                if (sentences[i].effects.Count > 0)
                    _convData.sentences.Add(new Sentence(sentences[i].text, sentences[i].effects));
                else
                    _convData.sentences.Add(new Sentence(sentences[i].text, null));
            }
        }
        else
        {
            _convData.sentences = null;
        }
        _convData.nameEffects = nameEffects;
        _convData.type = ESceneTreeType.ConversationWithEffects;
        _convData.sender = this;

        _data = _convData;

    }

}
