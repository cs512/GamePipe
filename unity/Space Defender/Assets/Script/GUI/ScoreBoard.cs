//Author : Guangzhao Feng
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    public float live = 0;
    private float staticLive = 0;
    public float fund = 10.0f;
    public float wave = 10;
    public Text tWaves;
    public Text tFunds;
    public Text tLives;
    //initial game with life and funds and waves
    public void initGame() {
        if (GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health == 0)
            live = 0;
        else
            live = GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health;
        staticLive = live;
    }
    public void setLive(){
        if (GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health == 0)
            live = 0;
        else
            live = GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health;
        tLives.text = "Health: " + live.ToString();
        if (live <= 0)
        {
            gameOver();
        }
    }
    public void setFund() {      
        tFunds.text = "Resource: " + fund.ToString();
    }
    public void setWaves() {
        WaveManager waveMgr = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        wave = waveMgr.GetRemainedWave();
        tWaves.text = "Remaining Waves: " + wave.ToString();
    }

    //loseFund, >0 lose，<0 add, if money is not enough alert and nothing happend
    public bool LoseFund(float i) {
        float temp = fund;
        fund -= i;
        
        if (fund < 0) {
            fund = temp; 
            return false;
        }
        Debug.Log("remain fund:" + fund);
        return true;
    }

    // working on, call this whenever a wave of enemies are  purged
    public void gameEnd() {
        
        bool isCompleted=GameObject.Find ("WaveManager").GetComponent<WaveManager>().HasComplete();
        if (live != 0 && isCompleted == true) {
            float cases = live / staticLive;
            if (cases == 1) {
                Debug.Log("GameEnd! 3 stars");
            } else if (cases >= 2 / 3) {
                Debug.Log("GameEnd! 2 stars!");
            } else
                Debug.Log("GameEnd! 1 stars!");
        } else {
            if (live == 0) {
                Debug.Log("GameEnd! You Lose!");
            }
        }
    }

    //working on 
    public void gameOver() {
        Debug.Log("Game Over!");

        Time.timeScale = 0;
        //SceneManager.LoadScene("GameEnd");
    }
    void Start() {
        initGame();
    }
    //Update is called once per frame
    void Update() {
        setLive();
        setFund();
        setWaves();
        gameEnd();
    }
}
