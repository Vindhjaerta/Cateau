using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUI : SceneTreeObject
{
    public bool enableInsteadOfDisable;
    public override void Continue(int nodeIndex)
    {

    }

    protected override void Initialize()
    {
        if (GameController.Instance != null)
        {
            if (enableInsteadOfDisable)
            {
                GameController.Instance.EnableConversationController();
            }
            else
            {
                GameController.Instance.DisableConversationController();
            }
        }
        Continue();
    }

}
