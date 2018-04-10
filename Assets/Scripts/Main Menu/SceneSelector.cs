using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SceneSelector {
    
    [Tooltip("Scene filename, without extension")]
    public string Scene;

    [Tooltip("Display name, what you see in-game")]
    public string DisplayName;

    [Tooltip("In-game description")]
    public string Description;

    [Tooltip("Thumbnail to be used in-game")]
    public Texture Thumbnail;
}

/*
[CustomPropertyDrawer(typeof(SceneSelector))]
public class SceneSelectorDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var sceneNameRect = new Rect(position.x, position.y, position.width / 3f - 5f, position.height);
        var displayNameRect = new Rect(position.x + position.width / 3f + 5f, position.y, position.width / 3f - 5f, position.height);
        var descriptionRect = new Rect(position.x + position.width / 3f * 2f + 10f, position.y, position.width / 3f - 10f, position.height);

        EditorGUI.PropertyField(sceneNameRect, property.FindPropertyRelative("SceneName"), GUIContent.none);
        EditorGUI.PropertyField(displayNameRect, property.FindPropertyRelative("DisplayName"), GUIContent.none);
        EditorGUI.PropertyField(descriptionRect, property.FindPropertyRelative("Description"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
*/
