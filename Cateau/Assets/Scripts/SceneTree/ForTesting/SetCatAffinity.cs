using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCatAffinity : SceneTreeObject
{
    public StringVariable catIdentifier;
    public int affinitToSet;
    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        if (GameStateContainer.Instance != null)
        {
            if (GameStateContainer.Instance.isCatHome != null)
            {
                if (GameStateContainer.Instance.affinity.ContainsKey(catIdentifier.value))
                {
                    GameStateContainer.Instance.affinity[catIdentifier.value] = affinitToSet;
                }
                else
                {
                    //Debug.Log("CatIdentifier wasn't in GamestateContainer but is now added. The StringVaribale added was " + catIdentifier.value + " with affinity: " + affinitToAdd);
                    GameStateContainer.Instance.affinity.Add(catIdentifier.value, affinitToSet);
                }
            }
            else
            {
                Debug.LogWarning("affinity dictionary in GameStateContainer doesn't exist");
            }
        }
        else
        {
            Debug.LogWarning("The GameStateContainer couldn't be found by: " + gameObject + ".");
        }
        Continue();
    }
}
