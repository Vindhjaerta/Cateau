using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmojiController : MonoBehaviour {

    public GameObject spawnTemplate;

	public void Spawn(EmojiType type)
    {
        GameObject obj = Instantiate(spawnTemplate, transform);
        Emoji emoji = obj.GetComponent<Emoji>();
        emoji.Initialize(type.lifeDuration, type.fadeDuration, type.speed, type.sprite);
    }
}
