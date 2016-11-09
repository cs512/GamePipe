using UnityEngine;
using System.Collections.Generic;
using System;

public class SingleBarrel : TurretBase {

    public Transform shotSpawn;
    public GameObject shot;
    public List<GameObject> shootEnemys = new List<GameObject>();
    public override void SetUpAttributions() {
        return;
    }
    override public void ShotSpawn() {
        (Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<TurretBullet>().setTarget(currentTarget);
    }
    override public void SetShootEnemy(GameObject enemy) {
        if (enemy != null) {
            shootEnemys.Add(enemy);
        }
    }
    public override void ReduceShield(float damage)
    {
        return;
    }
    override public void DismissShootEnemy() {
        if (this.shootEnemys.Count != 0) {
            foreach (GameObject enemy in shootEnemys) {
                if (enemy != null) {
                    enemy.GetComponent<Enemy>().flag = 0;
                }
            }
        }
    }
}
