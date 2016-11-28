﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class TurretBase : MonoBehaviour, Killer, Victim {

    public int fireInterval;
    public Dictionary<int, Victim> victims;
    public float rotateSpeed;
    public Victim currentVictim = null;
    public Transform currentTarget = null;
    public int level = 0;
    public bool showRange;
    public float range;
    public float turretCost = 50.0f;
    public float health;
    public GameObject explosion;
    public float buildSpeed = 10.0f;
	public GameObject rangeDisplay;

    private Slider healthSlider;
    private Slider levelSlider;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Quaternion lastRotation;
    private float maxHealth;
    private GameObject sliderCanvas;
    private bool built = false;
	private Collider turretCollider;

    void Start() {
        GameObject.Find("Hex Grid").GetComponent<HexGrid>().SetBuilding(new Vector3(this.transform.position.x, 0, this.transform.position.z), this.gameObject);
        GameObject prefab = Resources.Load("Prefabs/SliderSet", typeof(GameObject)) as GameObject;
        sliderCanvas = Instantiate(prefab, transform.position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
        //sliderCanvas.transform.parent = this.transform;
        Debug.Log("Built!");
        Slider[] sliderList = sliderCanvas.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliderList) {
            if (slider.name.Equals("HealthSlider")) {
                healthSlider = slider;
            } else {
                levelSlider = slider;
            }
        }
        maxHealth = health;
		turretCollider = gameObject.GetComponent<Collider>();
		turretCollider.enabled = false;

		float factor = 2 / rangeDisplay.transform.lossyScale.x;
		rangeDisplay.transform.localScale += new Vector3(range * factor, range * factor);
		rangeDisplay.SetActive(false);
    }

	void Update() {
        SetLevelUI();
        if (!built) {
            BuildProcess();
        } else {
            if (currentTarget != null) {
                TargetLockOn();
            }
        }
    }

    void BuildProcess() {
        healthSlider.value += Time.deltaTime * buildSpeed / maxHealth * 100;
        if (healthSlider.value >= 100) {
            built = true;
            SetHealthUI();
            Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
            dispatcher.turretRegisteVictim(this);
            dispatcher.turretRegisteKiller(this);
			turretCollider.enabled = true;
        }
    }

    abstract public void SetUpAttributions();

    public virtual void Attack(Dictionary<int, Victim> victims) {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        if (victims.Count != 0) {
            float min_dist = float.MaxValue;
            //print ("current target: " + currentTarget);
            if (currentTarget != null && (range < Vector3.Distance(currentTarget.position, transform.position) || currentVictim.GetHealth() <= 0f)) {
                currentTarget = null;
                currentVictim = null;
            }
            if (currentTarget == null) {
                foreach (int id in victims.Keys) {
                    //print (id + " : " + victims [id]);
                    if (dispatcher.enemyVictims.ContainsKey(id)) {
                        GameObject targetObj = victims[id].GetGameObject();
                        Transform target = targetObj.transform;
                        float distance = Vector3.Distance(target.position, transform.position);
                        if (range < distance)
                            continue;
                        if (min_dist >= distance) {
                            currentTarget = target;
                            currentVictim = victims[id];
                            min_dist = distance;
                        }
                    }
                }
            }
			if (currentTarget != null && IsFacingTarget()) {
                ShotSpawn();
            }
        }
    }

    abstract public void ShotSpawn();
    abstract public void SetShootEnemy(GameObject enemy);
    abstract public void DismissShootEnemy();
    abstract public void ReduceShield(float damage, Vector3 hittingPoint);

    bool IsFacingTarget() {
        RaycastHit[] hits;
//        Vector3 lowerPosition = new Vector3 (transform.position.x, 20, transform.position.z);
//        Vector3 higherPosition = new Vector3 (transform.position.x, 44, transform.position.z);
//        hitsSki = Physics.RaycastAll(lowerPosition, transform.forward, range);
//        hitsStory = Physics.RaycastAll(lowerPosition, transform.forward, range);
        hits = Physics.RaycastAll(transform.position, transform.forward, range);
        foreach(RaycastHit hit in hits) {
			if(currentTarget == hit.transform){
				return true;
			}
		}
		return false;
	}

    void TargetLockOn() {
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
        int upgradedfireInterval = fireInterval;
        upgradedfireInterval = fireInterval * (10 - level*2) / 10;
        return upgradedfireInterval;
    }

    int Killer.GetID() {
        return GetInstanceID();
    }

    public void DealDamage(float damage) {
        health -= damage;
        if (health <= 0f) {
            levelSlider.value = 0;
            this.DestorySelf();
            //            Destroy(healthSlider);
            //            Destroy(levelSlider);
        }
        SetHealthUI();
    }

    public void SlowDown(float percentage) {
        return;
    }

    public void DestorySelf() {
        GameObject.Find("Hex Grid").GetComponent<HexGrid>().DeleteBuilding(new Vector3(this.transform.position.x, 0, this.transform.position.z));
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.turretDeregisteVictim(this);
        dispatcher.turretDeregisteKiller(this);
        DismissShootEnemy();
        GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        AudioSource audio = GameObject.Find("TurretDestorySound").GetComponent<AudioSource>();
        audio.Play();
        Destroy(gameObject);
        Destroy(sliderCanvas);
        Destroy(boom, 2);
    }

    public void Sold() {
        GameObject.Find("Hex Grid").GetComponent<HexGrid>().DeleteBuilding(new Vector3(this.transform.position.x, 0, this.transform.position.z));
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.turretDeregisteVictim(this);
        dispatcher.turretDeregisteKiller(this);
        DismissShootEnemy();
        AudioSource audio = GameObject.Find("TurretSellingSound").GetComponent<AudioSource>();
        audio.Play();
        Destroy(gameObject);
        Destroy(sliderCanvas);
    }

    public float GetHealth() {
        return health;
    }
    public GameObject GetGameObject() {
        return this.gameObject;
    }

    void SetHealthUI() {
        healthSlider.value = health / maxHealth * 100;
    }

    void SetLevelUI() {
        levelSlider.value = level / 3.0f * 100;
    }

}
