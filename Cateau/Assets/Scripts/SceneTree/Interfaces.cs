
using UnityEngine.EventSystems;
public interface IAffinity : IEventSystemHandler
{
    void ChangeAffinity(int affinityValue);
}

public interface ISceneTreeObject : IEventSystemHandler
{
    void EnableMe();
    void DisableMe();

    void OnChildDisable();
}

public interface ISendCatData : IEventSystemHandler
{
    void SendCatData();
}