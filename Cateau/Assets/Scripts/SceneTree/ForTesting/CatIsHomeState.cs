using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIsHomeState : SceneTreeObject
{
    public StringVariable catIdentifier;
    public bool catComesHome;
    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        if (GameStateContainer.Instance != null)
        {
            if(GameStateContainer.Instance.isCatHome != null)
            {
                if (GameStateContainer.Instance.isCatHome.ContainsKey(catIdentifier.value))
                {
                    GameStateContainer.Instance.isCatHome[catIdentifier.value] = catComesHome;
                }
                else
                {
                    Debug.Log("CatIdentifier wasn't in GamestateContainer but is now added. The StringVaribale added was " + catIdentifier.value);
                    GameStateContainer.Instance.isCatHome.Add(catIdentifier.value, catComesHome);
                }
            }
            else
            {
                Debug.LogWarning("isCatHome dictionary in GameStateContainer doesn't exist");
            }
        }
        else
        {
            Debug.LogWarning("The GameStateContainer couldn't be found by: " + gameObject + ".");
        }
        Continue();
    }
}
