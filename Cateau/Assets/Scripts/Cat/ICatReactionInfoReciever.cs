using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICatReactionInfoReciever : IEventSystemHandler
{
    void RecieveReactionInfo(ButtonData catReactionData);
}
