using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVpan : MonoBehaviour {

    // default scroll values
    // positive values move to the right and down
    // negative left and up
    public float horizontalScrollSpeed = 0.1f;
    public float verticalScrollSpeed = 0.1f;

    // works with most materials. The actual material must be selected in the Inspector.
    public Material texture;
    public UnityEngine.UI.RawImage image;

    // on-off switch
    [SerializeField]
    private bool scroll = true;

    private float verticalOffset;
    private float horizontalOffset;

	void Update()
    {
        // As long as "scroll" is on - offset the texture component in the material.
        if (scroll)
        {
            verticalOffset += Time.deltaTime * verticalScrollSpeed;
            if (verticalOffset > 1)
                verticalOffset = verticalOffset % 1;

            horizontalOffset += Time.deltaTime * horizontalScrollSpeed;
            if (horizontalOffset > 1)
                horizontalOffset = horizontalOffset % 1;

            texture.mainTextureOffset = new Vector2(horizontalOffset, verticalOffset);
            image.enabled = false;
            image.enabled = true;

        }
    }
}
