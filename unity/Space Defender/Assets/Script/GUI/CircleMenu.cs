using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleMenu : MonoBehaviour {

    public CircleButton buttonPrefab;
    public CircleButton selected;
    public GameObject newObject;

    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit hit;

    // Use this for initialization
    public void SpawnButtons (Interactable obj) {
        for (int i = 0;i < obj.options.Length; i++)
        {
            CircleButton newButton = Instantiate(buttonPrefab) as CircleButton;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / obj.options.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 15f;
            newButton.circle.color = obj.options[i].color;
            newButton.icon.sprite = obj.options[i].sprite;
            newButton.title = obj.options[i].title;
            newButton.myMenu = this;
        }
    }

    void Update()
    {
		int nbTouches = Input.touchCount;

		if(nbTouches > 0)
		{
			for (int i = 0; i < nbTouches; i++)
			{
				Touch touch = Input.GetTouch(i);

				if(touch.phase == TouchPhase.Ended)
				{
					if (selected) {
						Debug.Log (selected.title + " was selected");
						transform.position = new Vector3 (380f, 0f, 120f);
						transform.localRotation = new Quaternion (0f, 90f, 0f, 0f);
						if (selected.title == "trtLrg") {
							Object prefab = Resources.Load ("Prefabs/G425", typeof(GameObject)) as GameObject;
							newObject = Instantiate (prefab, transform.position, transform.localRotation) as GameObject;
						} else if (selected.title == "trtMd") {
							Object prefab = Resources.Load ("Prefabs/G150dual", typeof(GameObject)) as GameObject;
							newObject = Instantiate (prefab, transform.position, transform.localRotation) as GameObject;
						} else if (selected.title == "trtSml") {
							Object prefab = Resources.Load ("Prefabs/G75", typeof(GameObject)) as GameObject;
							newObject = Instantiate (prefab, transform.position, transform.localRotation) as GameObject;
						}
						selected = null;

					}
					Destroy (gameObject);
				}

			}
		}
//		foreach (Touch touch in Input.touches) {
//			Ray ray = GetComponent<Camera> ().ScreenPointToRay (touch.position);
//			if (Physics.Raycast (ray, out hit, touchInputMask)) {
//
//				GameObject recipient = hit.transform.gameObject;
//				touchList.Add (recipient);
//
//				if (touch.phase == TouchPhase.Ended) {
//					
//				}
//			}
//		}
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0)){
            if (selected) {
                Debug.Log(selected.title + " was selected");
                transform.position = new Vector3(380f, 0f, 120f);
                transform.localRotation = new Quaternion(0f, 90f, 0f, 0f);
                if (selected.title == "trtLrg") {
                    Object prefab = Resources.Load("Prefabs/G425", typeof(GameObject)) as GameObject;
                    newObject = Instantiate(prefab, transform.position, transform.localRotation) as GameObject;
                } else if (selected.title == "trtMd") {
                    Object prefab = Resources.Load("Prefabs/G150dual", typeof(GameObject)) as GameObject;
                    newObject = Instantiate(prefab, transform.position, transform.localRotation) as GameObject;
                } else if (selected.title == "trtSml") {
                    Object prefab = Resources.Load("Prefabs/G75", typeof(GameObject)) as GameObject;
                    newObject = Instantiate(prefab, transform.position, transform.localRotation) as GameObject;
                }
            }
            Destroy(gameObject);
        }
#endif
    }
}
