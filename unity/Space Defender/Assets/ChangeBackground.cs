using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour {

    public Image image;
    [System.Serializable]
    public class Backgrounds
    {
        public Sprite sprite;
    }
    public Backgrounds[] background;
    // Use this for initialization
    void Start () {
        string name = Toolbox.FindObjectOfType<LevelManager>().GetCurrentLevelSki().name;
        image.sprite = background[Convert.ToInt32(name[name.Length - 1])-1-48].sprite;
    }
	
}
