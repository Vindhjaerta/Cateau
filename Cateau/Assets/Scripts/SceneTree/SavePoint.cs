using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISerializeCat : IEventSystemHandler
{
    void SerializeCat(bool save);
}

public class SavePoint : SceneTreeObject {

    public SceneTreeObject nodeForLoadContinue;

    public override void Continue(int nodeIndex)
    {
        if (GameStateContainer.Instance != null && savepointIndex != 0)
        {
            GameStateContainer.Instance.savepointIndex = savepointIndex;

            if (GameStateContainer.Instance.useSavepointContinuePath)
            {
                GameStateContainer.Instance.useSavepointContinuePath = false;
                targetNode = nodeForLoadContinue;
            }
            if (nodeIndex != 0)
            {
                ExecuteEvents.ExecuteHierarchy<ISerializeCat>(gameObject, null, (sender, data) => sender.SerializeCat(true));
                GameStateContainer.Instance.SaveGameState();
            }
        }
        Continue();
    }

    protected override void Initialize()
    {
        Continue(1);
    }
}
