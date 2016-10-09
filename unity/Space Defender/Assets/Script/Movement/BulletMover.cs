using UnityEngine;
using System.Collections;

public class BulletMover : TurretBase {

<<<<<<< HEAD
    public float speed  =20f;
    public float damage = 1f;
	public Transform target;
=======
    public float speed;
    public float damage;
    public float bulletRotateSpeed = 100f;
>>>>>>> 295d326847fe367a10ac50b31c5310752ca5fdd3

    void Start(){
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //Destroy(gameObject, 10);
    }

<<<<<<< HEAD
	// Update is called once per frame
	void Update() {
		if (target == null) {
			// the enemy went away!
			Destroy(this.gameObject);
			return;
		}
		//Debug.Log(target.position);
		Vector3 dir = target.position - this.transform.localPosition;
		float framDist = speed * Time.deltaTime;
		transform.Translate(dir.normalized * framDist, Space.World);
		this.transform.rotation = Quaternion.LookRotation(dir);
	}
=======
    void Update()
    {
        Vector3 targetDir = currentTarget.position - transform.position;
        float step = bulletRotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
>>>>>>> 295d326847fe367a10ac50b31c5310752ca5fdd3

    public float setDamage(float inputDamage) {
        damage = inputDamage; 
        return damage;
    } 
    public float getDamage() {
        return this.damage;
    }
	public void setTarget(Transform inputTarget) {
		target = inputTarget; 
	} 
	public Transform getTarget() {
		return this.target;
	}
    void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Enemy") {
            Destroy(gameObject);
            Victim victim = other.GetComponent<Enemy>();
            victim.DealDamage(this.damage);
            Debug.Log(victim.GetHealth());
        }
    }
    public override void SetUpAttributions()
    {
        return;
    }
    public override void ShotSpawn()
    {
        return;
    }
}
