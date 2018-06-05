using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObject : SceneTreeObject
{
    [SerializeField]
    private bool waitForPictureToSlide = true;
    [SerializeField]
    private Transform _objectToSlide;
    [SerializeField]
    private Vector3Variable _endPosition;

    [SerializeField]
    private FloatVariable _slideSpeed;

    private bool _startSlide;

    private bool continueTrue = true;

    private bool startUpdate;

    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        _startSlide = true;
        continueTrue = true;
        startUpdate = true;
        if (_startSlide == false || waitForPictureToSlide == false)
        {
            Continue();
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (_endPosition != null)
        {
            gameObject.transform.localPosition = _endPosition.value;
        }
        else
        {
            Debug.LogError("The end position Vector3Variable wasn't set on: " + gameObject);
        }
    }

    // Update is called once per frame
   /* void Update()
    {
        if (startUpdate)
        {
            if (_startSlide)
            {
                if (GameController.Instance != null)
                {
                    if (GameController.Instance.buttonsClickable == true)
                    {
                        GameController.Instance.buttonsClickable = false;
                    }
                }
            }
            else if (!_startSlide)
            {
                if (GameController.Instance != null)
                {
                    if (GameController.Instance.buttonsClickable == false)
                    {
                        GameController.Instance.buttonsClickable = true;
                    }
                }
            }
        }
    }*/

    private void Update()
    {
        if (_startSlide)
        {
            if (_objectToSlide != null)
            {
                if (GameController.Instance != null)
                {
                        GameController.Instance.buttonsClickable = false;
                }
                if (_slideSpeed != null)
                {
                    _objectToSlide.position = Vector3.Lerp(_objectToSlide.position, transform.position, _slideSpeed.value * Time.deltaTime);
                }
                else
                {
                    Debug.LogError("The slide speed float variable hasn't been set on: " + gameObject);
                }

                if (Vector3.Distance(_objectToSlide.localPosition, transform.localPosition) < 0.1f && continueTrue == true)
                {
                    continueTrue = false;
                    Continue();
                    _objectToSlide.localPosition = transform.localPosition;
                }

                if (Vector3.Distance(_objectToSlide.localPosition, transform.localPosition) < 0.001f)
                {
                    _startSlide = false;
                    if (GameController.Instance != null)
                    {
                            GameController.Instance.buttonsClickable = true;
                    }
                }
            }
            else
            {
                Debug.LogError("There wasn't any object set to slide on: " + gameObject);
            }
        }
    }
}
