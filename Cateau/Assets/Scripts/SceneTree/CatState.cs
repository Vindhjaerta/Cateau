using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CatStateData : SceneTreeData
{
    public bool catEnabled = true;
    public bool applyTransform = false;
    public Transform newTransform;
    public SpriteCatScript newCatPrefab;
}

public class CatState : SceneTreeObject {

    public bool catEnabled = true;
    public bool applyTransform = false;
    public SpriteCatScript newCatPrefab;

    private CatStateData _catState = new CatStateData();
    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        _catState.catEnabled = catEnabled;
        _catState.applyTransform = applyTransform;
        _catState.newTransform = transform;
        _catState.newCatPrefab = newCatPrefab;
        _catState.sender = this;
        _catState.type = ESceneTreeType.CatState;
        _data = _catState;
    }

}
