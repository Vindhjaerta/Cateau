using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationIndex : SceneTreeObject
{
    public StringVariable characterIdentifier;
    public int newAnimationLayerIndex;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        ChangeAnimationLayerData changeAnimationLayerData = new ChangeAnimationLayerData();
        if (characterIdentifier != null)
        {
            changeAnimationLayerData.characterIdentifier = characterIdentifier;
        }

        if (newAnimationLayerIndex >= 0)
        {
            changeAnimationLayerData.animationIndex = newAnimationLayerIndex;
        }

        if (CharacterController.Instance != null)
        {
            CharacterController.Instance.DelegateAnimationLayerChange(changeAnimationLayerData);
        }

    }

}
