using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(EnemySpawnList))]
public class EnemySpawnListInspector : Editor {
    private ReorderableList rl;

    private void OnEnable() {
        rl = new ReorderableList(serializedObject, serializedObject.FindProperty("List"), true, true, true, true);

        rl.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = rl.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Enemy"), typeof(GameObject), new GUIContent("Enemy"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Amount"), new GUIContent("Amount"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2f + 4f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MaxDistance"), new GUIContent("Max Distance"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 3f + 6f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MinDistance"), new GUIContent("Min Distance"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 4f + 8f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Health"), new GUIContent("Health"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 5f + 10f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Damage"), new GUIContent("Damage"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 6f + 12f, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Speed"), new GUIContent("Speed"));
        };

        rl.onAddCallback = (ReorderableList list) => {
            var index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;
            var element = list.serializedProperty.GetArrayElementAtIndex(index);

            element.FindPropertyRelative("Amount").intValue = 1;
            element.FindPropertyRelative("MaxDistance").floatValue = 50f;
            element.FindPropertyRelative("MinDistance").floatValue = 5f;
            element.FindPropertyRelative("Health").floatValue = 5f;
            element.FindPropertyRelative("Damage").floatValue = 5f;
            element.FindPropertyRelative("Speed").floatValue = 1f;
        };

        rl.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Spawn");
        };

        rl.elementHeight *= 7;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        rl.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}