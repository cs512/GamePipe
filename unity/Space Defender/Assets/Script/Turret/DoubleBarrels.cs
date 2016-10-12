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

    override public void ShotSpawn() {
        Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation);
        Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation);
		BulletMover bullet = shot.GetComponent<BulletMover>();
		bullet.setTarget(currentTarget);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
    override public void SetShootEnemy(GameObject enemy){
        shootEnemys.Add(enemy);
    }
    override public void DismissShootEnemy(){
        if (this.shootEnemys.Count != 0) {
            foreach (GameObject enemy in shootEnemys) {
                if (enemy != null) {
                    enemy.GetComponent<Enemy>().flag = 0;
                }
            }
        }
    }
}
