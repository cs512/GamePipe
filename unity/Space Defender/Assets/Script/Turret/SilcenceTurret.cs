using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SilcenceTurret : TurretBase {

	public Transform shotSpawn;
	public GameObject shot;
	public List<GameObject> shootEnemys = new List<GameObject>();

	public override void Attack(Dictionary<int, Victim> victims) {
		Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
		if (victims.Count != 0) {
			foreach (int id in victims.Keys) {
				if (dispatcher.enemyVictims.ContainsKey(id)) {
					GameObject targetObj = victims[id].GetGameObject();
					Transform target = targetObj.transform;
					float distance = Vector3.Distance(target.position, transform.position);
					if (range >= distance) {
						SilenceTarget(targetObj);
					}
				}
			}
		}
	}

	public void SilenceTarget(GameObject targetObj) {
		Enemy target = targetObj.GetComponent<Enemy>();
		target.Silence();

	}

	public override void SetUpAttributions() {
		return;
	}
	override public void ShotSpawn() {
		return;
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
