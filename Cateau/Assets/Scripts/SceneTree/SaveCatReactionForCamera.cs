﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCatReactionForCamera : SceneTreeObject
{
    public IntVariable imageIndex;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        if (GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.activePhotos[imageIndex.value] = true;
        }


    }
}
