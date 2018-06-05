using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData : SceneTreeData
{
    public string characterTag;
    public string caption;
}

public class InputSceneObject : SceneTreeObject {

    public StringReference characterTag;
    public StringReference caption;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        InputData newData = new InputData();
        newData.characterTag = characterTag;
        if (caption == null) newData.caption = "";
        else newData.caption = caption;
        newData.sender = this;
        newData.type = ESceneTreeType.InputField;
        _data = newData;

    }

}
