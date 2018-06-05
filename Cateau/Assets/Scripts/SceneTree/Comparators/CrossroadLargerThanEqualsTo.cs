using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Larger than or equals to", menuName = "Comparator/Larger than or equals to")]
public class CrossroadLargerThanEqualsTo : Comparator
{
    public override bool Check(float baseValue, float compareValue)
    {
        return baseValue >= compareValue ? true : false;
    }
}