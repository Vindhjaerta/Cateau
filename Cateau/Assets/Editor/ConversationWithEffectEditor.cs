using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConversationWithEffects))]
class ConversationWithEffectsEditor : Editor
{
    SerializedProperty savepointIndex;
    SerializedProperty _nextNode;
    SerializedProperty characterNameTag;
    SerializedProperty nameEffects;
    SerializedProperty sentences;

    private static GUIContent
        moveButtonContent = new GUIContent("\u21b4", "move down"),
        duplicateButtonContent = new GUIContent("+", "duplicate"),
        deleteButtonContent = new GUIContent("-", "delete");

    private static int buttonWidth = 16, buttonHeight = 16;

    public void OnEnable()
    {
        savepointIndex = serializedObject.FindProperty("savepointIndex");
        _nextNode = serializedObject.FindProperty("_nextNode");

        characterNameTag = serializedObject.FindProperty("characterNameTag");
        nameEffects = serializedObject.FindProperty("nameEffects");
        sentences = serializedObject.FindProperty("sentences");
    }

    public override void OnInspectorGUI()
    {
        int indent = EditorGUI.indentLevel;
        serializedObject.Update();

        EditorGUILayout.PropertyField(savepointIndex);
        EditorGUILayout.PropertyField(_nextNode);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(characterNameTag);
        EditorGUILayout.PropertyField(nameEffects, true);
        EditorGUILayout.Space();

        SerializedProperty sentencesArray = sentences.FindPropertyRelative("Array.size");
        EditorGUILayout.PropertyField(sentencesArray, new GUIContent("Sentences"));

        for (int i = 0; i < sentencesArray.intValue; i++)
        {
            EditorGUILayout.PropertyField(sentences.GetArrayElementAtIndex(i));
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(moveButtonContent, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                sentences.MoveArrayElement(i, i + 1);
            }
            if (GUILayout.Button(duplicateButtonContent, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                sentences.InsertArrayElementAtIndex(i);
            }
            
            EditorGUILayout.LabelField(new GUIContent(""));
            if (GUILayout.Button(deleteButtonContent, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                sentences.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();

        }

        serializedObject.ApplyModifiedProperties();
        EditorGUI.indentLevel = indent;
    }

}