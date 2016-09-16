using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Transform target;
	public float speed;
    public int damage;

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
		if (dir.magnitude <= framDist) {
			Shoot();
		} else {
			transform.Translate(dir.normalized * framDist,Space.World);
			this.transform.rotation = Quaternion.LookRotation(dir);
		}
	}
	void Shoot(){
		target.GetComponent<Victim>().DealDamage(damage);
		Destroy(gameObject);
	}
}
