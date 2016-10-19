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
        return true;
    }

    // always on, if no more enemy exists and would be generated and still has life, calculate the score.
    public void gameEnd() {
        bool isCompleted=GameObject.Find ("WaveManager").GetComponent<WaveManager>().HasComplete();
        int enemyCount = GameObject.Find("Dispatcher").GetComponent<Dispatcher>().GetEnemyCount();
        if (live != 0 && isCompleted == true && (enemyCount ==0)) {
            float cases = live / staticLive;
            if (cases == 1) {
                GameObject.Find ("GameEnd").GetComponent<GameEnd> ().ShowEnd (3);
                Time.timeScale = 0;
            } else if (cases >= 2 / 3) {
                GameObject.Find ("GameEnd").GetComponent<GameEnd> ().ShowEnd (2);
                Time.timeScale = 0;
            } else {
                GameObject.Find ("GameEnd").GetComponent<GameEnd> ().ShowEnd (1);
                Time.timeScale = 0;
            }
        } 
    }

    //always on if planet has no life, u lose.
    public void gameOver() {
        Debug.Log("Game Over!");
        GameObject.Find ("GameEnd").GetComponent<GameEnd>().ShowEnd (0);
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
