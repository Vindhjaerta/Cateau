using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : ReferenceDrawer
{
    
}

[CustomPropertyDrawer(typeof(StringReference))]
public class StringReferenceDrawer : PropertyDrawer
{
    private readonly string[] popupOptions =
        { "Use constant (line)", "Use constant (field)", "Use variable" };

    /// <summary> Cached style to use to draw the popup button. </summary>
    private GUIStyle popupStyle;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);
        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("constantValue");
        SerializedProperty variable = property.FindPropertyRelative("variable");
        SerializedProperty GUIpresentationIndex = property.FindPropertyRelative("GUIpresentationIndex");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int result = EditorGUI.Popup(buttonRect, GUIpresentationIndex.intValue, popupOptions, popupStyle);
        //EditorGUI.indentLevel = result;
        GUIpresentationIndex.intValue = result;

        switch (GUIpresentationIndex.intValue)
        {
            case 0:
                useConstant.boolValue = true;
                EditorGUI.PropertyField(position, constantValue, GUIContent.none);
                break;
            case 1:
                useConstant.boolValue = true;
                constantValue.stringValue = GUI.TextArea(position, constantValue.stringValue);
                break;
            case 2:
                useConstant.boolValue = false;
                EditorGUI.PropertyField(position, variable, GUIContent.none);
                break;
        }

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {

        float totalHeight = EditorGUI.GetPropertyHeight(property, label, true);
        SerializedProperty GUIpresentationIndex = property.FindPropertyRelative("GUIpresentationIndex");
        switch(GUIpresentationIndex.intValue)
        {
            case 0:
                break;
            case 1:
                totalHeight += 50;
                break;
            case 2:
                break;
        }

        return totalHeight;

    }
}


[CustomPropertyDrawer(typeof(IntReference))]
public class IntReferenceDrawer : ReferenceDrawer
{
   
}

[CustomPropertyDrawer(typeof(BoolReference))]

public class BoolReferenceDrawer : ReferenceDrawer
{

}

[CustomPropertyDrawer(typeof(PercentageReference))]
public class PercentageReferenceDrawer : PropertyDrawer
{
    private readonly string[] popupOptions =
        { "Use Constant", "Use Variable" };

    /// <summary> Cached style to use to draw the popup button. </summary>
    private GUIStyle popupStyle;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("constantValue");
        SerializedProperty variable = property.FindPropertyRelative("variable");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

        useConstant.boolValue = result == 0;
        float min = 0.0f;
        float max = 1.0f;
        if (useConstant.boolValue)
        {
            EditorGUI.Slider(position,constantValue, min, max,GUIContent.none);
        }
        else
        {
            EditorGUI.PropertyField(position,
                useConstant.boolValue ? constantValue : variable,
                GUIContent.none);
        }
        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

}

public class ReferenceDrawer : PropertyDrawer
{
    private readonly string[] popupOptions =
        { "Use Constant", "Use Variable" };

    /// <summary> Cached style to use to draw the popup button. </summary>
    private GUIStyle popupStyle;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("constantValue");
        SerializedProperty variable = property.FindPropertyRelative("variable");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

        useConstant.boolValue = result == 0;

        EditorGUI.PropertyField(position,
            useConstant.boolValue ? constantValue : variable,
            GUIContent.none);

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}

