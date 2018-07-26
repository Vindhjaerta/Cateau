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
            if (text.text.Length > 0)
            {
                TextGenerator textGen = text.cachedTextGenerator;

                return new Vector3(textGen.verts[textGen.vertexCount - 1].position.x, textGen.verts[textGen.vertexCount - 1].position.y, 0);
                //return text.transform.TransformPoint(new Vector3(textGen.verts[textGen.vertexCount - 1].position.x, textGen.verts[textGen.vertexCount - 1].position.y, 0));

                //int charIndex = text.text.Length - 1;
                //TextGenerator textGen = new TextGenerator(text.text.Length);
                //Vector2 extents = text.gameObject.GetComponent<RectTransform>().rect.size;
                //textGen.Populate(text.text, text.GetGenerationSettings(extents));

                //int newLine = text.text.Substring(0, charIndex).Split('\n').Length - 1;
                //int whiteSpace = text.text.Substring(0, charIndex).Split(' ').Length - 1;
                //int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;
                //if (indexOfTextQuad < textGen.vertexCount)
                //{
                //    Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
                //        textGen.verts[indexOfTextQuad + 1].position +
                //        textGen.verts[indexOfTextQuad + 2].position +
                //        textGen.verts[indexOfTextQuad + 3].position) / 4f;

                //    return avgPos;
                //}

                //return text.transform.TransformPoint(new Vector3(textGen.verts[textGen.vertexCount - 1].position.x, textGen.verts[textGen.vertexCount - 1].position.y, 0));
                //return new Vector3(textGen.verts[textGen.vertexCount - 1].position.x, textGen.verts[textGen.vertexCount - 1].position.y, 0);
            }
        }
        return Vector3.zero;
    }

}