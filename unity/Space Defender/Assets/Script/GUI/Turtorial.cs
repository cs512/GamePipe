using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Turtorial : MonoBehaviour {
    public RectTransform rButton;
    public Transform transBut;
    public Transform transTex;
    public Transform tPanel;
    public Text tText;
    public Text aText;
    public RectTransform rAButton;
    public Transform transABut;
    private int i = 0;
    private int count = 0;
    public void chTrText() {
        i++;
        if (i == 1) {
            TouchControl tc = GameObject.Find("TouchControl").GetComponent<TouchControl>();
            tc.enableTouch = false;
            tText.text = "Here is your resource.";
            tText.fontSize = 22;
            rButton.sizeDelta = new Vector2(220f, 60f);
            transBut.position = new Vector3(-330, 0, 250);
            transTex.position = new Vector3(-330, 0, 250);
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().setFundColor(Color.red);
        }
        if (i == 2)
        {
            tText.text = "Here is your health.";
            tText.fontSize = 22;
            rButton.sizeDelta = new Vector2(220f, 60f);
            transBut.position = new Vector3(-230, 0, 250);
            transTex.position = new Vector3(-230, 0, 250);
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().setFundColor(Color.white);
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().setLiveColor(Color.red);
        }
        if (i == 3)
        {
            tText.text = "Also you can see the\nremaining enemy waves.";
            tText.fontSize = 22;
            rButton.sizeDelta = new Vector2(260f, 60f);
            transBut.position = new Vector3(-60, 0, 250);
            transTex.position = new Vector3(-60, 0, 250);
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().setLiveColor(Color.white);
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().setWaveColor(Color.red);
        }
        if (i == 4) {
            tText.text = "You can alter time here,\nalso exit easily.";
            transBut.position = new Vector3(250, 0, 250);
            transTex.position = new Vector3(240, 0, 250);
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().setWaveColor(Color.white);
            GameObject.Find("Pause").GetComponent<TimeControl>().setTimeColor(Color.red);
            GameObject.Find("EXIT").GetComponent<Exit>().setEColor(Color.red);
        }
        if (i == 5) {
            rButton.sizeDelta = new Vector2(260f, 60f);
            tText.text = "This is your base Planet,\nalso your source refiner.";
            tText.fontSize = 20;
            transBut.position = new Vector3(0, -50, 40);
            transTex.position = new Vector3(0, -50, 40);
            GameObject.Find("Pause").GetComponent<TimeControl>().setTimeColor(Color.white);
            GameObject.Find("EXIT").GetComponent<Exit>().setEColor(Color.white);
        }
        if (i == 6)
        {
            rButton.sizeDelta = new Vector2(260f, 60f);
            tText.text = "You can upgrade it \nto gain resources quickly.";
            tText.fontSize = 20;
            transBut.position = new Vector3(0, -50, 40);
            transTex.position = new Vector3(0, -50, 40);
        }
        if (i == 7)
        {
            rButton.sizeDelta = new Vector2(260f, 60f);
            tText.color = Color.red;
            tText.text = "Beware, you must protect\n it from enemies!";
            tText.fontSize = 20;
            transBut.position = new Vector3(0, -50, 40);
            transTex.position = new Vector3(0, -50, 40);
        }
        if (i == 8) {
            tPanel.gameObject.SetActive(false);
            rAButton.sizeDelta = new Vector2(360f, 60f);
            aText.fontSize = 22;
            aText.text = "Now drag around to view your arena.";
            var touch = GameObject.Find("TouchControl").GetComponent<TouchControl>();
            touch.enableTouch = true;
        }
        if (i == 9) {
            transBut.position = new Vector3(0, 0, 40);
            transTex.position = new Vector3(0, 0, 40);
            rButton.sizeDelta = new Vector2(200f, 80f);
            tText.color = Color.blue;
            tText.text = "Enemy is coming!\nDestory them now!";
            Time.timeScale = 1;
            Invoke("timeReturn", 2);
        }
    }
    public void aChTex() {
        i++;
        if(i == 1) {
            rAButton.sizeDelta = new Vector2(400f, 60f);
            transABut.position = new Vector3(-200,0,50);
            aText.text = "Touch one of your favourite green hexgon.";
            Button ab = GameObject.Find("aButton").GetComponent<Button>();
        }
        HexGrid hg = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        if((count!=hg.GetTurretCount())&&(i==1))
        {
            aText.text = "Now you can pick your favourite tower.";
            i += 1;
            Button ab = GameObject.Find("aButton").GetComponent<Button>();
            ab.interactable = true; 
        }
    }
    void timeReturn() {
        tText.text = "";
        rButton.sizeDelta = new Vector2(0f, 0f);
        tPanel.gameObject.SetActive(false);
    }
    void Gain() { 

}
    void stopTime() {
        Time.timeScale = 0;
    }
    void chColor() {//
        HexGrid hg = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        hg.buildableColor = Color.white;
    }
    void Start() {
        Invoke("stopTime", 0.5f);
        tText.text = "Hello, Commander!";
        tText.fontSize = 22;
        rButton.sizeDelta = new Vector2(220f, 60f);
        rAButton.sizeDelta = new Vector2(0f, 0f);
        HexGrid hg = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        count = hg.GetTurretCount();
    }

    // Update is called once per frame
    void Update() {

    }
}
