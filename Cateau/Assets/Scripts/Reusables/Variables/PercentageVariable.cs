using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Percentage Variable",menuName ="Variables/Percentage")]
public class PercentageVariable : ScriptableObject {

    [Range(0.0f,1.0f)]
    public float value;


    public void SetValue(float value)
    {
        this.value = value;
    }

    public void SetValue(FloatVariable value)
    {
        this.value = value.value;
    }

    public void ApplyChange(float amount)
    {
        value += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        value += amount.value;
    }
}
