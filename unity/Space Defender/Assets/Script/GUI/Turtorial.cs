using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Turtorial : MonoBehaviour {
    public RectTransform rButton;
    public Button transButton;
    public Transform transBut;
    public Transform transTex;
    public Transform tPanel;
    public Text tText;
    private int i = 0;
    public void chTrText() {
        i++;
        if (i == 1) {
            tText.text = "You can check your resource health \nand how many enemy's gates here.";
            tText.fontSize = 22;
            rButton.sizeDelta = new Vector2(400f, 80f);
            transBut.position = new Vector3(-250, 0, 250);
            transTex.position = new Vector3(-260, 0, 250);
        }
        if (i == 2) {
            tText.text = "You can alter the time here,\n also you can exit easily.";
            transBut.position = new Vector3(250, 0, 250);
            transTex.position = new Vector3(240, 0, 250);
        }
        if (i == 3) {
            rButton.sizeDelta = new Vector2(300f, 160f);
            tText.text = "This is your sourcePlanet,\nalso your basement.\nYou can upgrade it \nto gain resources quickly.\nBeware, you must protect\n it from enemies!";
            tText.fontSize = 20;
            transBut.position = new Vector3(0, -50, 40);
            transTex.position = new Vector3(0, -50, 40);
        }
        if (i == 4) {
            rButton.sizeDelta = new Vector2(400f, 200f);
            tText.text = "Look at this white cycles,\npoint at it.\nFind one of your faviourate\nSACS (space auto-counterattack system).\nNanometer 3D Printer can build it real quick.\nAs a tutorial you can \nhave enough Resources now.";
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(-9799);
        }
        if (i == 5) {
            transBut.position = new Vector3(0, 0, 40);
            transTex.position = new Vector3(0, 0, 40);
            rButton.sizeDelta = new Vector2(200f, 80f);
            tText.color = Color.blue;
            tText.text = "Enemy is coming!\nDestory them now!";
            Time.timeScale = 1;
            Invoke("timeReturn", 2);
            Debug.Log("1423124124");
        }
    }
    void timeReturn() {
        tText.text = "";
        rButton.sizeDelta = new Vector2(0f, 0f);
        tPanel.gameObject.SetActive(false);
        Debug.Log("1afssa");
    }
    void Gain() { 

}
    void stopTime() {
        Time.timeScale = 0;
    }
    //public void transPos2() {
    //    trans.position = new Vector3(0,0,0);
    //}
    //public void chText2() {
    //    tText.fontSize = 400;
    //    tText.color = Color.red;
    //    tText.text = "This is your sourcePlanet, also your basement.\n You can upgrade it to gain resources quickly.\n Be ware, you must protect it from enemies!";
    //}

    // Use this for initialization
    void Start() {
        Invoke("stopTime", 0.5f);
        tText.text = "Hello, Commander!";
        tText.fontSize = 40;
        rButton.sizeDelta = new Vector2(360f, 100f);
    }

    // Update is called once per frame
    void Update() {

    }
}
