using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Int Variable",menuName ="Variables/Int")]
public class IntVariable : ScriptableObject {

    public int value;

    public void SetValue(int value)
    {
        this.value = value;
    }

    public void SetValue(IntVariable value)
    {
        this.value = value.value;
    }

    public void ApplyChange(int amount)
    {
        value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        value += amount.value;
    }
}
