using UnityEngine.EventSystems;
using UnityEngine;

public class PhoneSceneObjectAddMessage : SceneTreeObject
{
    public Sprite sprite;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        ExecuteEvents.Execute<IPhone>(GameController.Instance.gameObject, null, (handler, data) => handler.OnAddMessage(sprite));



    }

    
}