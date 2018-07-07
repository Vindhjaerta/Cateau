using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Crossroad))]
[CanEditMultipleObjects]
class CrossroadEditor : Editor//SceneTreeObjectEditor
{
    SerializedProperty savepointIndex;
    SerializedProperty _nextNode;

    SerializedProperty type;
    SerializedProperty catIdentifier;
    SerializedProperty catChoices;
    SerializedProperty stringChoices;


    public void OnEnable()
    {
        savepointIndex = serializedObject.FindProperty("savepointIndex");
        _nextNode = serializedObject.FindProperty("_nextNode");

        type = serializedObject.FindProperty("type");
        catChoices = serializedObject.FindProperty("catChoices");
        stringChoices = serializedObject.FindProperty("stringChoices");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        int indent = EditorGUI.indentLevel;
        Crossroad script = (Crossroad)target;
        serializedObject.Update();

        EditorGUILayout.PropertyField(savepointIndex);
        EditorGUILayout.PropertyField(_nextNode);

        EditorGUILayout.PropertyField(type);

        EditorGUILayout.Space();

        switch (script.type)
        {
            case Crossroad.ECrossroad.SaveableString:
                SerializedProperty stringArraySize = stringChoices.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(stringArraySize, new GUIContent("Choices"));

                EditorGUI.indentLevel++;
                if (stringArraySize.intValue > 0)
                {
                    for (int i = 0; i < stringArraySize.intValue; i++)
                    {
                        EditorGUILayout.PropertyField(stringChoices.GetArrayElementAtIndex(i).FindPropertyRelative("savedString"));
                        EditorGUILayout.PropertyField(stringChoices.GetArrayElementAtIndex(i).FindPropertyRelative("targetNode"));

                        if (stringArraySize.intValue > 1 && i < stringArraySize.intValue - 1) EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    }
                }


                        break;
            case Crossroad.ECrossroad.CatAffinity:
                SerializedProperty choiceArraySize = catChoices.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(choiceArraySize, new GUIContent("Choices"));

                EditorGUI.indentLevel++;
                if (choiceArraySize.intValue > 0)
                {
                    for (int i = 0; i < choiceArraySize.intValue; i++)
                    {
                        EditorGUILayout.PropertyField(catChoices.GetArrayElementAtIndex(i).FindPropertyRelative("catIdentifier"));
                        EditorGUILayout.PropertyField(catChoices.GetArrayElementAtIndex(i).FindPropertyRelative("targetNode"));

                        SerializedProperty comparators = catChoices.GetArrayElementAtIndex(i).FindPropertyRelative("comparators");
                        SerializedProperty comparatorArraySize = comparators.FindPropertyRelative("Array.size");
                        EditorGUILayout.PropertyField(comparatorArraySize, new GUIContent("Comparators"));

                        EditorGUI.indentLevel++;
                        if(comparatorArraySize.intValue > 0)
                        {
                            for (int j = 0; j < comparatorArraySize.intValue; j++)
                            {
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.PropertyField(comparators.GetArrayElementAtIndex(j).FindPropertyRelative("comparator"),new GUIContent(""));
                                EditorGUILayout.PropertyField(comparators.GetArrayElementAtIndex(j).FindPropertyRelative("compareValue"), new GUIContent(""));
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                        EditorGUI.indentLevel--;
                        if(choiceArraySize.intValue > 1 && i < choiceArraySize.intValue - 1) EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                        //EditorGUILayout.Space();
                        //EditorGUILayout.LabelField("");

                        
                    }
                }
                break;
            case Crossroad.ECrossroad.InSceneCatAffainity:
                SerializedProperty choiceArraySizeInScene = catChoices.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(choiceArraySizeInScene, new GUIContent("Choices"));

                EditorGUI.indentLevel++;
                if (choiceArraySizeInScene.intValue > 0)
                {
                    for (int i = 0; i < choiceArraySizeInScene.intValue; i++)
                    {
                        EditorGUILayout.PropertyField(catChoices.GetArrayElementAtIndex(i).FindPropertyRelative("catIdentifier"));
                        EditorGUILayout.PropertyField(catChoices.GetArrayElementAtIndex(i).FindPropertyRelative("targetNode"));

                        SerializedProperty comparators = catChoices.GetArrayElementAtIndex(i).FindPropertyRelative("comparators");
                        SerializedProperty comparatorArraySize = comparators.FindPropertyRelative("Array.size");
                        EditorGUILayout.PropertyField(comparatorArraySize, new GUIContent("Comparators"));

                        EditorGUI.indentLevel++;
                        if (comparatorArraySize.intValue > 0)
                        {
                            for (int j = 0; j < comparatorArraySize.intValue; j++)
                            {
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.PropertyField(comparators.GetArrayElementAtIndex(j).FindPropertyRelative("comparator"), new GUIContent(""));
                                EditorGUILayout.PropertyField(comparators.GetArrayElementAtIndex(j).FindPropertyRelative("compareValue"), new GUIContent(""));
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                        EditorGUI.indentLevel--;
                        if (choiceArraySizeInScene.intValue > 1 && i < choiceArraySizeInScene.intValue - 1) EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                        //EditorGUILayout.Space();
                        //EditorGUILayout.LabelField("");


                    }
                }
                break;
        }

        EditorGUI.indentLevel = indent;
        serializedObject.ApplyModifiedProperties();
    }
}

