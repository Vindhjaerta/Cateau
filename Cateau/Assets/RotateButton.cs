using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour {

    public bool rotate = true;
    public float speed;
    private float rotation;
    private Image _image;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ToggeRotation);
        _image = GetComponent<Image>();
    }

    private void Update()
    {

        if (rotate)
        {
            if (GameStateContainer.Instance != null)
            {
                if (GameStateContainer.Instance.autoTurnPage)
                {
                    rotation += Time.deltaTime * speed;
                    if (rotation > Mathf.PI * 2) rotation = rotation % (Mathf.PI * 2);

                    _image.rectTransform.Rotate(Vector3.forward * -rotation);
                }
            }
        }
    }

    public void ToggeRotation()
    {
        rotation = 0;
        _image.rectTransform.rotation = Quaternion.identity;
    }
}
