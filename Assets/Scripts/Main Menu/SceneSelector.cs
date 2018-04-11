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
    public Sprite Thumbnail;
}
