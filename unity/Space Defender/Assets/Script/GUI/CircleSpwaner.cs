using UnityEngine;
using System.Collections;

public class CircleSpwaner : MonoBehaviour {
    public static CircleSpwaner ins;
    public CircleMenu menuPrefab;

    void Awake()
    {
        ins = this;
    }
	// Use this for initialization
	public void SpawnMenu(Interactable obj)
    {
        CircleMenu newMenu = Instantiate(menuPrefab) as CircleMenu;
        newMenu.transform.SetParent(transform, false);
        newMenu.transform.localPosition = new Vector3(0f, 0f, 0f);
        newMenu.SpawnButtons(obj);
    }
}
