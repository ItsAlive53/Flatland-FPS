using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(WaveList))]
public class WaveListInspector : Editor {
    private ReorderableList rl;

    private void OnEnable() {
        rl = new ReorderableList(serializedObject, serializedObject.FindProperty("List"), true, true, true, true);

        rl.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = rl.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.ObjectField(new Rect(rect.x + 2, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Enemies"), new GUIContent("Spawns"));
        };

        rl.onAddCallback = (ReorderableList list) => {
            var index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;
        };

        rl.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Waves");
        };
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        rl.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}