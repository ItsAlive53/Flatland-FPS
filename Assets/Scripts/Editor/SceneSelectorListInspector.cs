using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(SceneSelectorList))]
public class SceneSelectorListInspector : Editor {

    private ReorderableList rl;

    private void OnEnable() {
        rl = new ReorderableList(serializedObject, serializedObject.FindProperty("List"), true, true, true, true);

        rl.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = rl.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 2;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Scene"), new GUIContent("Scene Name"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("DisplayName"), new GUIContent("Displayed Name"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2f + 4f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Description"), new GUIContent("Description"));
            EditorGUI.ObjectField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 3f + 6f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Thumbnail"), new GUIContent("Thumbnail"));
        };

        rl.onAddCallback = (ReorderableList list) => {
            var index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;
            var element = list.serializedProperty.GetArrayElementAtIndex(index);

            element.FindPropertyRelative("Scene").stringValue = "Scene Name";
            element.FindPropertyRelative("DisplayName").stringValue = "Displayed Name";
            element.FindPropertyRelative("Description").stringValue = "Description";
        };

        rl.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Scenes");
        };

        rl.elementHeight *= 4;
    }
    
    public override void OnInspectorGUI() {
        serializedObject.Update();
        rl.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}