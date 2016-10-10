using UnityEngine;
using System.Collections;

public class EnemyBullet : BulletMover {
    override public void OnTriggerEnter(Collider other) {
        if (other.tag == "Turrets") {
            Destroy(gameObject);
            Victim victim = other.GetComponent<Enemy>();
            victim.DealDamage(this.damage);
            Debug.Log(victim.GetHealth());
        }
    }
}
