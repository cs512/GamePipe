using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour {
    private int live = 20;
    private int fund = 100;
    private int wave = 10;
    private Text Funds;
    private Text Lives;
    //initial game with life and funds and waves
    public void initGame(int lives, int funds, int waves) {
        live = lives;
        fund = funds;
        wave = waves;
    }
    // loseLife， >0 lose，<0 add 
    public void loseLife(int i) {
        live -= i;
        if (live <= 0) {
            gameOver();
        }
    }
    //loseFund, >0 lose，<0 add if money is not enough ，return false
    public bool loseFund(int i) {
        int temp = fund;
        fund -= i;
        if (fund <= 0) {
            fund = temp;
            Debug.Log("no enough money");
            return false;
        }
        return true;
    }
    //call this  every end of a wave
    public void gameEnd(){
        wave -= 1;
        if (wave == 0){
            SceneManager.LoadScene("GameEnd");
        }
    }
    //未完成状态
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
        Funds.text = "Funds: $" + fund.ToString();
        Lives.text = "Lives: " + live.ToString();
    }
}
