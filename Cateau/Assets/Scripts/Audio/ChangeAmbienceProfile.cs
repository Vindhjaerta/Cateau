using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmbienceProfile : SceneTreeObject
{

    public AmbienceProfile ambienceProfile;

    public override void Continue(int nodeIndex)
    {
        //Continued
    }

    protected override void Initialize()
    {
        if (AmbienceManager.Instance != null && ambienceProfile != null)
        {
            AmbienceManager.Instance.StartPlayingAmbienceProfile(ambienceProfile);
        }

        Continue();
    }

}
