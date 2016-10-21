using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Final : MonoBehaviour
{
    public RectTransform prompt;
    public Text prompts;
    public void FinalStrike() {
        if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(600))
        {
            GameObject.FindWithTag("Enemy").GetComponent<Enemy>().DestorySelf();
        }
        else
            prompt.localPosition = new Vector3(0, 20, 0);
            prompts.text = "You need 600 money.";
    }
    public void Hint()
    {
        prompt.localPosition = new Vector3(0,20,0);
        prompts.text = "600 for a moment of silence!";
    }
    public void HintOff()
    {
        prompts.text = "";
    }
    void Start()
    {
        prompts.text = "";
    }
}
