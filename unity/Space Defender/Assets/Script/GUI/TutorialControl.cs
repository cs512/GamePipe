using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TutorialControl : MonoBehaviour {
    public Sprite[] images;
    public Image image;
    public void loadTutorial() {
        int page = Convert.ToInt32(image.sprite.name) - 1;
        if (gameObject.name == "Prev") {
            if (page == 0) {
                page = 3;
            } else {
                page--;
            }
            image.sprite = images[page];
        } else if (gameObject.name == "Next") {
            if (page == 3) {
                page = 0;
            } else {
                page++;
            }
            image.sprite = images[page];
        } else if (gameObject.name == "MainMenu") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            SceneManager.LoadScene("opeing");
        }
    }
    void Start() {
        GameObject.Find("LoadingImage").GetComponent<Image>().enabled = false;
    }
}