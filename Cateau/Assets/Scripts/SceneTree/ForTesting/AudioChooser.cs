using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChooser : SceneTreeObject
{
    public AudioSwitch audioSwitch;
    public AmbienceContainer[] ambienceContainers;

    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        audioSwitch.StartAudioController(ambienceContainers);
        Continue();
    }
}
