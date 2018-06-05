using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Less than or equals to", menuName = "Comparator/Less than or equals to")]
public class CrossroadLessThanEqualsTo : Comparator
{
    public override bool Check(float baseValue, float compareValue)
    {
        return baseValue <= compareValue ? true : false;
    }
}