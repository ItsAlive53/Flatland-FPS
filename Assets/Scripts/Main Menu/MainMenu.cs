using System.Collections;
using System.Collections.Generic;
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

    private SceneSelectorList Scenes;

    private void Awake() {
        Scenes = GetComponent<SceneSelectorList>();

        if (!Scenes) {
            Debug.LogErrorFormat("No SceneSelectorList component found in {0}", name);
        }

        if (MainMenuCanvas) {
            MainMenuCanvas.gameObject.SetActive(true);
        }

        if (OptionsCanvas) {
            OptionsCanvas.gameObject.SetActive(false);
        }

        if (LevelSelectionCanvas) {
            LevelSelectionCanvas.gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (StartButton) {
            if (StartButton.WasClicked) {
                StartButton.WasClicked = false;

                if (MainMenuCanvas && LevelSelectionCanvas) {
                    MainMenuCanvas.gameObject.SetActive(false);
                    LevelSelectionCanvas.gameObject.SetActive(true);
                }
            }
        }

        if (OptionsButton) {
            if (OptionsButton.WasClicked) {
                OptionsButton.WasClicked = false;
                
                if (MainMenuCanvas && OptionsCanvas) {
                    MainMenuCanvas.gameObject.SetActive(false);
                    OptionsCanvas.gameObject.SetActive(true);
                }
            }
        }

        if (ExitButton) {
            if (ExitButton.WasClicked) {
                ExitButton.WasClicked = false;
                Application.Quit();
            }
        }

        if (OptionsReturnToMenuButton) {
            if (OptionsReturnToMenuButton.WasClicked) {
                OptionsReturnToMenuButton.WasClicked = false;

                if (MainMenuCanvas && OptionsCanvas) {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        if (LevelSelectionReturnToMenuButton) {
            if (LevelSelectionReturnToMenuButton.WasClicked) {
                LevelSelectionReturnToMenuButton.WasClicked = false;

                if (MainMenuCanvas && LevelSelectionCanvas) {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}
