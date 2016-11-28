using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour, Victim, Killer {

    public Transform target;
    public float speed = 100f;
    public float oldSpeed = 0;
    public float damage = 1f;
    public float health = 10f;
    public float cost = 25f;
    public GameObject explosion;
    public int fireInterval = 2000;
    public Transform currentTarget = null;
    public Victim currentVictim = null;
    public float range = 300f;
    public GameObject shot;
    public Transform shotSpawn;
    public Slider healthSlider;
    public int flag = 0;//0--target source planet 1--target turret
    public int setPosition = 0;//0--first time 1-- 

    private float radius;
    private float height;
    private float timeCounter = 0;
    public float maxHealth;
    
    float stopTime;
    float moveTime;
    float vel_x, vel_y, vel_z;//speed
    int curr_frame;
    int total_frame;
    float timeCounter1;
    float timeCounter2;

    public Vector3 nextPathNode;
    public Vector3 sourceTarget;
    public Vector3 turretTarget;
    public float framDist;
    public float turretRadius;
    public float turretAngle;
    public bool isSilenced = false;
    public float silenceTime = 0;
    public float slowTime = 0;

    public int GetFireInterval() {
        return fireInterval;
    }

    // Use this for initialization

    public void Start() {
        oldSpeed = speed; // slow
        this.SetUpDefaultAttributions();
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.enemyRegisteVictim(this);
        dispatcher.enemyRegisteKiller(this);

        float x = target.position.x;
        float z = target.position.z;
        sourceTarget = new Vector3(x, this.transform.localPosition.y, z);
        
        maxHealth = health; // slider
        InvokeRepeating("Forwards", 0f, 0.05f);
        InvokeRepeating("RecoverSpeed", 0f, 2f);
        SetAbility();
    }

	public void Update() {
		RecoverFromSilence();
		RecoverFromSlow();
	}
    
    public void SetAbility()
    {

    }

    public void SetOldSpeed(float curSpeed){
        oldSpeed = curSpeed;
    }
    
    public void SetMaxHealth(float curHealth){
        maxHealth = curHealth;
    }
        
    
    public void RecoverSpeed() {
		if(isSilenced)
			return;
        speed = oldSpeed;
    }

    public void RecoverHealth()
    {
        if (health >= maxHealth)
            return;
        health += 100f;
        print("add blood:" + health);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Forwards() {
        framDist = speed * Time.deltaTime;
        if (target == null) {
            // the enemy went away!
            Destroy(this);
            return;
        }
        //Debug.Log(target.position);
        if (this.flag == 0) {
            Vector3 dir = sourceTarget - this.transform.localPosition;
            transform.Translate (dir.normalized * framDist, Space.World);
            Quaternion rotation = Quaternion.LookRotation (dir);
            this.transform.rotation = Quaternion.Lerp (this.transform.rotation, rotation, Time.deltaTime * 5) * Quaternion.Euler (0f, 0f, 0f);
        } else if (this.flag == 1) {
            Vector3 attackDir = turretTarget - this.transform.localPosition;
            if(attackDir.magnitude <= framDist) {
                // We reached the turret
                this.flag =2;
                turretRadius = Random.Range (80,150);
                turretAngle= Random.Range (0,360);
                float x = Mathf.Cos(turretAngle) * turretRadius + this.transform.localPosition.x;
                float z = Mathf.Sin(turretAngle) * turretRadius + this.transform.localPosition.z;
                nextPathNode = new Vector3(x,this.transform.localPosition.y,z);
            }
            transform.Translate (attackDir.normalized * framDist, Space.World);
            Quaternion attackRotation = Quaternion.LookRotation (attackDir);
            this.transform.rotation = Quaternion.Lerp (this.transform.rotation, attackRotation, Time.deltaTime * 5) * Quaternion.Euler (0f, 0f, 0f);
        } else {
            Vector3 flyDir = nextPathNode - this.transform.localPosition;
            if(flyDir.magnitude <= framDist) {
                // We reached the node
                this.flag =1;
            }
            transform.Translate (flyDir.normalized * framDist, Space.World);
            Quaternion flyRotation = Quaternion.LookRotation (flyDir);
            this.transform.rotation = Quaternion.Lerp (this.transform.rotation, flyRotation, Time.deltaTime * 5) * Quaternion.Euler (0f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void OnEnable() {
        SetHealthUI();
    }

    void SetHealthUI() {
        if (healthSlider)
            healthSlider.value = health / maxHealth * 100;
    }

    public virtual void OnTriggerEnter(Collider colliderObject) {
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
        SetHealthUI();
    }

	public void Silence() {
		speed = 10;
		isSilenced = true;
		silenceTime = 2.0f;
	}

	public void RecoverFromSilence() {
		if(!isSilenced)
			return;
		if(silenceTime <= 0) {
			speed = oldSpeed;
			isSilenced = false;
			silenceTime = 0;
		} else {
			silenceTime -= Time.deltaTime;
		}
	}

	public void RecoverFromSlow() {
		if(!isSilenced)
			return;
		if(slowTime <= 0 && speed < oldSpeed) {
			speed = oldSpeed;
			slowTime = 1;
		} else {
			slowTime -= Time.deltaTime;
		}
	}

    void Victim.SlowDown(float percentage)
    {
        if (speed >= 100)
        {
            speed *= percentage;

            Debug.Log("slowed!" + percentage + "%");
        }
    }

    void Victim.ReduceShield(float damage, Vector3 hittingPoint)
    {
        return;
    }

   public virtual void DestorySelf() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        ScoreBoard sc = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
        sc.LoseFund(-this.cost);
        dispatcher.enemyDeregisteVictim(this);
        dispatcher.enemyDeregisteKiller(this);
        AudioSource audio = GameObject.Find("EnemyDestorySound").GetComponent<AudioSource>();
        audio.Play();
        //Debug.Log("booooom!");
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

    public void SetHealth(float health) {
        this.health = health;
    }

    GameObject Victim.GetGameObject() {
        return this.gameObject;
    }

    abstract public void SetUpDefaultAttributions();

    //regard as a killer
    public void Attack(Dictionary<int, Victim> turretVictims) {
		if(isSilenced) 
			return;
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        if (turretVictims.Count != 0) {
            float min_dist = float.MaxValue;
            if (currentTarget != null && (range < Vector3.Distance(currentTarget.position, transform.position) || currentVictim.GetHealth() <= 0f)) {
                currentTarget = null;
                currentVictim = null;
            }
            if (currentTarget == null) {
                foreach (int id in turretVictims.Keys) {
                    if (dispatcher.turretVictims.ContainsKey(id)) {

                        GameObject targetObj = turretVictims[id].GetGameObject();
                        Transform target = targetObj.transform;
                        float distance = Vector3.Distance(target.position, transform.position);
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
            if (currentTarget != null&&this.flag!=2){
                this.flag = 1;
                float x = currentTarget.position.x;
                float z = currentTarget.position.z;
                turretTarget = new Vector3(x,this.transform.localPosition.y,z);
                ShotSpawn();
                currentTarget.gameObject.GetComponent<TurretBase>().SetShootEnemy(this.gameObject);
            }
        }
    }

    public virtual void ShotSpawn() {
        (Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<EnemyBullet>().setTarget(currentTarget);
    }

    int Killer.GetID() {
        return GetInstanceID();
    }


}
