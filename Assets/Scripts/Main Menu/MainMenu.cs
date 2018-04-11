using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    [Header("Main Menu")]
    public Canvas MainMenuCanvas;
    public MainMenuButton StartButton;
    public MainMenuButton OptionsButton;
    public MainMenuButton ExitButton;

    [Header("Options Menu")]
    public Canvas OptionsCanvas;
    public MainMenuButton OptionsReturnToMenuButton;

    [Header("Level Selector Menu")]
    public Canvas LevelSelectionCanvas;
    public MainMenuButton LevelSelectionReturnToMenuButton;
    public MainMenuButton PreviousLevelButton;
    public MainMenuButton NextLevelButton;
    public MainMenuButton StartLevelButton;
    public MainMenuImage LevelThumbnail;
    public Text LevelName;
    public Text LevelDescription;

    private Canvas activeCanvas;

    private SceneSelectorList Scenes;

    // Level selection vars
    private int index;
    private SceneSelector selectedScene;

    private void Awake() {
        Scenes = GetComponent<SceneSelectorList>();
        index = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!Scenes) {
            Debug.LogErrorFormat("No SceneSelectorList component found in {0}", name);
        }

        InitMainMenu();
    }

    private void Update() {
    }

    private void Exit() {
        Application.Quit();
    }

    private void ReturnToMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void InitMainMenu() {
        DisableCanvases();

        MainMenuCanvas.gameObject.SetActive(true);
        activeCanvas = MainMenuCanvas;

        OptionsButton.OnClick += InitOptions;

        StartButton.OnClick += InitLevelSelector;

        ExitButton.OnClick += Exit;
    }

    private void InitOptions() {
        DisableCanvases();

        OptionsCanvas.gameObject.SetActive(true);
        activeCanvas = OptionsCanvas;

        OptionsReturnToMenuButton.OnClick += ReturnToMenu;
    }

    private void InitLevelSelector() {
        DisableCanvases();

        LevelSelectionCanvas.gameObject.SetActive(true);
        activeCanvas = LevelSelectionCanvas;

        index = 0;
        selectedScene = Scenes.List.First();

        UpdateLevelSelector();

        LevelSelectionReturnToMenuButton.OnClick += ReturnToMenu;

        PreviousLevelButton.OnClick += () => {
            if (index <= 0) {
                index = Scenes.List.Count;
            }

            index--;

            selectedScene = Scenes.List.ElementAt(index);

            UpdateLevelSelector();
        };

        NextLevelButton.OnClick += () => {
            index++;

            if (index >= Scenes.List.Count) {
                index = 0;
            }

            selectedScene = Scenes.List.ElementAt(index);

            UpdateLevelSelector();
        };
    }

    private void UpdateLevelSelector() {
        if (LevelThumbnail) {
            LevelThumbnail.Image = selectedScene.Thumbnail;
        }

        if (LevelName) {
            LevelName.text = selectedScene.DisplayName;
        }

        if (LevelDescription) {
            LevelDescription.text = selectedScene.Description;
        }

        StartLevelButton.OnClick = () => {
            UnityEngine.SceneManagement.SceneManager.LoadScene(selectedScene.Scene);
        };
    }

    private void DisableCanvases() {
        if (MainMenuCanvas) {
            MainMenuCanvas.gameObject.SetActive(false);
        }

        if (OptionsCanvas) {
            OptionsCanvas.gameObject.SetActive(false);
        }

        if (LevelSelectionCanvas) {
            LevelSelectionCanvas.gameObject.SetActive(false);
        }

        activeCanvas = null;
    }
}
