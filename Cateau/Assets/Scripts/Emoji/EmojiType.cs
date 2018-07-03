using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Emoji", menuName = "Images/Emoji")]
public class EmojiType : ScriptableObject {

    public Sprite sprite;
    public float lifeDuration;
    public float fadeDuration;
    public float speed;

    //Added 2018-07-03 by Jesper (Audio for Emoji / )
    public string soundEffectContainerName;

    public float idle;
}
