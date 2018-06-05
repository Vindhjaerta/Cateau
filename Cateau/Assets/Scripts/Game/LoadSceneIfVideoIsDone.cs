using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoadSceneIfVideoIsDone : MonoBehaviour
{

    private VideoPlayer _vp;
    private double clipLength;
    private Timer timer;
	// Use this for initialization
	void Start ()
    {
        _vp = GetComponent<VideoPlayer>();
        _vp.Play();
        timer = new Timer((float) _vp.clip.length + 1);
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer.Tick(Time.deltaTime);
        if (timer.Done)
        {
            if(Load_Menu.Instance != null)
            {
                Load_Menu.Instance.LoadMenu();
                //Debug.Log("Isn't playing: " + _vp.isPlaying);
            }
        }
	}
}
