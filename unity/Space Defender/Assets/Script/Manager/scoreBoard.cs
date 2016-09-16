using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour
{
    int live = 20;
    int fund = 100;
    public Text Funds;
    public Text Lives;
    //初始游戏，生命值金钱
    public void initGame(int lives, int funds)
    {
        live = lives;
        fund = funds;
    }
    // 调用loseLife，控制生命损失 >0掉血，<0加血
    public void loseLife(int i)
    {
        live -= i;
        if (live <= 0)
        {
            gameOver();
        }
    }
    //loseFund,控制金钱 >0掉钱，<0加钱 若钱为空或小于0，返回false
    public bool loseFund(int i)
    {
        int temp = fund;
        fund -= i;
        if (fund <= 0)
        {
            fund = temp;
            return false;
        }
        return true;
    }
    //未完成状态
    public void gameOver()
    {
        Debug.Log("Game Over!");
    }
    void Start()
    {
        initGame(50, 1000);
        Debug.Log(Funds.text);
    }
    //Update is called once per frame
    void Update()
    {
        Funds.text = "Funds: $" + fund.ToString();
        Lives.text = "Lives: " + live.ToString();
        // loseLife(50);
    }
}
