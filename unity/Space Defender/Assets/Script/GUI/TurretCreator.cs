using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class TurretCreator : MonoBehaviour {

    public Image circle;
    public Image icon;
    public float turretCost;
    public string title;
    private GameObject newObject;
    private bool built = false;
    public TextMesh textMesh;
    // Use this for initialization
    void Update() {
        textMesh.text = turretCost.ToString();
    }
    public void InstantiateTurret() {
        Debug.Log("Clicked!");
        if (this.title == "trtLrg") {
            GameObject prefab = Resources.Load("Prefabs/Turrets/IonBlast", typeof(GameObject)) as GameObject;
            float cost = prefab.GetComponent<TurretBase>().turretCost;
            Debug.Log("Cost is" + cost);
            if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(cost)) {
                newObject = Instantiate(prefab, transform.parent.position, transform.localRotation) as GameObject;
                built = true;
            }

        } else if (this.title == "trtMd") {

            GameObject prefab = Resources.Load("Prefabs/Turrets/G250dual", typeof(GameObject)) as GameObject;
            float cost = prefab.GetComponent<TurretBase>().turretCost;
            Debug.Log("Cost is" + cost);
            if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(cost)) {
                newObject = Instantiate(prefab, transform.parent.position, transform.localRotation) as GameObject;
                built = true;
            }
        } else if (this.title == "trtSml") {

            GameObject prefab = Resources.Load("Prefabs/Turrets/G350", typeof(GameObject)) as GameObject;
            float cost = prefab.GetComponent<TurretBase>().turretCost;
            Debug.Log("Cost is" + cost);
            if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(cost)) {
                newObject = Instantiate(prefab, transform.parent.position, transform.localRotation) as GameObject;
            built = true;
            }
        } else if (this.title == "frzTrt") {

            GameObject prefab = Resources.Load("Prefabs/Turrets/FreezePulse", typeof(GameObject)) as GameObject;
            float cost = prefab.GetComponent<TurretBase>().turretCost;
            Debug.Log("Cost is" + cost);
            if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(cost)) {
                newObject = Instantiate(prefab, transform.parent.position, transform.localRotation) as GameObject;
                built = true;
            }
        } else if (this.title == "mssLnchr") {

            GameObject prefab = Resources.Load("Prefabs/Turrets/missle_launcher", typeof(GameObject)) as GameObject;
            float cost = prefab.GetComponent<TurretBase>().turretCost;
            Debug.Log("Cost is" + cost);
            if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(cost)) {
                newObject = Instantiate(prefab, transform.parent.position, transform.localRotation) as GameObject;
                built = true;
            }

        } else if (this.title == "shield") {

            GameObject prefab = Resources.Load("Prefabs/Turrets/ShieldGenerator", typeof(GameObject)) as GameObject;
            float cost = prefab.GetComponent<TurretBase>().turretCost;
            Debug.Log("Cost is" + cost);
            if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(cost)) {
                newObject = Instantiate(prefab, transform.parent.position, transform.localRotation) as GameObject;
                built = true;
            }

        }
        if (built)
        {
            AudioSource audio = GameObject.Find("TurretBuildSound").GetComponent<AudioSource>();
            audio.Play();
        }
        Transform parentTransform = transform.parent;
        parentTransform.GetComponent<MenuSpawner>().DestroyMenu();
    }
}
