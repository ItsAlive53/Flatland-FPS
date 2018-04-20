using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public Image HighlightImage;

    [Range(0, 1f)]
    public float FadeInStep;

    [Range(0, 1f)]
    public float FadeOutStep;

    [HideInInspector]
    public Action OnClick;

    private float opacity;
    private bool hovered;

    private void OnEnable() {
        opacity = 0;
    }

    private void Start() {
        opacity = 0;
    }

    private void Update() {
        if (hovered && opacity < 1f) {
            opacity += FadeInStep;
            if (opacity > 1f) {
                opacity = 1f;
            }
        }

        if (!hovered && opacity > 0) {
            opacity -= FadeOutStep;
            if (opacity < 0) {
                opacity = 0;
            }
        }

        if (HighlightImage) {
            HighlightImage.color = new Color(255f, 255f, 255f, opacity);
        }
    }

    public void OnPointerEnter(PointerEventData evt) {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData evt) {
        hovered = false;
    }

    public void OnPointerClick(PointerEventData evt) {
        if (OnClick != null) OnClick();
    }
}
