using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {
    LevelManager lvlMgr;
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
    public void HideEnd() {

    }
    public void ShowEnd() {

    }

	// Use this for initialization
	void Start () {
        lvlMgr = Toolbox.Instance.GetOrAddComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
