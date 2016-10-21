using System;
using System.Collections.Generic;
using UnityEngine;
public class Final : MonoBehaviour
{
    public void FinalStrike() {
        if (GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(600)) {
            GameObject.FindWithTag("Enemy").GetComponent<Enemy>().DestorySelf();
        }
    }
}
