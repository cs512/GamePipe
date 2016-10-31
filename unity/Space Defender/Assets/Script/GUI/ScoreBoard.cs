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
    }
    public void setLiveColor(Color x)
    {
        tLives.color = x;
    }
    public void setFund() {
        tFunds.text = "Resource: " + fund.ToString();
    }
    public void setFundColor(Color x)
    {
        tFunds.color = x;
    }
    public void setWaves() {
        WaveManager waveMgr = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        wave = waveMgr.GetRemainedWave();
        tWaves.text = "Remaining Waves: " + wave.ToString();
    }
    public void setWaveColor(Color x)
    {
        tWaves.color = x;
    }
    //loseFund, >0 lose，<0 add, if money is not enough alert and nothing happend
    public bool LoseFund(float i) {
        float temp = fund;
        fund -= i;
        
        if (fund < 0) {
            fund = temp;
            tFunds.color = Color.red;
            Invoke("colorReturn", 0.5f);
            return false;
        }
        return true;
    }
    void colorReturn() {
        tFunds.color = Color.white;
    }
    // always on, if no more enemy exists and would be generated and still has life, calculate the score.
    public void gameEnd() {
        bool isCompleted=GameObject.Find ("WaveManager").GetComponent<WaveManager>().HasComplete();
        int enemyCount = GameObject.Find("Dispatcher").GetComponent<Dispatcher>().GetEnemyCount();
        if (live != 0 && isCompleted == true && (enemyCount ==0)) {
            Time.timeScale = 0;
            float cases = (live / staticLive);
            if (cases == 1) {
                print(live / staticLive);
                GameObject.Find ("GameEnd").GetComponent<GameEnd> ().ShowEnd (3);             
            } else if (cases >= (2 / 3)) {
                print(live / staticLive);
                GameObject.Find("GameEnd").GetComponent<GameEnd>().ShowEnd(2);
            } else if((cases <=(1/3))&&(cases!=0)){
                print(live / staticLive);
                GameObject.Find ("GameEnd").GetComponent<GameEnd> ().ShowEnd (1);
            }else if(cases == 0) {
                print(live / staticLive);
                GameObject.Find("GameEnd").GetComponent<GameEnd>().ShowEnd(0);
            }
        } 
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
