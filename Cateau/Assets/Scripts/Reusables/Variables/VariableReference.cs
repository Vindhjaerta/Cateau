using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatReference{

    public bool useConstant;
    public float constantValue;
    public FloatVariable variable;

    public FloatReference(float value)
    {
        useConstant = true;
        constantValue = value;
    }

    public float value
    {
        get
        {
            if (useConstant)
            {
                return constantValue;
            }
            else
            {
                if (variable != null)
                {
                    return variable.value;
                }
                else
                {
                    Debug.LogError("FloatReference is null");
                    return 0;
                }
            }
        }
    }

    public static implicit operator float(FloatReference reference)
    {
        return reference.value;
    }

}

[System.Serializable]
public class StringReference
{

    public bool useConstant;
    public string constantValue;
    public StringVariable variable;
#if UNITY_EDITOR
    public int GUIpresentationIndex;
#endif
    public StringReference(string value)
    {
        useConstant = true;
        constantValue = value;
    }

    public string value
    {
        get
        {
            if (useConstant)
            {
                return constantValue;
            }
            else
            {
                if (variable != null)
                {
                    return variable.value;
                }
                else
                {
                    Debug.LogError("StringReference is null");
                    return "";
                }
            }
        }
    }

    public static implicit operator string(StringReference reference)
    {
        return reference.value;
    }

}

[System.Serializable]
public class IntReference
{
    public bool useConstant = true;
    public int constantValue;
    public IntVariable variable;

    public IntReference(int value)
    {
        useConstant = true;
        constantValue = value;
    }

    public int value
    {
        get
        {
            if (useConstant)
            {
                return constantValue;
            }
            else
            {
                if (variable != null)
                {
                    return variable.value;
                }
                else
                {
                    Debug.LogError("IntReference is null");
                    return 0;
                }
            }
        }
    }

    public static implicit operator int(IntReference reference)
    {
        return reference.value;
    }

}

[System.Serializable]
public class BoolReference
{
    public bool useConstant;
    public bool constantValue;
    public BoolVariable variable;

    public BoolReference(bool value)
    {
        useConstant = true;
        constantValue = value;
    }

    public bool value
    {
        get
        {
            if (useConstant)
            {
                return constantValue;
            }
            else
            {
                if (variable != null)
                {
                    return variable.value;
                }
                else
                {
                    Debug.LogError("BoolReference is null");
                    return false;
                }
            }
        }
    }

    public static implicit operator bool(BoolReference reference)
    {
        return reference.value;
    }

}

[System.Serializable]
public class PercentageReference
{

    public bool useConstant = true;
    public float constantValue;
    public PercentageVariable variable;

    public PercentageReference(float value)
    {
        useConstant = true;
        constantValue = value;
    }

    public float value
    {
        get
        {
            if (useConstant)
            {
                return constantValue;
            }
            else
            {
                if (variable != null)
                {
                    return variable.value;
                }
                else
                {
                    Debug.LogError("PercentageReference is null");
                    return 0;
                }
            }
        }
    }

    public static implicit operator float(PercentageReference reference)
    {
        return reference.value;
    }

}