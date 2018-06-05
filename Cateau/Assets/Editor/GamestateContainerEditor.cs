using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameStateContainer))]
class GamestateContainerEditor : Editor
{
    /*
    private StringReference _filename;

    public bool[] activePhotos;

    [SerializeField]
    public SettingsContainer settings;

    public string scene;
    public int savepointIndex;

    public UnityDictionary<int> affinity;
    public UnityDictionary<string> names;

    public List<string> savableStrings;     
     */

    SerializedProperty _filename;
    SerializedProperty activePhotos;
    SerializedProperty settings;
    SerializedProperty scene;
    SerializedProperty savepointIndex;
    SerializedProperty savableStrings;
    SerializedProperty typingSpeeds;
    SerializedProperty fontSizes;

    SerializedProperty affinityTags;
    SerializedProperty affinityValues;
    SerializedProperty nameTags;
    SerializedProperty nameValues;


    public void OnEnable()
    {
        _filename = serializedObject.FindProperty("_filename");
        activePhotos = serializedObject.FindProperty("activePhotos");
        settings = serializedObject.FindProperty("settings");
        scene = serializedObject.FindProperty("scene");
        savepointIndex = serializedObject.FindProperty("savepointIndex");
        savableStrings = serializedObject.FindProperty("savableStrings");
        typingSpeeds = serializedObject.FindProperty("typingSpeeds");
        fontSizes = serializedObject.FindProperty("fontSizes");

        affinityTags = serializedObject.FindProperty("affinityTags");
        affinityValues = serializedObject.FindProperty("affinityValues");
        nameTags = serializedObject.FindProperty("nameTags");
        nameValues = serializedObject.FindProperty("nameValues");
    }

    public override void OnInspectorGUI()
    {
        int indent = EditorGUI.indentLevel;
        GameStateContainer script = (GameStateContainer)target;
        serializedObject.Update();

        EditorGUILayout.PropertyField(_filename);
        EditorGUILayout.PropertyField(savepointIndex);
        EditorGUILayout.PropertyField(scene);
        EditorGUILayout.PropertyField(settings, true);
        EditorGUILayout.PropertyField(typingSpeeds,true);
        EditorGUILayout.PropertyField(fontSizes,true);
        EditorGUILayout.PropertyField(activePhotos, true);
        EditorGUILayout.PropertyField(savableStrings, true);


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Affinity"));
        EditorGUILayout.LabelField(new GUIContent("Names"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            affinityTags.arraySize++;
            affinityValues.arraySize++;
        }
        if (GUILayout.Button("Remove"))
        {
            if (affinityTags.arraySize > 0)
            {
                affinityTags.arraySize--;
                affinityValues.arraySize--;
            }
        }
        if (GUILayout.Button("Add"))
        {
            nameTags.arraySize++;
            nameValues.arraySize++;
        }
        if (GUILayout.Button("Remove"))
        {
            if (nameTags.arraySize > 0)
            {
                nameTags.arraySize--;
                nameValues.arraySize--;
            }
        }
        EditorGUILayout.EndHorizontal();

        int count = affinityTags.arraySize >= nameTags.arraySize ? affinityTags.arraySize : nameTags.arraySize;

        for (int i = 0; i < count; i++)
        {


            EditorGUILayout.BeginHorizontal();
            if (affinityTags.arraySize > 0 && affinityTags.arraySize - 1 >= i)
            {
                EditorGUILayout.PropertyField(affinityTags.GetArrayElementAtIndex(i), new GUIContent(""));
                EditorGUILayout.PropertyField(affinityValues.GetArrayElementAtIndex(i), new GUIContent(""));
            }
            else EditorGUILayout.LabelField(new GUIContent(""));
            if (nameTags.arraySize > 0 && nameTags.arraySize - 1 >= i)
            {
                EditorGUILayout.PropertyField(nameTags.GetArrayElementAtIndex(i), new GUIContent(""));
                EditorGUILayout.PropertyField(nameValues.GetArrayElementAtIndex(i), new GUIContent(""));
            }
            else EditorGUILayout.LabelField(new GUIContent(""));
            EditorGUILayout.EndHorizontal();
        }

        if(affinityTags.arraySize == 0 && nameTags.arraySize == 0)
        {
            EditorGUILayout.LabelField(new GUIContent("<empty>"));
        }

        //

        EditorGUI.indentLevel = indent;
        serializedObject.ApplyModifiedProperties();
    }
}

