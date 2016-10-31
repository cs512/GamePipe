using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour {

    public Text content;
    public void setTimeColor(Color x)
    {
        content.color = x;
    }
	public void pause()
    {
        if (content.text == "pause")
        {
            Time.timeScale = 0;
            content.text = "start";
        }
        else if (content.text == "start")
        {
            Time.timeScale = 1;
            content.text = "pause";
        }
    }
    public void speedUp()
    {
        Time.timeScale *= 2;
    }
    public void slowDown()
    {
        Time.timeScale /= 2;
    }
}
