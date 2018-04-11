using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuImage : MonoBehaviour {

    public Sprite Image;

    private void Update() {
        GetComponent<Image>().sprite = Image;
        GetComponent<Image>().color = Color.white;
        GetComponent<Image>().preserveAspect = true;
    }
}
