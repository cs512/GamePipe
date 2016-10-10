using UnityEngine;
using System.Collections;

public class subMenu : MonoBehaviour {

    private GameObject newObject;
    public GameObject panel;

    public void createSubMenu() {
        Object prefab = Resources.Load("Prefabs/levels", typeof(GameObject)) as GameObject;
        newObject = Instantiate(prefab, panel.transform.position, panel.transform.rotation) as GameObject;
        Destroy(panel);
    }
}
