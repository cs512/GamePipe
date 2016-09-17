﻿using UnityEngine;
using System.Collections;

public class CircleMenu : MonoBehaviour {

    public CircleButton buttonPrefab;
    public CircleButton selected;

	// Use this for initialization
	public void SpawnButtons (Interactable obj) {
        for (int i = 0;i < obj.options.Length; i++)
        {
            CircleButton newButton = Instantiate(buttonPrefab) as CircleButton;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / obj.options.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 10f;
            newButton.circle.color = obj.options[i].color;
            newButton.icon.sprite = obj.options[i].sprite;
            newButton.title = obj.options[i].title;
            newButton.myMenu = this;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (selected)
            {
                Debug.Log(selected.title + " was selected");
                transform.position = new Vector3 (380f, 0f, 120f);
                transform.localRotation = new Quaternion(0f, 90f, 0f, 0f);
                if (selected.title == "trtLrg")
                {
                    Instantiate(GameObject.Find("G425"), transform.position, transform.localRotation);
                }
                else if (selected.title == "trtMd")
                {
                    Instantiate(GameObject.Find("G150dual"), transform.position, transform.localRotation);
                }
                else if (selected.title == "trtSml")
                {
                    Instantiate(GameObject.Find("G75"), transform.position, transform.localRotation);
                }
                Destroy(gameObject);
            }
            
        }

    }
}
