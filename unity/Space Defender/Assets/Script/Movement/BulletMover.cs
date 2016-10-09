using UnityEngine;
using System.Collections;

public class BulletMover : TurretBase {

    public float speed;
    public float damage;
    public float bulletRotateSpeed = 100f;

    void Start(){
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, 10);
    }

    void Update()
    {
        Vector3 targetDir = currentTarget.position - transform.position;
        float step = bulletRotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public float setDamage(float inputDamage) {
        damage = inputDamage; 
        return damage;
    } 
    public float getDamage() {
        return this.damage;
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
