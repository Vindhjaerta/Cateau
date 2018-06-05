using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FadeEffect : ScriptableObject
{
    public abstract void UpdateEffect(FadeSceneTreeObject fadeSceneTreeObject, float deltaTime);
}
