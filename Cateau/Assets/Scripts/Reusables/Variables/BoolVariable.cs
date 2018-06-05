using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Bool Variable",menuName ="Variables/Bool")]
public class BoolVariable : ScriptableObject {

    public bool value;

    public void SetValue(bool value)
    {
        this.value = value;
    }

    public void SetValue(BoolVariable value)
    {
        this.value = value.value;
    }

    public void Switch()
    {
        value = !value;
    }

}
