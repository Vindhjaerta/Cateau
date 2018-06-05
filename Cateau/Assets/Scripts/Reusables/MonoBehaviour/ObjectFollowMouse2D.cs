using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
     Author: Daniel Vindhjärta
     Last updated: 2018-12-26 
     Purpose:
     Allows a GameObject to follow the mouse on screen (on a 2D plane).
     
     Usage:
     Attach script to any GameObject. As long as the script is active the object will follow the mouse. If "camera" is not set
     the GameObject will move on the main camera.
     "bindToScreen" locks the GameObject inside the camera view. "useLerp" (in combination with "moveSpeed") gives a small delay
     in the object's movement.
     
     Dependencies:
     Unity
     
*/
public class ObjectFollowMouse2D : MonoBehaviour {

    public Camera camera;

    private Vector3 _mousePosition;
    
    public bool useLerp;
    public float moveSpeed = 0.1f;
    public bool bindToScreen;

    private Camera _camera;

    // Use this for initialization
    void Start () {
        if (camera == null) _camera = Camera.main;
        else _camera = camera;
	}

    // Update is called once per frame
    void Update()
    {

        _mousePosition = Input.mousePosition;

        if (bindToScreen)
        {
            if (_mousePosition.x < 0) _mousePosition.x = 0;
            if (_mousePosition.x > Screen.width) _mousePosition.x = Screen.width;
            if (_mousePosition.y < 0) _mousePosition.y = 0;
            if (_mousePosition.y > Screen.width) _mousePosition.y = Screen.width;
        }

        _mousePosition = _camera.ScreenToWorldPoint(_mousePosition);
        if (useLerp)
            transform.position = Vector2.Lerp(transform.position, _mousePosition, moveSpeed);
        else
            transform.position = new Vector3(_mousePosition.x, _mousePosition.y);
    }
}
