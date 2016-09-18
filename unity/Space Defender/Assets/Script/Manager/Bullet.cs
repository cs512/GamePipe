using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour,Victim {

	public Transform target;
	public float speed;
	public float damage;
	private float health;

	// Use this for initialization
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		if(target == null) {
			// the enemy went away!
			Destroy(gameObject);
			return;
		}
		//Debug.Log(target.position);
		Vector3 dir = target.position - this.transform.position;
		float framDist = speed * Time.deltaTime;
		transform.Translate(dir.normalized * framDist,Space.World);
		this.transform.rotation = Quaternion.LookRotation(dir);

	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		//Debug.Log(collisionInfo.gameObject.transform.name);
		//Debug.Log(target.name);
		if(collisionInfo.gameObject.transform.name==target.name){
			Destroy(gameObject);
		}
		//target.GetComponent<Victim>().DealDamage(damage);
	}
	void Victim.DealDamage(float damage) {
		health -= damage;
	}

	float Victim.GetHealth() {
		return health;
	}

	int Victim.GetID() {
		return GetInstanceID();
	}
}
