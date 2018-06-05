using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Less than", menuName = "Comparator/Less than")]
public class CrossroadLessThan : Comparator
{
    public override bool Check(float baseValue, float compareValue)
    {
        return baseValue < compareValue ? true : false;
    }
}