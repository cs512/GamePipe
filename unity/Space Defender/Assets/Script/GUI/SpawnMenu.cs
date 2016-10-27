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

    private bool clicked = false;
    // Use this for initialization

    public void ControlMenuRender() {
        if (!clicked) {
            ShowMenu();
            clicked = true;
        } else {
            DestroyMenu();
            clicked = false;
        }
    }
    void DestroyMenu() {
        Transform parentTransform = transform.parent;
        foreach (Transform child in parentTransform) {
            if (child.tag == "Menu Button") {
                Destroy(child.gameObject);
            } else {
                Debug.Log(child.tag);
            }
        }
    }
    void ShowMenu() {
        for (int i = 0; i < options.Length; i++) {
            CreateTurret newButton = Instantiate(button) as CreateTurret;
            newButton.transform.SetParent(transform.parent, false);
            float theta = (2 * Mathf.PI / options.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localScale += new Vector3(5f, 5f, 5f);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 50f;
            //newButton.circle.color = options[i].color;
            newButton.icon.sprite = options[i].sprite;
            newButton.title = options[i].title;
            if (newButton.title == "trtLrg") {
                GameObject prefab = Resources.Load("Prefabs/IonBlast", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "trtMd") {
                GameObject prefab = Resources.Load("Prefabs/G250dual", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "trtSml") {
                GameObject prefab = Resources.Load("Prefabs/G350", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "frzTrt") {
                GameObject prefab = Resources.Load("Prefabs/FreezePulse", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
            } else if (newButton.title == "mssLnchr") {
                GameObject prefab = Resources.Load("Prefabs/missle_launcher", typeof(GameObject)) as GameObject;
                newButton.turretCost = prefab.GetComponent<TurretBase>().turretCost;
                Debug.Log("Launcher cost is " + newButton.turretCost);
            }
        }
    }
}
