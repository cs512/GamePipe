using UnityEngine;
using System.Collections;

public class Shots : MonoBehaviour {
	GameObject targetObject;
	Transform target;
	float speed=5f;
	public static string targetName;
	public Victim victim;

	public Shots(Victim vic){
		victim = vic;
		targetName = vic.gameObjectName;
	}
	// Use this for initialization
	void Start () {
		targetObject = GameObject.Find (targetName);
	}

	// Update is called once per frame
	void Update () {
		target = target.transform;
		//Debug.Log(target.position);
		Vector3 dir = target.position - this.transform.position;
		float framDist = speed * Time.deltaTime;
		if (dir.magnitude <= framDist) {
			SelfDestruct();
			victim.dealDamage ();
		} else {
			transform.Translate(dir.normalized * framDist,Space.World);
			this.transform.rotation = Quaternion.LookRotation (dir);
		}
	}
	void SelfDestruct(){
		Destroy (gameObject);
	}
}
