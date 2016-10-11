//Author : Guangzhao Feng
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
     
    public interface WaveProvider {
        int getRemainingWaves();
    }

    public float live = 0;
    private float staticLive = 0;
    public float fund = 10.0f;
    public float wave = 10;
    public string waveProvider;
    public Text Waves;
    public Text Funds;
    public Text Lives;
    //initial game with life and funds and waves
    public void initGame() {
        if (GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health == 0)
            live = 0;
        else
            live = GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health;
        staticLive = live;
    }
    public Text getLive() {
        if (GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health == 0)
            live = 0;
        else
            live = GameObject.Find("SourcePlanet").GetComponent<SourcePlanet>().health;
        Lives.text = "Health :" + live.ToString();
        if (live <= 0)
        {
            gameOver();
        }
            return Lives;  
    }
    public Text getFund() {      
        Funds.text = "Resource: " + fund.ToString();
        return Funds;
    }

    public void getWaves() {
        return;
    }

    //loseFund, >0 lose，<0 add, if money is not enough alert and nothing happend
    public float loseFund(float i) {
        float temp = fund;
        fund -= i;
        if (fund <= 0) {
            fund = temp;    
            Debug.Log("no enough money");
            return fund;
        }
        return fund;
    }

    // working on, call this whenever a wave of enemies are  purged
    public void gameEnd() {
        wave -= 1;
        if (wave == 0)
        {
            float cases = live / staticLive;
            if (cases == 1) {
                Debug.Log("3 stars！");
            }
            else if (cases >=2 / 3) {
                Debug.Log("2 stars!");
            }
            else
                Debug.Log("1 stars!");
        }
    }

    //working on 
    public void gameOver() {
        //Debug.Log("Game Over!");

        Time.timeScale = 0;
        //SceneManager.LoadScene("GameEnd");
    }
    void Start() {
        initGame();
    }
    //Update is called once per frame
    void Update() {
        getLive();
        getFund();
        getWaves();
    }
}
