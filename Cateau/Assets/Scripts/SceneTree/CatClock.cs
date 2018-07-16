using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatClock : SceneTreeObject
{
    private Animator animator;
    public List<RotateRectTransform> objectsToRotateList;
    public override void Continue(int nodeIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        animator.enabled = true;
        if (objectsToRotateList != null)
        {
            foreach (RotateRectTransform rotateRectTransform in objectsToRotateList)
            {
                rotateRectTransform.StartRotate();
            }
        }
        Continue();
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
}
