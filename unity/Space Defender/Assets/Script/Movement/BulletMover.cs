using UnityEngine;
using System.Collections;

public class BulletMover : TurretBase {

    public float speed;
    public float damage;
    public float bulletRotateSpeed = 100f;

    void Start() {
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //Destroy(gameObject, 10);
    }

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
    void OnTriggerEnter(Collider other) {

        if (other.tag == "Enemy") {
            Destroy(gameObject);
            Victim victim = other.GetComponent<Enemy>();
            victim.DealDamage(this.damage);
            Debug.Log(victim.GetHealth());
        }
    }
    public override void SetUpAttributions() {
        return;
    }
    public override void ShotSpawn() {
        return;
    }
}
