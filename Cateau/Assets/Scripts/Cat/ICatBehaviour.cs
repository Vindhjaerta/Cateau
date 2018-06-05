using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICatBehaviour : IEventSystemHandler
{
    void ReceiveAffinity(int reactValue, int affinityValue, bool react);

    int SendAffinity();

    string SendTag();

    string CatName
    {
        get;
        set;
    }
}
