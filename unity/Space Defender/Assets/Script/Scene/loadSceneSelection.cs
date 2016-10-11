using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadSceneSelection : MonoBehaviour {
    public void loadScene() {
        if (gameObject.name == "mainMenu") {
            SceneManager.LoadScene("opeing");
        } else if (gameObject.name == "tutorial") {
            SceneManager.LoadScene("Tutorial");
        } else if (gameObject.name == "Story") {
            SceneManager.LoadScene("levelSelection");
        } else if (gameObject.name == "level1") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevel(1);
        } else if (gameObject.name == "Level 2") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevel(2);
        } else if (gameObject.name == "Level 3") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevel(3);
        } else if (gameObject.name == "Level 4") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevel(4);
        } else if (gameObject.name.Contains("Level")) {
            SceneManager.LoadScene("levelSelection");
        }

    }
}