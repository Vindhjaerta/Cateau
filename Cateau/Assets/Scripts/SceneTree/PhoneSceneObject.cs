using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneData : SceneTreeData
{
    public bool showOnScreen;
    public bool clearHistory;
}

public class PhoneSceneObject : SceneTreeObject
{
    public bool showOnScreen = true;
    public bool clearHistory = true;

    private PhoneData _phoneData;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        _phoneData = new PhoneData();
        _phoneData.clearHistory = clearHistory;
        _phoneData.showOnScreen = showOnScreen;
        _phoneData.type = ESceneTreeType.Phone;
        _phoneData.sender = this;

        _data = _phoneData;
    }
}

