using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float damage = 2.0f;
	public LineRenderer laser;

	private Transform start;
	private Transform target;
	private float remainTime = 2.0f;
	private bool allSet = false;

	void Update() {
		KeepDirection();
		CountTime();
	}

	public void SetTarget(Transform start, Transform target) {
		laser.SetPosition(0, start.position);
		this.target = target;
		allSet = true;

	}

	void KeepDirection() {
		if(allSet) {
			Vector3 des = new Vector3(target.position.x, target.position.y, target.position.z);
			laser.SetPosition(1, des);
		}

	}

	void CountTime() {
		if(remainTime <= 0) {
			Destroy(gameObject);
		} else {
			remainTime -= Time.deltaTime;
		}
	}


}
