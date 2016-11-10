using UnityEngine;
using System.Collections;

public class EnemyBullet : BulletMover {
    public string colliderObject;
    override public void OnTriggerEnter(Collider turret) {
        RaycastHit hit;
        
        colliderObject = turret.tag;
        if (colliderObject == "Turrets" || colliderObject == "Launchers") {
            Destroy(gameObject);
            Victim victim = turret.GetComponent<TurretBase>();
            victim.DealDamage(this.damage);
        }else if (colliderObject == "Shield")
        {
            Destroy(gameObject);
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                Victim victim = turret.transform.parent.transform.GetComponent<TurretBase>();
                victim.ReduceShield(this.damage, hit.point);
            }
            
        }
    }
}
