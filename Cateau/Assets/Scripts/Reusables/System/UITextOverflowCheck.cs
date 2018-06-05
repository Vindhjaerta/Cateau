using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class UITextOverflowCheck {

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

}