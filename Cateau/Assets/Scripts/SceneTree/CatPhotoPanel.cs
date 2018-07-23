using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatPhotoPanel : SceneTreeObject
{
    [SerializeField]
    private bool _stayUntilSceneChange;
    [SerializeField]
    private FloatVariable _displayTime;

    private Image _image;


    public override void Continue(int nodeIndex)
    {
        StartCoroutine(Wait());
    }

    protected override void Initialize()
    {
        _image.enabled = true;

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_displayTime.value);
        if(!_stayUntilSceneChange)
        {
            _image.enabled = false;
        }
        Continue();
    }

    // Use this for initialization
    void Start ()
    {
        _image = GetComponent<Image>();
        if (_image.enabled == true)
        {
            _image.enabled =false;
        }
	}
	
}
