using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class UITextOverflow {

    public static bool IsOverflow(Text text) { 
    
        if(text != null)
        {
            RectTransform transform = text.rectTransform;
            float testWidth = LayoutUtility.GetPreferredWidth(transform);
            float testHeight = LayoutUtility.GetPreferredHeight(transform);
            float rectWidth = transform.rect.width;
            float rectHeight = transform.rect.height;

            if (testHeight > rectHeight)
            {
                return true;
            }
            else
            {
                if (text.horizontalOverflow == HorizontalWrapMode.Overflow)
                {
                    if(testWidth > rectWidth)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static float GetHeight(Text text)
    {
        if(text != null)
        {
            RectTransform transform = text.rectTransform;
            return LayoutUtility.GetPreferredHeight(transform);
        }
        return 0;
    }

    public static float GetWidth(Text text)
    {
        if (text != null)
        {
            RectTransform transform = text.rectTransform;
            return LayoutUtility.GetPreferredWidth(transform);
        }
        return 0;
    }

    public static Vector3 GetLastPosition(Text text)
    {
        if(text != null)
        {
            TextGenerator textGen = text.cachedTextGenerator;

            return new Vector3(textGen.verts[textGen.vertexCount - 1].position.x, textGen.verts[textGen.vertexCount - 1].position.y, 0);

        }
        return Vector3.zero;
    }

}