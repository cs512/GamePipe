using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Turtorial : MonoBehaviour {
    public RectTransform rButton;
    public Button transButton;
    public Transform transBut;
    public Transform transTex;
    public Text tText;
    private int i = 0;
    public void chTrText() {
        i++;
        if(i == 1) {
            tText.text = "You can check your resource health \nand how many enemy's gates here.";
            tText.fontSize = 25;
            transBut.position = new Vector3(-300, 0, 250);
            transTex.position = new Vector3(-300, 0, 250);
        }
        if(i == 2) {
            tText.text = "This is your sourcePlanet, also your basement.\nYou can upgrade it to gain resources quickly.\nBeware, you must protect it from enemies!";
            tText.color = Color.red;
            tText.fontSize = 25;
            transBut.position = new Vector3(-200, 0, 40);
            transTex.position = new Vector3(-200, 0, 40);
        }
        if (i == 3)
        {
            rButton.sizeDelta=new Vector2(400f,300f);
            tText.text = "Now try to point on your planet.\nFind one of your faviourate SACS\n(space auto-counterattack system).\nHold and drag it to a good position.\nOnce you finished,click the text.";
        }
        if(i ==4) {
            tText.color = Color.blue;
            tText.text = "Enemy is coming!\nMaybe one is far from enough.";
            Time.timeScale = 1;
            Invoke("timeReturn", 5);
            Debug.Log("1423124124");
        }
    }
    void timeReturn()
    {
        tText.text = "";
        rButton.sizeDelta = new Vector2(0f, 0f);
        Debug.Log("1afssa");

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
    void Start () {
        tText.text = "Hello, Commander!";
        tText.fontSize = 40;
        Time.timeScale = 0;


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
