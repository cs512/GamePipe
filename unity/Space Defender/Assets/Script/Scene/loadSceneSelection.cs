using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadSceneSelection : MonoBehaviour {
    public void loadScene()
    {
        if (gameObject.name == "mainMenu")
        {
            SceneManager.LoadScene("opeing");
        }
        else if (gameObject.name == "tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (gameObject.name == "Story")
        {
            SceneManager.LoadScene("levelSelection");
        }
        else if (gameObject.name.Contains("Level"))
        {
            SceneManager.LoadScene("levelSelection");
        }

    }
}