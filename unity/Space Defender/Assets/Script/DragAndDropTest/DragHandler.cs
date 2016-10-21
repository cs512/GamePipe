using UnityEngine;
using System.Collections;

public class DragHandler : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	private bool reachTarget = false;
	// Use this for initialization
	void Start () {
//		RaycastHit hit;
//		Ray targetRay = new Ray(transform.position, Vector3.down);
//		if(Physics.Raycast(targetRay, out hit)){
//			print(hit.collider.gameObject.name);
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));


	}

	void OnMouseDrag() {
		if(!reachTarget){
			Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
			transform.position = cursorPosition;
		}
	}

	void OnMouseUp() {

		RaycastHit[] hits; 
		Ray targetRay = new Ray(transform.position, Vector3.down);
		hits = Physics.RaycastAll (targetRay);
		for (int i = 0; i < hits.Length; i++) {
			if (hits[i].collider.gameObject.name.Equals ("Turret Base")) {
				reachTarget = true;
				Vector3 targetVector = hits[i].collider.gameObject.transform.position;
				Vector3 dest = new Vector3(targetVector.x, transform.position.y, targetVector.z);
				transform.position = Vector3.MoveTowards(transform.position, dest, 100f);
				return;
			}
		}
	}
}
