using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSceneTreeData : SceneTreeData
{
    public EmojiType emojiType;
}

public class EmojiSceneTree : SceneTreeObject {

    public EmojiType emojiType;

    private EmojiSceneTreeData _emojiData;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        _emojiData = new EmojiSceneTreeData();
        _emojiData.emojiType = emojiType;
        _emojiData.type = ESceneTreeType.Emoji;
        _emojiData.sender = this;

        _data = _emojiData;
    }

}
