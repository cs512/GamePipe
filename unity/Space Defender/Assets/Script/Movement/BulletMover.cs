using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour {

    public float speed  =20f;
    public float damage = 1f;
    void Start(){
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
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
            Destroy(this.gameObject);
            Victim victim = other.GetComponent<Enemy>();
            victim.DealDamage(this.damage);
            Debug.Log(victim.GetHealth());
        }
    }
}
