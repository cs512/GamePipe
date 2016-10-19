using UnityEngine;
using System.Collections;

public class TurretBullet : BulletMover {
    override public void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Destroy(gameObject);
            Victim victim = other.GetComponent<Enemy>();
            victim.DealDamage(this.damage);
            Debug.Log(victim.GetHealth());
        }
    }
}
