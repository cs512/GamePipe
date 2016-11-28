using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameInitializator : MonoBehaviour {

    AsyncOperation result;

    public void PerformSenceTransit()
    {
        result.allowSceneActivation = true;
    }

    // Use this for initialization
    void Start() {
        Toolbox tb = Toolbox.Instance;
        tb.RegisterComponent<LevelManager>();
        tb.GetComponent<LevelManager>().LoadData(0);
        tb.GetComponent<LevelManager>().LoadData(1);
        result = SceneManager.LoadSceneAsync("levelSelectionSki", LoadSceneMode.Single);
        result.allowSceneActivation = false;
    }
    // Update is called once per frame
    void Update() {

    }
}
