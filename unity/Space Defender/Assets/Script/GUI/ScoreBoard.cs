//Author : Guangzhao Feng
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour {
    private int live = 30;
    private int staticLive = 0;
    private int fund = 100;
    private int wave = 10;
    public Text Waves;
    public Text Funds;
    public Text Lives;
    //initial game with life and funds and waves
    public void initGame(int lives, int funds,int waves) {
        live = lives;
        fund = funds;
        wave = waves;
        staticLive = lives;
    }
    public Text getLive() {
        Lives.text = "Life :" + live.ToString();
        return Lives;
    }
    public Text getFund() {
        Funds.text = "Funds: $" + fund.ToString();
        return Funds;
    }
    public Text getWave() {
        Waves.text = "Wave remains: " + wave.ToString();
        return Waves;
    }

    // loseLife， >0 lose，<0 add 
    public int loseLife(int i) {
        live -= i;
        if (live <= 0) {
            gameOver();
        }
        return live;
    }
    //loseFund, >0 lose，<0 add, if money is not enough alert and nothing happend
    public int loseFund(int i) {
        int temp = fund;
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
                SceneManager.LoadScene("GameEnd");
            }
            else if (cases >=2 / 3) {
                Debug.Log("2stars!");
                SceneManager.LoadScene("GameEnd");
            }
            else
                Debug.Log("1 stars!");
                SceneManager.LoadScene("GameEnd");
        }
    }

    //working on 
    public void gameOver() {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameEnd");
    }
    void Start() {
        initGame(50, 1000,30);
        Debug.Log(Funds.text);
    }
    //Update is called once per frame
    void Update() {
        getFund();
        getLive();
        getWave();
    }
}
