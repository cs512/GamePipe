using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {
    LevelManager lvlMgr = GameObject.Find("LevelManger").GetComponent<LevelManager>();
    public Sprite tri1;
    public Sprite tri2;
    public Sprite tri3;
    public void Retry() {// that is a reloading of this scene
        try {
            lvlMgr.ReloadLevel();
        }
        catch
        {
            Debug.Log("wrong with LevelManager.cs");
        }
    }
    public void Next() {//To the next level.
        if (lvlMgr.JumpToNextLevel() == false)
        {
            Debug.Log("No Next Level!");
        }
        else
            lvlMgr.JumpToNextLevel();
    }
    public void Back() {//go back to the Level Selection
        SceneManager.LoadScene("levelSelection");
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
