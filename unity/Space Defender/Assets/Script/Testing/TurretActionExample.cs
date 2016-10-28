using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretActionExample : MonoBehaviour, Killer {

    public Transform shotSpawn;
    public GameObject shot;
    public float rotateSpeed = 5;
    public int fireInterval = 1;
    public float range = 15f;
    public bool showRange = true;

    private Transform currentTarget = null;
    private float nextFire = 0;

    public GameObject target1;
    public GameObject target2;
    public ArrayList targetArray;

    public void Attack(Dictionary<int, Victim> victims) {
        float min_dist = float.MaxValue;
        if (currentTarget == null || range < Vector3.Distance(currentTarget.position, transform.position)) {
            foreach (int id in targetArray) {
                Transform target = victims[id].GetGameObject().transform;
                float distance = Vector3.Distance(target.position, transform.position);
                if (range < distance)
                    continue;
                if (min_dist >= distance) {
                    currentTarget = target;
                    min_dist = distance;
                }
            }
        } else {
            targetLockOn();
            ShotSpawn();
        }
    }

    public void ShotSpawn() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireInterval;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    void targetLockOn() {
        Vector3 targetDir = currentTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public int GetFireInterval() {
        return fireInterval;
    }

    public int GetID() {
        return GetInstanceID();
    }

    void OnDrawGizmosSelected() {
        if (showRange) {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
