using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeControl : MonoBehaviour {

    public Text content;
    public Text speed;
    public void setTimeColor(Color x)
    {
        content.color = x;
    }
    public void speedUp()
    {
        if (Time.timeScale <= 2)
        {
            Time.timeScale *= 2;
        }
        speed.text = 'x' + Time.timeScale.ToString();
    }
    public void slowDown()
    {
        if (Time.timeScale >= 1)
        {
            Time.timeScale /= 2;
        }
        if (Time.timeScale < 1)
        {
            speed.text = "x1/2";
        }
        else
        {
            speed.text = 'x' + Time.timeScale.ToString();
        }

    }
}
