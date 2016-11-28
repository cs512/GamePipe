using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public RectTransform gamePauseMenu;
    public RectTransform retry;
    public RectTransform levelSelection;
    public RectTransform resume;
    private float oldTimeScale;
    private bool menuShowed;

    LevelManager lvlMgr;
    public void Retry()
    {// that is a reloading of this scene
        try
        {
            Time.timeScale = 1;
            lvlMgr.ReloadLevel();
        }
        catch
        {
            Debug.Log("wrong with LevelManager.cs");
        }
    }
    public void Resume() { 
        Time.timeScale = 1;
        gamePauseMenu.sizeDelta = new Vector2(0, 0);
        retry.sizeDelta = new Vector2(0, 0);
        levelSelection.sizeDelta = new Vector2(0, 0);
        resume.sizeDelta = new Vector2(0, 0);
        menuShowed = false;
    }

    public void BackToLevelSelection()
    {//go back to the Level Selection
        Time.timeScale = 1;
        SceneManager.LoadScene("levelSelectionSki");
    }
    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        gamePauseMenu.sizeDelta = new Vector2(0, 0);
        retry.sizeDelta = new Vector2(0, 0);
        levelSelection.sizeDelta = new Vector2(0, 0);
        resume.sizeDelta = new Vector2(0, 0);
    }
    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        gamePauseMenu.sizeDelta = new Vector2(600, 320);
        retry.sizeDelta = new Vector2(120, 80);
        resume.sizeDelta = new Vector2(120, 80);
        levelSelection.sizeDelta = new Vector2(120, 80);
    }

    public void toggleMenu()
    {
        if (menuShowed)
        {
            HidePauseMenu();
            menuShowed = false;
        }
        else
        {
            ShowPauseMenu();
            menuShowed = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        lvlMgr = Toolbox.Instance.GetOrAddComponent<LevelManager>();
        HidePauseMenu();
        menuShowed = false;
        //print(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
