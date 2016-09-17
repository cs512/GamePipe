using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    
    [System.Serializable]
    public class Action
    {
        public Color color;
        public Sprite sprite;
        public string title;
    }
    public Action[] options;
	// Use this for initialization
	void OnMouseDown()
    {
        //spawn a menu
        CircleSpwaner.ins.SpawnMenu(this);
    }
}
