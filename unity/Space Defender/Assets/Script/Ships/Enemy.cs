using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour, Victim,Killer {

    private Transform target;
    public float speed;
    public float damage;
    public float health;
    public GameObject explosion;
    public int fireInterval;
    public Transform currentTarget = null;
    public Victim currentVictim = null;
    public float range;
    public GameObject shot;
    public Transform shotSpawn;
    
    public int GetFireInterval() {
        return fireInterval;
    }

    // Use this for initialization
    void Start() {
        this.SetUpDefaultAttributions();
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.enemyRegisteVictim(this);
        dispatcher.enemyRegisteKiller(this);
        
    }

    // Update is called once per frame
    void Update() {
        if (target == null) {
            // the enemy went away!
            Destroy(this);
            return;
        }
        //Debug.Log(target.position);
		Vector3 dir = target.position - this.transform.localPosition;
        float framDist = speed * Time.deltaTime;
        transform.Translate(dir.normalized * framDist, Space.World);
        this.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90f, 0f, 0f);
    }

	void OnTriggerEnter(Collider colliderObject){
		if (colliderObject.tag == "Planet") {
			Victim victim = colliderObject.gameObject.GetComponent<SourcePlanet>();
            victim.DealDamage(this.damage);
            this.DestorySelf();
        }
//        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
//        dispatcher.RegisteVictim(this);
    }

    void Victim.DealDamage(float damage) {
        health -= damage;
        if (health <= 0f) {
            this.DestorySelf();
        }
    }

    void DestorySelf() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.enemyDeregisteVictim(this);
        dispatcher.enemyDeregisteKiller(this);
        GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        Destroy(boom, 2);
    }

    float Victim.GetHealth() {
        return health;
    }

    int Victim.GetID() {
        return GetInstanceID();
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    public Transform GetTarget() {
        return this.target;
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }
    GameObject Victim.GetGameObject() {
        return this.gameObject;
    }
    abstract public void SetUpDefaultAttributions();
    
    //regard as a killer
    public void Attack(Dictionary<int, Victim> turretVictims) {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        if (turretVictims.Count != 0) {
            float min_dist = float.MaxValue;
            print ("current target: " + currentTarget);
            if (currentTarget != null && (range < Vector3.Distance (currentTarget.position, transform.position) || currentVictim.GetHealth () <= 0f)) {
                currentTarget = null;
                currentVictim = null;
            }
            if (currentTarget == null) {
                foreach (int id in turretVictims.Keys) {   
                    if (dispatcher.turretVictims.ContainsKey(id)) {
                        print (id + " : " + turretVictims [id]);
                        GameObject targetObj = turretVictims[id].GetGameObject ();
                        Transform target = targetObj.transform;
                        float distance = Vector3.Distance (target.position, transform.position);
                        if (range < distance)
                            continue;
                        if (min_dist >= distance) {
                            currentTarget = target;
                            currentVictim = turretVictims[id];
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

    public void ShotSpawn() {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        EnemyBullet bullet = shot.GetComponent<EnemyBullet>();
        bullet.setTarget(currentTarget);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
    int Killer.GetID() {
        return GetInstanceID();
    }
}
