using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnNameBasedOnTag : SceneTreeObject
{

    public StringVariable nameIdentifier;

    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        if (GameStateContainer.Instance != null)
        {
            if(GameStateContainer.Instance.names != null)
            {
                if (GameStateContainer.Instance.names.ContainsKey(nameIdentifier.value))
                {
                    if (GameStateContainer.Instance.names[nameIdentifier.value] != null)
                    {
                        string name = GameStateContainer.Instance.names[nameIdentifier.value];
                        Debug.Log("NameIdentifier existed GameStateContainer, the name returned from GameStateContainer was: " + name);
                    }
                }
                else
                {
                    Debug.Log("NameIdentifier was null in GameStateContainer");
                }
            }
            else
            {
                Debug.Log("NamesDictionary was null in GameStateContainer");
            }
        }
        else
        {
            Debug.Log("GameStateContainer was null");
        }
        Continue();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
