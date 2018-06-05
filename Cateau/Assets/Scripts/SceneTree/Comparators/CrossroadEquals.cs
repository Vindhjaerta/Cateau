using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equals", menuName = "Comparator/Equals")]
public class CrossroadEquals : Comparator
{
    public override bool Check(float baseValue, float compareValue)
    {
        return baseValue == compareValue ? true : false;
    }
}