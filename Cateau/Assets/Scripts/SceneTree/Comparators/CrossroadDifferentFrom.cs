using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Different from", menuName = "Comparator/Different from")]
public class CrossroadDifferentFrom : Comparator
{
    public override bool Check(float baseValue, float compareValue)
    {
        return baseValue != compareValue ? true : false;
    }
}