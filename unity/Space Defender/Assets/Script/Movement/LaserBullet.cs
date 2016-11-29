using UnityEngine;
using System.Collections;

public class LaserBullet : MonoBehaviour {

	public float damage = 20.0f;

	private LineRenderer laser;
	private Transform start;
	private Transform target;
	private Victim victim;
	private float durationTime;
	private bool allSet = false;

	void Update() {
		if(!allSet)
			return;
		KeepDirection();
		DealDamge();
	}

	public void SetTarget(Transform start, Transform target, Victim victim, float damage, float durationTime) {
		laser = gameObject.GetComponent<LineRenderer>();
		laser.SetPosition(0, start.position);
		this.target = target;
		this.damage = damage;
		this.durationTime = durationTime;
		this.victim = victim;
		allSet = true;
	}

	void KeepDirection() {
		if(allSet) {
			Vector3 des = new Vector3(target.position.x, target.position.y, target.position.z);
			laser.SetPosition(1, des);
		}

	}

	void DealDamge() {
		if(victim.GetHealth() <= 0f)
			Destroy(gameObject);
		victim.DealDamage(damage);
	}


}
