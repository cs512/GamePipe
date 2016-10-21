using UnityEngine;
using System.Collections;

public class TurretBullet : BulletMover {
    override public void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            if  (gameObject.tag != "Freeze")
            {
                Destroy(gameObject);
                Victim victim = other.GetComponent<Enemy>();
                victim.DealDamage(this.damage);
                Debug.Log(victim.GetHealth());
            }
            else
            {
                Destroy(gameObject);
                Victim victim = other.GetComponent<Enemy>();
                victim.SlowDown(this.slowRate);
                Debug.Log(victim.GetHealth());
            }
        }
    }
}
