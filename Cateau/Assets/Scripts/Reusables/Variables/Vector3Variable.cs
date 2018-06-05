using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3 Variable", menuName = "Variables/Vector3")]
public class Vector3Variable : ScriptableObject
{
    public Vector3 value;

    public void SetValue(Vector3 value)
    {
        this.value = value;
    }

    public void SetValue(Vector3Variable value)
    {
        this.value = value.value;
    }

    public void ApplyChange(Vector3 amount)
    {
        value += amount;
    }

    public void ApplyChange(Vector3Variable amount)
    {
        value += amount.value;
    }
}
