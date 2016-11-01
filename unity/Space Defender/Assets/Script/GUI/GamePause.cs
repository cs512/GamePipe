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
    public Button menuCog;
    private bool menuToggled;
    public void chColor(Color norm,Color high,Color press,Color disable) {
        ColorBlock cb = new ColorBlock();
        cb.normalColor = norm;
        cb.highlightedColor = high;
        cb.pressedColor = press;
        cb.disabledColor = disable;
        menuCog.colors = cb;
    }
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
    }

    public void BackToLevelSelection()
    {//go back to the Level Selection
        Time.timeScale = 1;
        SceneManager.LoadScene("levelSelection");
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
        retry.localPosition = new Vector3(-160, 0, -6);
        levelSelection.localPosition = new Vector3(160, 0, -6);
        resume.localPosition = new Vector3(0, 0, -6);
        retry.sizeDelta = new Vector2(120, 80);
        resume.sizeDelta = new Vector2(120, 80);
        levelSelection.sizeDelta = new Vector2(120, 80);
    }

    public void toggleMenu()
    {
        if (menuToggled)
        {
            HidePauseMenu();
            menuToggled = false;
        }
        else
        {
            ShowPauseMenu();
            menuToggled = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        lvlMgr = Toolbox.Instance.GetOrAddComponent<LevelManager>();
        HidePauseMenu();
        menuToggled = false;
        //print(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
