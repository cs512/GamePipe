using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {
    public RectTransform gameEnd;
    public Transform tTrishade1;
    public Transform tTrishade2;
    public Transform tTrishade3;
    public Renderer tri1;
    public Renderer tri2;
    public Renderer tri3;
    public Renderer triShade1;
    public Renderer triShade2;
    public Renderer triShade3;
    public RectTransform retry;
    public RectTransform nextLevel;
    public RectTransform goBack;

    LevelManager lvlMgr;
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
        gameEnd.sizeDelta = new Vector2(0,0);
        triShade1.GetComponent<Renderer>().enabled = false;
        triShade2.GetComponent<Renderer>().enabled = false;
        triShade3.GetComponent<Renderer>().enabled = false;
        tri1.GetComponent<Renderer>().enabled = false;
        tri2.GetComponent<Renderer>().enabled = false;
        tri3.GetComponent<Renderer>().enabled = false;
        retry.sizeDelta = new Vector2(0, 0);
        nextLevel.sizeDelta = new Vector2(0, 0);
        goBack.sizeDelta = new Vector2(0, 0);
    }
    public void ShowEnd(float some) {
        gameEnd.sizeDelta = new Vector2(300, 180);
        triShade1.GetComponent<Renderer>().enabled = true;
        triShade2.GetComponent<Renderer>().enabled = true;
        triShade3.GetComponent<Renderer>().enabled = true;
        tri1.GetComponent<Renderer>().enabled = true;
        tri2.GetComponent<Renderer>().enabled = true;
        tri3.GetComponent<Renderer>().enabled = true;
        retry.GetComponent<Renderer>().enabled = true;
        //nextLevel.GetComponent<Renderer>().enabled = true;
        goBack.GetComponent<Renderer>().enabled = true;
        if(some == 0) {
            tTrishade1.position = new Vector3(-80, -6, 30);
            tTrishade2.position = new Vector3(0, -6, 30);
            tTrishade3.position = new Vector3(80, -6, 30);
            nextLevel.GetComponent<Renderer>().enabled = true;
        }
        if (some == 1) {
            tTrishade1.position = new Vector3(-80, -6, 30);
            tTrishade2.position = new Vector3(0, -6, 30);
            //tTrishade3.position = new Vector3(80, -6, 30);
            nextLevel.GetComponent<Renderer>().enabled = true;
        }
        if(some == 2) {
            tTrishade1.position = new Vector3(-80, -6, 30);
            //tTrishade2.position = new Vector3(0, -6, 30);
            //tTrishade3.position = new Vector3(80, -6, 30);
            nextLevel.GetComponent<Renderer>().enabled = true;
        }
        if(some == 3) {
            //tTrishade1.position = new Vector3(-80, -6, 30);
            //tTrishade2.position = new Vector3(0, -6, 30);
            //tTrishade3.position = new Vector3(80, -6, 30);
            nextLevel.GetComponent<Renderer>().enabled = true;
        }

    }

	// Use this for initialization
	void Start () {
        lvlMgr = Toolbox.Instance.GetOrAddComponent<LevelManager>();
        HideEnd();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
