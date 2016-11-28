using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SilcenceTurret : TurretBase {

	public Transform shotSpawn;
	public GameObject shot;
	public List<GameObject> shootEnemys = new List<GameObject>();
	public int maxTarget;
	public float damage = 10.0f;
	public float durationTime = 2.0f;

	public override void Attack(Dictionary<int, Victim> victims) {
		Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
		if (victims.Count != 0) {
			List<GameObject> targetList = new List<GameObject>();
			foreach (int id in victims.Keys) {
				if (dispatcher.enemyVictims.ContainsKey(id)) {
					GameObject targetObj = victims[id].GetGameObject();
					Transform target = targetObj.transform;
					float distance = Vector3.Distance(target.position, transform.position);
					if (range >= distance) {
						targetList.Add(targetObj);
					}
				}
			}

			targetList.Sort((x, y) => Vector3.Distance(x.transform.position, transform.position).CompareTo(
				Vector3.Distance(y.transform.position, transform.position)));
			for(int i = 0; i < maxTarget; i++) {
				if(i == targetList.Count)
					break;
				SilenceTarget(targetList[i]);
			}
		}
	}

	public void SilenceTarget(GameObject targetObj) {
		Enemy target = targetObj.GetComponent<Enemy>();
		target.Silence();
		(Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<LaserBullet>().SetTarget(
			shotSpawn, targetObj.transform, currentVictim, damage, durationTime
		);

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
