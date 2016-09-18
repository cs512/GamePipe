using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TurretActionAndAttribution : DragTurret{

	public Transform shotSpawn;
	public GameObject shot;
	public float rotateSpeed = 5f;
	public float range = 200f;
	public bool showRange = true;

	private Transform currentTarget = null;
	private Victim currentVictim = null;
	private float nextFire = 0;

	public void init() {
		Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
		dispatcher.RegisteKiller(this);
	}

	override public void Attack(Dictionary<int, Victim> victims) {
		if (victims.Count == 0)
			return;
		float min_dist = float.MaxValue;

		// only pick new target when 1) no target right now; 2) target out of range; 3) target's hearlth equals 0
		if(currentTarget == null || range < Vector3.Distance (currentTarget.position, transform.position) || currentVictim.GetHealth() == 0f){
			foreach(int id in victims.Keys){
				Transform target = ((GameObject)EditorUtility.InstanceIDToObject(id)).transform;
				currentVictim = victims[id];
				float distance = Vector3.Distance (target.position, transform.position);
				if (range < distance)
					continue;
				if(min_dist >= distance){
					currentTarget = target;
					min_dist = distance;}
			}
		} else {
			targetLockOn();
			ShotSpawn();
		}
	}

	override public void ShotSpawn() {
		if(Time.time > nextFire){
			nextFire = Time.time + fireInterval;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		} 
	}

	void targetLockOn() {
		Vector3 targetDir = currentTarget.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (targetDir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}


	void OnDrawGizmos() {
		if(showRange){
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, range);
		}
	}

}
