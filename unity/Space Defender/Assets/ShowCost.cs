using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowCost : MonoBehaviour {
    private CircleButton parentButton;
    public TextMesh textMesh;
    // Use this for initialization
    void Start () {
        parentButton = transform.GetComponentInParent<CircleButton>();
        if (parentButton.title == "trtLrg") {
            GameObject prefab = Resources.Load("Prefabs/IonBlast", typeof(GameObject)) as GameObject;
            textMesh.text = prefab.GetComponent<TurretBase>().turretCost.ToString();
        }
        else if (parentButton.title == "trtMd") {
            GameObject prefab = Resources.Load("Prefabs/G250dual", typeof(GameObject)) as GameObject;
            textMesh.text = prefab.GetComponent<TurretBase>().turretCost.ToString();
        }
        else if (parentButton.title == "trtSml") {
            GameObject prefab = Resources.Load("Prefabs/G350", typeof(GameObject)) as GameObject;
            textMesh.text = prefab.GetComponent<TurretBase>().turretCost.ToString();
        }
        else if (parentButton.title == "frzTrt") {
            GameObject prefab = Resources.Load("Prefabs/FreezePulse", typeof(GameObject)) as GameObject;
            textMesh.text = prefab.GetComponent<TurretBase>().turretCost.ToString();
        }
        else if (parentButton.title == "mssLnchr") {
            GameObject prefab = Resources.Load("Prefabs/missle_launcher", typeof(GameObject)) as GameObject;
            textMesh.text = prefab.GetComponent<TurretBase>().turretCost.ToString();
        }
    }
}
