using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockOnClick : MonoBehaviour
{

    [SerializeField]
    private float timeToWait;
    Button _button;

    private float time;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToWait)
        {
            _button.interactable = true;
        }
    }

    private void OnEnable()
    {
        _button.interactable = false;
        time = 0;
    }
}
