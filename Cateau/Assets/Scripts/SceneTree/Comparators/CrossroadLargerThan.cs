using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Larger than", menuName = "Comparator/Larger than")]
public class CrossroadLargerThan : Comparator
{
    public override bool Check(float baseValue, float compareValue)
    {
        return baseValue > compareValue ? true : false;
    }
}