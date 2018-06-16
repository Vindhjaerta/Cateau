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


}
