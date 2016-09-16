using UnityEngine;
using System.Collections;

public class Shots : MonoBehaviour {
	GameObject targetObject;
	Transform target;
	private float speed=5f;
    private int damage = 0;
	private Victim victim;

	public Shots(Victim vic){
		this.victim = vic;
	}

    public void SetDamage(int damage) {
        this.damage = damage;
    }
	// Use this for initialization
	void Start() {
		targetObject = GameObject.Find(victim.GetInstanceID());
	}

	// Update is called once per frame
	void Update() {
		target = target.transform;
		//Debug.Log(target.position);
		Vector3 dir = target.position - this.transform.position;
		float framDist = speed * Time.deltaTime;
		if (dir.magnitude <= framDist) {
			SelfDestruct();
			victim.DealDamage(this.damage);
		} else {
			transform.Translate(dir.normalized * framDist,Space.World);
			this.transform.rotation = Quaternion.LookRotation(dir);
		}
	}
	void SelfDestruct(){
		Destroy(gameObject);
	}
}
