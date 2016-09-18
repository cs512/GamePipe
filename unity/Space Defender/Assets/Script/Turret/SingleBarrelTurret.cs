using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SingleBarrelTurret : MonoBehaviour, Killer {

    private Vector3 screenPoint;
    private Vector3 offset;

    public int fireInterval;
    Dictionary<int, Victim> victims;
    public Transform shotSpawn;
    public GameObject shot;
    public float rotateSpeed;

    private Victim currentVictim = null;
    private float nextFire = 1;
    private Quaternion lastRotation;

    public GameObject target1;
    public GameObject target2;
    public ArrayList targetArray;
    private Transform currentTarget = null;

    public bool showRange;
    public float range;


    // Use this for initialization
    void Start() {
        targetArray = new ArrayList();
        targetArray.Add(target1.GetInstanceID());
        targetArray.Add(target2.GetInstanceID());
        init();
    }

    // Update is called once per frame
    void Update() {
        Attack(victims);
    }

    void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag() {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }




    public void init() {
        lastRotation = transform.rotation;
    }

    public void Attack(Dictionary<int, Victim> victims) {
        float min_dist = float.MaxValue;
        if (currentTarget == null || range < Vector3.Distance(currentTarget.position, transform.position)) {
            foreach (int id in targetArray) {
                Transform target = ((GameObject)EditorUtility.InstanceIDToObject(id)).transform;
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
        Debug.Log(Time.time + "and " + nextFire);
        if (Time.time > nextFire) {
            Debug.Log(Time.time);
            nextFire = Time.time + fireInterval;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    bool roatateToTarget() {
        Vector3 targetDir = currentTarget.transform.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (lastRotation == transform.rotation)  // check if turret is facing target
            return false;

        lastRotation = transform.rotation;
        return true;
    }
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
