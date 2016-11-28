﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadSceneSelection : MonoBehaviour {
    public void loadScene() {
        if (gameObject.name == "mainMenu") {
            SceneManager.LoadScene("opeing");
        } else if (gameObject.name == "tutorial") {
            SceneManager.LoadScene("Tutorial");
        } else if (gameObject.name == "Story") {
            Toolbox.Instance.GetComponent<LevelManager>().LoadData(1);
            SceneManager.LoadScene("levelSelectionSki");
        } else if(gameObject.name == "Ski"){
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().LoadData(0);
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevel(1);
        } else if (gameObject.name == "level1Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(1);
        } else if (gameObject.name == "level2Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(2);
        } else if (gameObject.name == "level3Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(3);
        } else if (gameObject.name == "level4Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(4);
        } else if (gameObject.name == "level5Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(5);
        } else if (gameObject.name == "level6Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(6);
        }

    }
    void Start() {
        GameObject.Find("LoadingImage").GetComponent<Image>().enabled = false;
    }
}