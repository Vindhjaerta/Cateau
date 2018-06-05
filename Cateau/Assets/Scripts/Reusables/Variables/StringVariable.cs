using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="String Variable",menuName ="Variables/String")]
public class StringVariable : ScriptableObject {

    public string value;

    public void SetValue(string value)
    {
        this.value = value;
    }

    public void SetValue(StringVariable value)
    {
        this.value = value.value;
    }

    public void ApplyChange(string amount)
    {
        value += amount;
    }

    public void ApplyChange(StringVariable amount)
    {
        value += amount.value;
    }
}
