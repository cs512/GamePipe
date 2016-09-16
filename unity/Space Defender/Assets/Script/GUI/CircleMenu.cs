using UnityEngine;
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
                if (selected.title == "trtLrg")
                {
                    Instantiate(GameObject.Find("Naga"), transform.position, transform.rotation);
                }
                else if (selected.title == "trtMd")
                {
                    Instantiate(GameObject.Find("Armageddon"), transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
            
        }

    }
}
