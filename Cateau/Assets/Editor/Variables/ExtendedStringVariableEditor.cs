using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExtendedStringVariable))]
public class ExtendedStringVariableEditor : Editor
{
    SerializedProperty value;

    public void OnEnable()
    {
        value = serializedObject.FindProperty("value");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //GUIStyle style = new GUIStyle();
        //style.wordWrap = true;
        //style = GUI.skin.textArea;
        //style.wordWrap = true;
        //value.stringValue = EditorGUILayout.TextArea(value.stringValue, style, GUILayout.MaxHeight(200));

        EditorStyles.textArea.wordWrap = true;
        value.stringValue = EditorGUILayout.TextArea(value.stringValue,EditorStyles.textArea, GUILayout.MaxHeight(300));

        serializedObject.ApplyModifiedProperties();
    }
}