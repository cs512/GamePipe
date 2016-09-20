using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {
    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit hit;

    [System.Serializable]
    public class Action
    {
        public Color color;
        public Sprite sprite;
        public string title;
    }
    public Action[] options;
    // Use this for initialization

#if UNITY_EDITOR
    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            CircleSpwaner.ins.SpawnMenu(this);
        }
    }
#endif
    void Update()
    {
//		Touch touch = Input.GetTouch(0);
//		if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary) {
//			Debug.Log ("33333");
//			Ray ray = GetComponent<Camera> ().ScreenPointToRay (touch.position);
//			Debug.Log ("touch's position is:" + touch.position.x + " " + touch.position.y);
//			if (Physics.Raycast (ray, out hit, touchInputMask)) {
//				Debug.Log ("33333");
//				GameObject recipient = hit.transform.gameObject;
//				if (recipient!=null) {
//					Debug.Log(recipient.name);
//					Debug.Log("44444");
//				}
//				if(recipient.name == this.gameObject.name){
//					CircleSpwaner.ins.SpawnMenu (this);
//				}
//			}
//				   
//		}
//	}

		int nbTouches = Input.touchCount;

		if(nbTouches > 0)
		{
			for (int i = 0; i < nbTouches; i++)
			{
				Touch touch = Input.GetTouch(i);

				if(touch.phase == TouchPhase.Began)
				{
					Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
					RaycastHit hit;
					if (Physics.Raycast(screenRay, out hit))
					{
						if(hit.collider.gameObject.name == "Quad"){
							CircleSpwaner.ins.SpawnMenu(this);
						}
					}
				}

			}
		}
}
}
