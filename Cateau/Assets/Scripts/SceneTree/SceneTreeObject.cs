using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public interface ISceneTreeData : IEventSystemHandler
{
    void OnRecieveSceneTreeData(SceneTreeData data);
}

public class SceneTreeData : BaseEventData
{
    public ESceneTreeType type;
    public SceneTreeObject sender;
    public SceneTreeData() : base(EventSystem.current)
    {

    }
}

public abstract class SceneTreeObject : MonoBehaviour {

    public int savepointIndex;

    [SerializeField]
    protected SceneTreeObject _nextNode;

    protected SceneTreeData _data;
    public abstract void Continue(int nodeIndex);

    protected void Continue()
    {
        if (_nextNode != null)
        {
            if (_nextNode != this)
            {
                _nextNode.ActivateAndWait();
            }
        }
        else
        {
            SceneTreeObject obj = null;
            for (int i = 0; i < transform.childCount; i++)
            {
                obj = transform.GetChild(i).GetComponentInChildren<SceneTreeObject>();
                if (obj != null) break;
            }

            if (obj != null)
            {
                obj.ActivateAndWait();
            }
        }
    }

    protected abstract void Initialize();

    public void ActivateAndWait()
    {
        _data = new SceneTreeData();
        Initialize();
        ExecuteEvents.ExecuteHierarchy<ISceneTreeData>(gameObject, _data, (handler, data) => handler.OnRecieveSceneTreeData((SceneTreeData)data));
    }

}
