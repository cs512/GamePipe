using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadSceneSelection : MonoBehaviour {
    public void loadScene() {
        if (gameObject.name == "mainMenu") {
            SceneManager.LoadScene("opeing");
        } else if (gameObject.name == "Tutorial") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            SceneManager.LoadScene("TutorialTrue");
        } else if (gameObject.name == "Story") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().SetMode(1);
            GameObject.Find("Main Camera").GetComponent<GameInitializator>().PerformSenceTransit();
        } else if(gameObject.name == "Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().SetMode(0);
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevel(1);
        } else if (gameObject.name == "level1Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(1);
        } else if (gameObject.name == "level2Ski") {
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(2);
        } else if (gameObject.name == "level3Ski"){
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(3);
        } else if (gameObject.name == "level4Ski"){
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(4);
        } else if (gameObject.name == "level5Ski"){
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(5);
        } else if (gameObject.name == "level6Ski"){
            GameObject.Find("LoadingImage").GetComponent<Image>().enabled = true;
            Toolbox.Instance.GetComponent<LevelManager>().JumpLevelSki(6);
        }
    }
    void Start()
    {
        GameObject.Find("LoadingImage").GetComponent<Image>().enabled = false;
    }
}