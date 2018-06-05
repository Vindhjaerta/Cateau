using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableSceneTreeObject : SceneTreeObject
{

    [SerializeField]
    private SceneTreeObject[] disableSceneTreeObjects;

    [SerializeField]
    private SceneTreeObject[] enableSceneTreeObjects;


    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        foreach (SceneTreeObject disableSceneTreeObject in disableSceneTreeObjects)
        {
            if (disableSceneTreeObject == enabled)
            {
                disableSceneTreeObject.gameObject.SetActive(false);
            }
        }

        foreach (SceneTreeObject enableSceneTreeObject in enableSceneTreeObjects)
        {
            if (enableSceneTreeObject != enabled)
            {
                enableSceneTreeObject.gameObject.SetActive(true);
            }
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
