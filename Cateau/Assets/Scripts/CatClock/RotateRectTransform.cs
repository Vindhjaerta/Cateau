using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRectTransform : MonoBehaviour
{
    private RectTransform rectTransform;

    public float rotationSpeed;
    private bool rotate;
	// Use this for initialization
	void Start ()
    {
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (rotate == true)
        {
            rectTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
	}

    public void StartRotate()
    {
        rotate = true;
    }

    public void StopRotate()
    {
        rotate = false;
    }
}
