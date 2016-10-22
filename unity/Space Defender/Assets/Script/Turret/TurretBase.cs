using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class TurretBase : MonoBehaviour, Killer, Victim {

    public int fireInterval;
    public Dictionary<int, Victim> victims;
    public float rotateSpeed;
    public Victim currentVictim = null;
    public float nextFire = 1;
    public Transform currentTarget = null;
    public int shipsKilled = 0;
    public int levelUp = 1;
    public bool showRange;
    public float range;
    public float turretCost = 50.0f;
    public float health;
    public GameObject explosion;
	public float buildSpeed = 10.0f;

	private Slider healthSlider;
	private Slider levelSlider;
	private Vector3 screenPoint;
	private Vector3 offset;
	private Quaternion lastRotation;
    private float maxHealth;
	private bool onSet = false;
	private GameObject sliderCanvas;
	private bool built = false;
	private bool findSlider = false;

    void Start() {
//        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
//        dispatcher.turretRegisteVictim(this);
//        dispatcher.turretRegisteKiller(this);
		RaycastHit[] hits; 
		Ray targetRay = new Ray(transform.position, Vector3.down);
		hits = Physics.RaycastAll (targetRay);
		for (int i = 0; i < hits.Length; i++) {
			if (hits[i].collider.gameObject.name.Equals ("Range")) {
				onSet = true;
				Transform range = hits[i].collider.gameObject.transform;
				Vector3 targetVector = range.position;
				Vector3 destVector = new Vector3(targetVector.x, transform.position.y, targetVector.z);
				transform.position = Vector3.MoveTowards(transform.position, destVector, 100f);

				sliderCanvas = range.parent.gameObject.transform.GetChild(0).gameObject;
				Slider[] sliderList = sliderCanvas.GetComponentsInChildren<Slider>();
				foreach(Slider slider in sliderList) {
					if(slider.name.Equals("HealthSlider")) {
						healthSlider = slider;
					} else {
						levelSlider = slider;
					}
				}
				print(healthSlider.value);
			}
		}
        maxHealth = health;
    }

    void Update() {
		if(!findSlider){
			
		}
		if(!built) {
			BuildProcess();
		} else {
			if (currentTarget != null) {
				targetLockOn();
			}
		}
        
    }

    void DestroySelf() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.turretDeregisteKiller(this);
        dispatcher.turretDeregisteVictim(this);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(this);

    }

	void BuildProcess() {
		healthSlider.value += Time.deltaTime * buildSpeed / maxHealth * 100;
		if(healthSlider.value >= 100) {
			built = true;
			SetHealthUI();
			Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
			dispatcher.turretRegisteVictim(this);
			dispatcher.turretRegisteKiller(this);
		}
	}

    abstract public void SetUpAttributions();

//	void OnMouseUp() {
//
//		RaycastHit[] hits; 
//		Ray targetRay = new Ray(transform.position, Vector3.down);
//		hits = Physics.RaycastAll (targetRay);
//		for (int i = 0; i < hits.Length; i++) {
//			if (hits[i].collider.gameObject.name.Equals ("Range")) {
//				onSet = true;
//				Transform range = hits[i].collider.gameObject.transform;
//				Vector3 targetVector = range.position;
//				Vector3 destVector = new Vector3(targetVector.x, transform.position.y, targetVector.z);
//				transform.position = Vector3.MoveTowards(transform.position, destVector, 100f);
//
//				sliderCanvas = range.parent.gameObject.transform.GetChild(0).gameObject;
//				healthSlider = sliderCanvas.GetComponentInChildren<Slider>();
//				return;
//			}
//		}
//		DestorySelf();
//	}
//
//    void OnMouseDown() {
//        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
//        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
//    }
//
//    void OnMouseDrag() {
//		if(!onSet) {
//	        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
//	        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
//	        transform.position = cursorPosition;
//		}
//    }

    public void Attack(Dictionary<int, Victim> victims) {
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
                            shipsKilled += 1;
                            LevelUp();
                            SetLevelUI();
                            min_dist = distance;
                        }
                    }
                }
            }
            if (currentTarget != null) {
                ShotSpawn();
            }
        }
    }

    abstract public void ShotSpawn();
    abstract public void SetShootEnemy(GameObject enemy);
    abstract public void DismissShootEnemy();

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
        return fireInterval / levelUp;
    }

    int Killer.GetID() {
        return GetInstanceID();
    }

    public void DealDamage(float damage) {
        health -= damage;
        if (health <= 0f) {
            this.DestorySelf();
            Destroy(healthSlider);
        }
        SetHealthUI();
    }

    public void SlowDown(float percentage)
    {
        return;
    }

    public void DestorySelf() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.turretDeregisteVictim(this);
        dispatcher.turretDeregisteKiller(this);
        DismissShootEnemy();
        GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        AudioSource audio = GameObject.Find("TurretDestorySound").GetComponent<AudioSource>();
        audio.Play();
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
        healthSlider.value = health / maxHealth * 100;
    }

    void LevelUp()
    {
        if ((shipsKilled + 1) % 10 == 0)
        {
            if (levelUp < 3)
            {
                levelUp += 1;
            } 
        }
    }

    void SetLevelUI()
    {
        levelSlider.value = (levelUp - 1) / 3.0f * 100;
    }

}
