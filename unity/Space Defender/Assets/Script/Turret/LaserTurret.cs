using UnityEngine;
using System.Collections.Generic;
using System;

public class LaserTurret : TurretBase {

	public Transform shotSpawn;
	public GameObject shot;
	public float damage;
	public float durationTime;
	public List<GameObject> shootEnemys = new List<GameObject>();

	private Transform oldTarget = null;

	public override void SetUpAttributions() {
		return;
	}
	override public void ShotSpawn() {
		if(oldTarget != null){
			if(currentTarget == oldTarget)
				return;
		}
		shot.GetComponent<LaserBullet>().SetTarget(
			shotSpawn, currentTarget, currentVictim, damage, durationTime
		);
		oldTarget = currentTarget;
		(Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<LaserBullet>();
	}
	override public void SetShootEnemy(GameObject enemy) {
		if (enemy != null) {
			shootEnemys.Add(enemy);
		}
	}
	public override void ReduceShield(float damage, Vector3 hittingPoint)
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
