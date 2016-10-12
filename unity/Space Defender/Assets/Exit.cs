using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour {
    public void Back()
    {//go back to the Level Selection
        SceneManager.LoadScene("levelSelection");
    }
}
