using UnityEngine;
using System.Collections.Generic;
public class DoubleBarrels : TurretBase {

    public Transform shotSpawnR;
    public Transform shotSpawnL;
    public GameObject shot;
    public List<GameObject> shootEnemys = new List<GameObject>();

    public override void SetUpAttributions() {
        return;
    }

    public override void ReduceShield(float damage)
    {
        return;
    }

    override public void ShotSpawn() {
        if (gameObject.tag == "Launchers") {
            int fireCount = Random.Range(0, 3);
            if (fireCount == 2) {
                (Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation) as GameObject).GetComponent<TurretBullet>().setTarget(currentTarget);
            } else {
                (Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation) as GameObject).GetComponent<TurretBullet>().setTarget(currentTarget);
            }
        } else {
            (Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation) as GameObject).GetComponent<TurretBullet>().setTarget(currentTarget);
            (Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation) as GameObject).GetComponent<TurretBullet>().setTarget(currentTarget);
        }
    }
    override public void SetShootEnemy(GameObject enemy) {
        shootEnemys.Add(enemy);
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
