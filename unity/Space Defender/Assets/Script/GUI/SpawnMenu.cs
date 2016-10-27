using UnityEngine;
using System.Collections;

public class SpawnMenu : MonoBehaviour {
    [System.Serializable]
    public class Action {
        public Color color;
        public Sprite sprite;
        public string title;
    }
    public Action[] options;
    public CreateTurret button;
    bool menuShowing = false;

    public bool MenuShowing {
        get {
            return menuShowing;
        }
    }
    // Use this for initialization

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
                Destroy(child.gameObject);
            } else {
                Debug.Log(child.tag);
            }
        }
    }

    public void ShowMenu() {
        menuShowing = true;
        for (int i = 0; i < options.Length; i++) {
            CreateTurret newButton = Instantiate(button) as CreateTurret;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / options.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localScale += new Vector3(5f, 5f, 5f);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 50f;
            newButton.transform.localPosition -= new Vector3(0f, 0f, 15f);
            //newButton.circle.color = options[i].color;
            newButton.icon.sprite = options[i].sprite;
            newButton.title = options[i].title;
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
                Debug.Log("Launcher cost is " + newButton.turretCost);
            }
        }
    }
}
