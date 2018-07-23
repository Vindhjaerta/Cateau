using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSceneTreeObject : SceneTreeObject
{

    public RectTransform objectToTeleport;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        if (objectToTeleport != null)
        {
            objectToTeleport.position = gameObject.transform.position;
        }
        else
        {
            Debug.LogWarning("The rectTransform of an object to teleport hasn't been set in the inspector: " + gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        
    }

}
