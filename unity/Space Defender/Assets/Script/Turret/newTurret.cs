using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class newTurret : MonoBehaviour, Killer {

    private Vector3 screenPoint;
    private Vector3 offset;

    public int fireInterval = 100;
    Dictionary<int, Victim> victims;
    public Transform shotSpawn;
    public GameObject shot;
    public float rotateSpeed = 5;

    private GameObject currentTarget = null;
    private Victim currentVictim = null;
    private float nextFire = 0;
    private Quaternion lastRotation;

    public GameObject target1;
    public GameObject target2;
    public ArrayList targetArray;


    // Use this for initialization
    void Start() {
        targetArray = new ArrayList();
        targetArray.Add(GameObject.Find("Armageddon").GetInstanceID());
        targetArray.Add(GameObject.Find("Naga").GetInstanceID());
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
        foreach (int id in targetArray) {
            GameObject target = (GameObject)EditorUtility.InstanceIDToObject(id);
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (min_dist >= distance) {
                currentTarget = target;
                min_dist = distance;
            }
        }
        print(currentTarget.name);
        if (!roatateToTarget()) {
            ShotSpawn();
        }
    }

    public void ShotSpawn() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireInterval;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
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

    public int GetFireInterval() {
        return fireInterval;
    }

    public int GetID() {
        return GetInstanceID();
    }
}
