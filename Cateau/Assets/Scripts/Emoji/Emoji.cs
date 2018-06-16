using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emoji : MonoBehaviour {

    private float lifeDuration;
    private float fadeDuration;
    private float speed;

    private Image img;
    private float fadeCounter;

    public void Initialize(float lifeDuration, float fadeDuration, float speed, Sprite sprite)
    {
        this.fadeDuration = fadeDuration;
        this.speed = speed;
        this.lifeDuration = lifeDuration;
        img = GetComponent<Image>();
        img.sprite = sprite;
        this.fadeCounter = fadeDuration;
    }

    // Update is called once per frame
    void Update () {
		if(lifeDuration > 0)
        {
            lifeDuration -= Time.deltaTime;
        }
        else
        {
            if(fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;
                if(img != null)
                {
                    img.color = new Color(1, 1, 1, fadeCounter / fadeDuration);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
	}
}
