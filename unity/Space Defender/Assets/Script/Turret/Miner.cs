using UnityEngine;
using System.Collections.Generic;

public class Miner : TurretBase {

	public Transform shotSpawn;
	public GameObject shot;
	public List<GameObject> shootEnemys = new List<GameObject>();
	public override void SetUpAttributions() {
		return;
	}
	override public void ShotSpawn() {
		Vector3 start = new Vector3(shotSpawn.position.x, shotSpawn.position.y, shotSpawn.position.z);
		(Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<Laser>().SetUp(start, currentTarget);
		print("shoot");
	}
	override public void SetShootEnemy(GameObject enemy) {
		if (enemy != null) {
			shootEnemys.Add(enemy);
		}
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