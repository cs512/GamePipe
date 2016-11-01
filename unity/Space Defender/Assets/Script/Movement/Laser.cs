using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float damage = 2.0f;
	public LineRenderer laser;

	private Vector3 start;
	private Transform target;
	private float remainTime = 0.1f;

	void Update() {
		KeepDirection();
		CountTime();
	}

	public void SetUp(Vector3 start, Transform target) {
		laser.SetPosition(0, start);
		this.target = target;
	}

	void KeepDirection() {
		Vector3 des = new Vector3(target.position.x, target.position.y, target.position.z);
		laser.SetPosition(1, des);
	}

	void CountTime() {
		if(remainTime <= 0) {
			Destroy(gameObject);
		} else {
			remainTime -= Time.deltaTime;
		}
	}


}
