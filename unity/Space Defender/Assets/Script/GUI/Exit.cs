using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Exit : MonoBehaviour {
    public Text exitT;
    public void Back() {//go back to the Level Selection
        SceneManager.LoadScene("levelSelection");
    }
    public void setEColor(Color x) {
        exitT.color = x;
    }
}
