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
    public GameObject turret;
    private GameObject newObject;
    public TextMesh textMesh;
    // Use this for initialization
    // Update is called once per frame
    void Update() {
        textMesh.text = function.ToString();
    }

    public void ExecuteFunction() {
        if (this.title == "sell") {
            GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(0- turret.GetComponent<TurretBase>().turretCost);
            turret.GetComponent<TurretBase>().Sold();
        }
        else if (this.title == "upgrade") {
            int currentLevel = turret.GetComponent<TurretBase>().level + 1;
            if (currentLevel <= 3) {
                if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(currentLevel * 40)) {
                    turret.GetComponent<TurretBase>().level++;
                    AudioSource audio = GameObject.Find("TurretUpgradeSound").GetComponent<AudioSource>();
                    audio.Play();
                }
            }
        }
        Transform parentTransform = transform.parent;
        parentTransform.GetComponent<SpawnMenu>().DestroyMenu();
    }
}
