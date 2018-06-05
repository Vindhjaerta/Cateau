using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatReaction : SceneTreeObject
{
    [HideInInspector]
    public int reactValue;
    public ECatReaction catReaction;
    [HideInInspector]
    public bool react;
    public bool relativeReaction;
    public ECatSoundReaction catSoundReaction;
    public int affinity;

    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        if (catReaction == ECatReaction.Negative)
        {
            reactValue = -2;
            react = true;
        }
        else if (catReaction == ECatReaction.Neutral)
        {
            reactValue = 0;
            react = true;
        }
        else if (catReaction == ECatReaction.Positive)
        {
            reactValue = +2;
            react = true;
        }

        ButtonData buttonData = new ButtonData(EButtonChoice.catButton, "", reactValue, affinity, react, relativeReaction, catSoundReaction);
        ExecuteEvents.ExecuteHierarchy<ICatReactionInfoReciever>(gameObject, null, (x, y) => x.RecieveReactionInfo(buttonData));
        Continue();
    }
}
