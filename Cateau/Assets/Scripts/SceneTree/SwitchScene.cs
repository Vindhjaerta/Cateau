using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public interface ISwitchScene : IEventSystemHandler
{
    void OnSceneChange();
}

public class SwitchScene : SceneTreeObject {

    public string targetScene;
    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        if (MusicManager.Instance != null)
        {
            Destroy(MusicManager.Instance.gameObject);
        }
        if (GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.SaveGameState();
        }
        ExecuteEvents.ExecuteHierarchy<ISwitchScene>(gameObject, null, (handler, data) => handler.OnSceneChange());
        if(GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.scene = targetScene;
        }
        SceneManager.LoadScene(targetScene);
    }

}
