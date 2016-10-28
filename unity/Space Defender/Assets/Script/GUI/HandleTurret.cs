using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class HandleTurret : MonoBehaviour {

    public Image circle;
    public Image icon;
    public string title;
    public string function;
    private GameObject newObject;
    public TextMesh textMesh;
    // Use this for initialization
    // Update is called once per frame
    void Update() {
        textMesh.text = function.ToString();
    }

    public void ExecuteFunction() {
        if (this.title == "sell") {
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(0-this.transform.parent.GetComponent<TurretBase>().turretCost);
            this.transform.parent.GetComponent<TurretBase>().DestorySelf();
        }
        else if (this.title == "upgrade") {
            int currentLevel = this.transform.parent.GetComponent<TurretBase>().levelUp;
            if (currentLevel <= 2) {
                if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(currentLevel * 20)) {
                    this.transform.parent.GetComponent<TurretBase>().levelUp++;
                }
            }
        }
    }
}
