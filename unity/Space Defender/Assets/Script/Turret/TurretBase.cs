using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public abstract class TurretBase : MonoBehaviour, Killer {

    private Vector3 screenPoint;
    private Vector3 offset;

    public int fireInterval;
    public Dictionary<int, Victim> victims;

    public float rotateSpeed;

    private Victim currentVictim = null;
    public float nextFire = 1;
    private Quaternion lastRotation;
    private Transform currentTarget = null;

    public bool showRange;
    public float range;

    void Start() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.RegisteKiller(this);
        this.SetUpAttributions();
    }

    void DestroySelf() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.DeregisteKiller(this);
        Destroy(this);
    }

    abstract public void SetUpAttributions();

    void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag() {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }

    public void Attack(Dictionary<int, Victim> victims) {
        float min_dist = float.MaxValue;
        if (currentTarget != null && (range < Vector3.Distance(currentTarget.position, transform.position) || currentVictim.GetHealth() == 0f)) {
            currentTarget = null;
            currentVictim = null;
        }
        if (currentTarget == null) {
            foreach (int id in victims.Keys) {
                Debug.Log("Victim id is:" + id);
                GameObject targetObj = EditorUtility.InstanceIDToObject(id) as GameObject;
                Transform target = targetObj.transform;
                Debug.Log(target.position.x);

                float distance = Vector3.Distance(target.position, transform.position);
                if (range < distance)
                    continue;
                if (min_dist >= distance) {
                    currentTarget = target;
                    currentVictim = victims[id];
                    min_dist = distance;
                }
            }
        } else {
            targetLockOn();
            ShotSpawn();
        }
    }

    abstract public void ShotSpawn();

    void targetLockOn() {
        Vector3 targetDir = currentTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void OnDrawGizmosSelected() {
        if (showRange) {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }

    public int GetFireInterval() {
        return fireInterval;
    }

    public int GetID() {
        return GetInstanceID();
    }
}
