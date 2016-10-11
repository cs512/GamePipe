using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {
    Scene activeScene = SceneManager.GetActiveScene();
    public string path = "";
    public void Retry() {// that is a reloading of this scene
        if (activeScene.path != "")
        {
            path += activeScene.path;
            SceneManager.LoadScene(path);
        }
        else
            Debug.Log("Scene Wrong!");
    }
    public void Next() {//To the next level.
        if (path != "")
        {
            SceneManager.LoadScene(path);
        }
        else
            Debug.Log("Cant get next Level!");
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
