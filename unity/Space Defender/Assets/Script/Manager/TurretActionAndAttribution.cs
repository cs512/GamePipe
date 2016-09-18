using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TurretActionAndAttribution : MonoBehaviour, Killer{

	public Transform shotSpawn;
	public GameObject shot;
	public float rotateSpeed = 5;
	public int fireInterval = 1;

	private GameObject currentTarget = null;
	private Victim currentVictim = null;
	private float nextFire = 0;
	private Quaternion lastRotation;

	public GameObject target1;
	public GameObject target2;
	public ArrayList targetArray;

	public void init(){
		lastRotation = transform.rotation;

		Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
		dispatcher.RegisteKiller(this);
	}

	public void Attack(Dictionary<int, Victim> victims){
		if (victims.Count == 0)
			return;
		
		if(currentVictim == null || currentVictim.GetHealth() <= 0){
            float min_dist = float.MaxValue;
			int targetId = 0;
			foreach (int id in victims.Keys) {
				GameObject target = (GameObject)EditorUtility.InstanceIDToObject (id);
				float distance = Vector3.Distance (target.transform.position, transform.position);
				if (min_dist >= distance) {
					currentTarget = target;
					targetId = id;
				}
			}
			currentVictim = victims [targetId];
		}

		if (!roatateToTarget ())
			ShotSpawn ();
	}

	public void ShotSpawn(){
		if(Time.time > nextFire){
			nextFire = Time.time + fireInterval;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		} 
	}

	bool roatateToTarget(){
		Vector3 targetDir = currentTarget.transform.position - transform.position;
		float step = rotateSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);

		if(lastRotation == transform.rotation)  // check if turret is facing target
			return false;
		
		lastRotation = transform.rotation;
		return true;
	}

	public int GetFireInterval(){
		return fireInterval;
	}

	public int GetID() {
		return GetInstanceID();
	}
}
