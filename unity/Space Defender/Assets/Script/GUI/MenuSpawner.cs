using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSpawner : MonoBehaviour {
    [System.Serializable]
    public class Action {
        public Color color;
        public Sprite sprite;
        public string title;
    }
    public Action[] options;
    public Action[] turretOptions;
    public TurretCreator button;
    public TurretHandler turretButton;

    bool menuShowing = false;
    List<Action> buildingOption;

    void Start() {

    }

    void Awake() {
        int mode = Toolbox.FindObjectOfType<LevelManager>().GetMode ();
        if (mode== 0) {
            this.SetBuildingMask(Toolbox.FindObjectOfType<LevelManager>().GetCurrentLevel().turretMask);
        } else if(mode== 1) {
            this.SetBuildingMask(Toolbox.FindObjectOfType<LevelManager>().GetCurrentLevelSki().turretMask);
        } else {
            return;
        }
    }

    public bool MenuShowing {
        get {
            return menuShowing;
        }
    }
    // Use this for initialization

    public void SetBuildingMask(int mask) { //0b1
        buildingOption = new List<Action>();
        for (int i = 0; i < options.Length; ++i) {
            if ((1 << i & mask) != 0) {
                buildingOption.Add(options[i]);
            }
        }
    }

    public void TriggerMenu() {
        if (menuShowing) {
            DestroyMenu();
        } else {
            ShowMenu();
        }
        menuShowing = !menuShowing;
    }

    public void DestroyMenu() {
        menuShowing = false;
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform) {
            if (child.tag == "Menu Button") {
				if (child.GetComponent<TurretHandler>()){
					GameObject rangeDisplay = child.GetComponent<TurretHandler>().turret.GetComponent<TurretBase>().rangeDisplay;
					rangeDisplay.SetActive(false);
				}
                Destroy(child.gameObject);
            } else {
                Debug.Log(child.tag);
            }
        }
    }

    public void ShowMenu() {
        menuShowing = true;
        for (int i = 0; i < buildingOption.Count; i++) {
            TurretCreator newButton = Instantiate(button) as TurretCreator;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / buildingOption.Count) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localScale += new Vector3(5f, 5f, 5f);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 50f;
            newButton.transform.localPosition -= new Vector3(0f, 0f, 15f);
            //newButton.circle.color = options[i].color;
            newButton.icon.sprite = buildingOption[i].sprite;
            newButton.title = buildingOption[i].title;
            if (newButton.title == "trtLrg") {
                GameObject prefab = Resources.Load("Prefabs/Turrets/IonBlast", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "trtMd") {
                GameObject prefab = Resources.Load("Prefabs/Turrets/G250dual", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "trtSml") {
                GameObject prefab = Resources.Load("Prefabs/Turrets/G350", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "frzTrt") {
                GameObject prefab = Resources.Load("Prefabs/Turrets/FreezePulse", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "mssLnchr") {
                GameObject prefab = Resources.Load("Prefabs/Turrets/missle_launcher", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "shield") {
                GameObject prefab = Resources.Load("Prefabs/Turrets/ShieldGenerator", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            }
        }
    }

    public void ShowTurretMenu(GameObject turret) {

		GameObject rangeDisplay = turret.GetComponent<TurretBase>().rangeDisplay;
		rangeDisplay.SetActive(true);

        menuShowing = true;
        for (int i = 0; i < turretOptions.Length; i++) {
            Debug.Log("Show icon!");
            TurretHandler newButton = Instantiate(turretButton) as TurretHandler;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / turretOptions.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localScale += new Vector3(5f, 5f, 5f);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 70f;
            newButton.transform.localPosition -= new Vector3(0f, 0f, 15f);
            newButton.icon.sprite = turretOptions[i].sprite;
            newButton.title = turretOptions[i].title;
            newButton.turret = turret;
            if (newButton.title == "sell") {
                newButton.function = "Sell";
            } else if (newButton.title == "upgrade") {
                newButton.function = "Upgrade";
            }
        }
    }
}
