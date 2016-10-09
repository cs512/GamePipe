using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Turtorial : MonoBehaviour {
    public Transform trans;
    public TextMesh tText;
    public void transPos1() {
        trans.position = new Vector3(-600, 0, 270); ;
    }
    public void chText1() {
        tText.text = "You can check your resource health \n and how many enemy's gates remain here.";
    }
    public void transPos2() {
        trans.position = new Vector3(0,0,0);
    }
    public void chText2() {
        tText.fontSize = 400;
        tText.color = Color.red;
        tText.text = "This is your sourcePlanet, also your basement.\n You can upgrade it to gain resources quickly.\n Be ware, you must protect it from enemies!";
    }
  
    // Use this for initialization
    void Start () {
        tText.text = "Hello, Commander!";
        tText.fontSize = 500;
        Invoke("transPos1", 2);
        Invoke("chText1", 2);
        Invoke("transPos2", 4);
        Invoke("chText2", 4);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
