using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationData : SceneTreeData
{
    public string characterNameTag;
    public string[] stringList;

}

public class Conversation : SceneTreeObject {

    [SerializeField]
    private string _characterNameTag;

    [SerializeField]
    [TextArea(1,10)]
    private string[] _conversationList;



    private ConversationData _convData;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        _convData = new ConversationData();
        _convData.characterNameTag = _characterNameTag;
        _convData.stringList = (string[])_conversationList.Clone();
        _convData.type = ESceneTreeType.Conversation;
        _convData.sender = this;

        _data = _convData;
    }

}
