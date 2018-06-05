using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(ConversationWithEffects.GUISentence))]
public class GUISentenceEditor : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);
        int indent = EditorGUI.indentLevel;




        EditorGUI.indentLevel = 1;
        SerializedProperty text = property.FindPropertyRelative("text");
        SerializedProperty effects = property.FindPropertyRelative("effects");
        float rowHeight = 16;

        Rect textRect = new Rect(position.x, position.y, position.width * 0.5f, EditorGUI.GetPropertyHeight(text, label, true));
        EditorGUI.PropertyField(textRect, text, GUIContent.none);

        Rect effectRect = new Rect(position.x + position.width * 0.5f, position.y, position.width * 0.5f, rowHeight);
        EditorGUI.PropertyField(effectRect, effects, GUIContent.none, false);
        EditorGUI.LabelField(effectRect, new GUIContent("Effects"));
        if (effects.isExpanded)
        {

            SerializedProperty arraySize = effects.FindPropertyRelative("Array.size");
            EditorGUI.PropertyField(new Rect(effectRect.x, effectRect.y + rowHeight, effectRect.width, rowHeight), arraySize);


            for (int i = 0; i < effects.arraySize; i++)
            {
                SerializedProperty prop = effects.GetArrayElementAtIndex(i);
                EditorGUI.PropertyField(new Rect(effectRect.x, effectRect.y + (rowHeight * 2) + (rowHeight * i), effectRect.width, rowHeight), prop, new GUIContent(""));


            }

        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty text = property.FindPropertyRelative("text");
        SerializedProperty effects = property.FindPropertyRelative("effects");
        float textHeight = EditorGUI.GetPropertyHeight(text, label, true);
        float effectsHeight = EditorGUI.GetPropertyHeight(effects, label, true);

        return textHeight > effectsHeight ? textHeight : effectsHeight;
    }
}