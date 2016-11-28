﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {
    public RectTransform gameEnd;
    public Transform tTrishade1;
    public Transform tTrishade2;
    public Transform tTrishade3;
    public Renderer tri1;
    public Renderer tri2;
    public Renderer tri3;
    public Renderer triShade1;
    public Renderer triShade2;
    public Renderer triShade3;
    public RectTransform retry;
    public RectTransform nextLevel;
    public RectTransform goBack;

    LevelManager lvlMgr;
    public void Retry() {// that is a reloading of this scene
        try {
            Time.timeScale = 1;
            lvlMgr.ReloadLevel();
        } catch {
            Debug.Log("wrong with LevelManager.cs");
        }
    }
    public void Next() {//To the next level.
        if (lvlMgr.JumpToNextLevel() == false) {
            Time.timeScale = 1;
            Debug.Log("No Next Level!");
        }
        else {
            Time.timeScale = 1;
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                lvlMgr.JumpLevel(1);
            }
            //else
                //lvlMgr.JumpToNextLevel();
        }

    }
    public void Back() {//go back to the Level Selection
        Time.timeScale = 1;
        if (lvlMgr.GetMode() == 0)
        {
            SceneManager.LoadScene("opeing");
        }
        else
            SceneManager.LoadScene("levelSelectionSki");
    }
    public void HideEnd() {
        gameEnd.sizeDelta = new Vector2(0, 0);
        triShade1.GetComponent<Renderer>().enabled = false;
        triShade2.GetComponent<Renderer>().enabled = false;
        triShade3.GetComponent<Renderer>().enabled = false;
        tri1.GetComponent<Renderer>().enabled = false;
        tri2.GetComponent<Renderer>().enabled = false;
        tri3.GetComponent<Renderer>().enabled = false;
        retry.sizeDelta = new Vector2(0, 0);
        nextLevel.sizeDelta = new Vector2(0, 0);
        goBack.sizeDelta = new Vector2(0, 0);
    }
    public void ShowEnd(float some) {
        Time.timeScale = 0;
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            gameEnd.sizeDelta = new Vector2(600, 320);
            triShade1.GetComponent<Renderer>().enabled = true;
            triShade2.GetComponent<Renderer>().enabled = true;
            triShade3.GetComponent<Renderer>().enabled = true;
            tri1.GetComponent<Renderer>().enabled = true;
            tri2.GetComponent<Renderer>().enabled = true;
            tri3.GetComponent<Renderer>().enabled = true;
            nextLevel.localPosition = new Vector3(-160, -50, -6);
            //retry.localPosition = new Vector3(-160, -50, -6);
            goBack.localPosition = new Vector3(160, -50, -6);
            retry.sizeDelta = new Vector2(0, 0);
            //nextLevel.sizeDelta = new Vector2(80, 60);
            goBack.sizeDelta = new Vector2(120, 80);
            if (some == 0)
            {//u shall not pass
                print("no nono1");
                tri1.GetComponent<Renderer>().enabled = false;
                tTrishade1.localPosition = new Vector3(-100, 50, -7);
                tTrishade2.localPosition = new Vector3(0, 50, -7);
                tTrishade3.localPosition = new Vector3(100, 50, -7);
                //nextLevel.sizeDelta = new Vector2(80, 30);
            }
            if (some == 1)
            {//ok
                tTrishade1.localPosition = new Vector3(-100, 50, -7);
                tTrishade2.localPosition = new Vector3(0, 50, -7);
                //tTrishade3.position = new Vector3(80, -6, 30);
                nextLevel.sizeDelta = new Vector2(120, 80);
            }
            if (some == 2)
            {//good
                tTrishade1.localPosition = new Vector3(-100, 50, -7);
                //tTrishade2.position = new Vector3(0, -6, 30);
                //tTrishade3.position = new Vector3(80, -6, 30);
                nextLevel.sizeDelta = new Vector2(120, 80);
            }
            if (some == 3)
            {//execllent
             //tTrishade1.position = new Vector3(-80, -6, 30);
             //tTrishade2.position = new Vector3(0, -6, 30);
             //tTrishade3.position = new Vector3(80, -6, 30);
                nextLevel.sizeDelta = new Vector2(120, 80);
            }
        }
        else
        {
            gameEnd.sizeDelta = new Vector2(600, 320);
            triShade1.GetComponent<Renderer>().enabled = true;
            triShade2.GetComponent<Renderer>().enabled = true;
            triShade3.GetComponent<Renderer>().enabled = true;
            tri1.GetComponent<Renderer>().enabled = true;
            tri2.GetComponent<Renderer>().enabled = true;
            tri3.GetComponent<Renderer>().enabled = true;
            retry.localPosition = new Vector3(-160, -50, -6);
            goBack.localPosition = new Vector3(160, -50, -6);
            retry.sizeDelta = new Vector2(120, 80);
            //nextLevel.sizeDelta = new Vector2(80, 60);
            goBack.sizeDelta = new Vector2(120, 80);
            if (some == 0)
            {//u shall not pass
                print("nononon1");
                tri1.GetComponent<Renderer>().enabled = false;
                tTrishade1.localPosition = new Vector3(-100, 50, -7);
                tTrishade2.localPosition = new Vector3(0, 50, -7);
                tTrishade3.localPosition = new Vector3(100, 50, -7);
                //nextLevel.sizeDelta = new Vector2(80, 30);
            }
            if (some == 1)
            {//ok
                tTrishade1.localPosition = new Vector3(-100, 50, -7);
                tTrishade2.localPosition = new Vector3(0, 50, -7);
                //tTrishade3.position = new Vector3(80, -6, 30);
                nextLevel.sizeDelta = new Vector2(120, 80);
            }
            if (some == 2)
            {//good
                tTrishade1.localPosition = new Vector3(-100, 50, -7);
                //tTrishade2.position = new Vector3(0, -6, 30);
                //tTrishade3.position = new Vector3(80, -6, 30);
                nextLevel.sizeDelta = new Vector2(120, 80);
            }
            if (some == 3)
            {//execllent
             //tTrishade1.position = new Vector3(-80, -6, 30);
             //tTrishade2.position = new Vector3(0, -6, 30);
             //tTrishade3.position = new Vector3(80, -6, 30);
                nextLevel.sizeDelta = new Vector2(120, 80);
            }
        }
    }

    // Use this for initialization
    void Start() {
        lvlMgr = Toolbox.Instance.GetOrAddComponent<LevelManager>();
        HideEnd();
    }

    // Update is called once per frame
    void Update() {

    }
}
