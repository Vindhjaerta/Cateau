using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Comparator : ScriptableObject
{
    public abstract bool Check(float baseValue, float compareValue);
}

