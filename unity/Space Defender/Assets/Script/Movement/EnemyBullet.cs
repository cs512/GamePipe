using UnityEngine;
using System.Collections;

public class EnemyBullet : BulletMover {
    public string colliderObject;
    override public void OnTriggerEnter(Collider turret) {
        colliderObject=turret.tag;
        if (colliderObject == "Turrets") {
            Destroy(gameObject);
            Victim victim = turret.GetComponent<TurretBase>();
            victim.DealDamage(this.damage);
            Debug.Log(victim.GetHealth());
        }
    }
}
