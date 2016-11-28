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
            Toolbox.Instance.GetComponent<LevelManager>().LoadData(1);
            SceneManager.LoadScene("levelSelectionSki");
        } else if(gameObject.name == "Ski"){
            Toolbox.Instance.GetComponent<LevelManager>().LoadData(0);
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevel(1);
        } else if (gameObject.name == "level1Ski") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(1);
        } else if (gameObject.name == "level2Ski") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(2);
        } else if (gameObject.name == "level3Ski") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(3);
        } else if (gameObject.name == "level4Ski") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(4);
        } else if (gameObject.name == "level5Ski") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(5);
        } else if (gameObject.name == "level6Ski") {
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(6);
        }

    }
}