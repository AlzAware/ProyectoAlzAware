using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(DatosAlfabeto))]
[CanEditMultipleObjects]
[System.Serializable]

public class DibujoAlfabeto : Editor
{
    private ReorderableList AlphabetNormalList;
    private ReorderableList AlphabetGreenList;
    private ReorderableList AlphabetRedList;

    private void OnEnable()
    {
        InitializeReordableList(ref AlphabetNormalList, "AlphabetNormal", "Alphabet Normal");
        InitializeReordableList(ref AlphabetGreenList, "AlphabetGreen", "Alphabet Green");
        InitializeReordableList(ref AlphabetRedList, "AlphabetRed", "Alphabet Red");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        AlphabetNormalList.DoLayoutList();
        AlphabetGreenList.DoLayoutList();
        AlphabetRedList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
    private void InitializeReordableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName),
            true, true, true, true);
        list.drawHeaderCallback = (Rect Rect) =>
        {
            EditorGUI.LabelField(Rect, listLabel);

        };
        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("letter"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + 70, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("image"), GUIContent.none);
        };
    }
}
