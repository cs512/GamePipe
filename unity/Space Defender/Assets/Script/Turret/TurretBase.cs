using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class TurretBase : MonoBehaviour, Killer,Victim {

    private Vector3 screenPoint;
    private Vector3 offset;

    public int fireInterval;
    public Dictionary<int, Victim> victims;

    public float rotateSpeed;

    public Victim currentVictim = null;
    public float nextFire = 1;
    private Quaternion lastRotation;
    public Transform currentTarget = null;

    public bool showRange;
    public float range;

    public float turretCost = 50.0f;
    
    public float health;
    
    public GameObject explosion;

	//public Slider healthSlider;

	private float maxHealth;

    void Start() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.turretRegisteVictim(this);
        dispatcher.turretRegisteKiller(this);
        GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(turretCost);

		maxHealth = health;
    }

	void OnEnable() {
		SetHealthUI();
	}

    void Update() {
        if (currentTarget != null) {
            targetLockOn();
        }
    }

    void DestroySelf() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.turretDeregisteKiller(this);
        dispatcher.turretDeregisteVictim(this);
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
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        if (victims.Count != 0) {
            float min_dist = float.MaxValue;
            print ("current target: " + currentTarget);
            if (currentTarget != null && (range < Vector3.Distance (currentTarget.position, transform.position) || currentVictim.GetHealth () <= 0f)) {
                currentTarget = null;
                currentVictim = null;
            }
            if (currentTarget == null) {
                foreach (int id in victims.Keys) {
                    print (id + " : " + victims [id]);
                    if (dispatcher.enemyVictims.ContainsKey (id)) {
                        GameObject targetObj = victims[id].GetGameObject ();
                        Transform target = targetObj.transform;
                        float distance = Vector3.Distance (target.position, transform.position);
                        if (range < distance)
                            continue;
                        if (min_dist >= distance) {
                            currentTarget = target;
                            currentVictim = victims [id];
                            min_dist = distance;
                        }
                    }
                }
            }
            if (currentTarget != null) {
                ShotSpawn ();
            }
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
    
    int Victim.GetID() {
        return GetInstanceID();
    }
    
    //regard turretBase as a victim
    public int GetFireInterval() {
        return fireInterval;
    }

    int Killer.GetID() {
        return GetInstanceID();
    }
    
    public void DealDamage(float damage) {
        health -= damage;
        if (health <= 0f) {
            this.DestorySelf();
        }
		SetHealthUI();
    }

    public void DestorySelf() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.turretDeregisteVictim(this);
        dispatcher.turretDeregisteKiller(this);
        GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        Destroy(boom, 2);
    }

    public float GetHealth() {
        return health;
    }
    public GameObject GetGameObject() {
        return this.gameObject;
    }

	void SetHealthUI() {
		//healthSlider.value = health / maxHealth * 100;
	}
}
