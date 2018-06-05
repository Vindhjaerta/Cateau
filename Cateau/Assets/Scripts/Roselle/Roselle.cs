using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roselle : MonoBehaviour
{

    [SerializeField]
    private Vector2 _blinkInterval;

    private Animator animator;

    private float _blinkTimer;

    private float _blinkAtMoment;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _blinkTimer += Time.deltaTime;
        if (_blinkTimer > _blinkInterval.x)
        {
            if (_blinkAtMoment != 0)
            {
                if (_blinkTimer > _blinkAtMoment)
                {
                    animator.SetTrigger("timeToBlink");
                    _blinkAtMoment = 0;
                    _blinkTimer = 0;
                }
            }
            else
            {
                _blinkAtMoment = Random.Range(_blinkInterval.x, _blinkInterval.y);
                //Debug.Log(_blinkAtMoment);
            }
        }
    }
}
