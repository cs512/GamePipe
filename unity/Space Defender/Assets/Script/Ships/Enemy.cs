using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour, Victim, Killer
{
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
	public Slider healthSlider;
    public int flag=0;//0--target source planet 1--target turret

	//ship patrol
	public object[] points;
	private int destPoint = 0;
	private Vector3 destination;
	private int patrolMode;

	enum Patrol { Base, Corner, Circle };

	private float radius;
	private float height;
	private float timeCounter = 0;
	private float maxHealth;

	public int GetFireInterval ()
	{
		return fireInterval;
	}

	// Use this for initialization
	void Start ()
	{
		this.SetUpDefaultAttributions ();
		Dispatcher dispatcher = GameObject.Find ("Dispatcher").GetComponent<Dispatcher> ();
		dispatcher.enemyRegisteVictim (this);
		dispatcher.enemyRegisteKiller (this);

		//ship patrol
		patrolMode = (int)Patrol.Circle;   //switch patrol mode
		points = new object[4];
		SetPoints (target, points);
		radius = 200f;
		height = this.transform.position.y;
        
		if (patrolMode == (int)Patrol.Corner)
			GotoNextPoint ();
		else if (patrolMode == (int)Patrol.Base)
			destination = target.position;
		else if (patrolMode == (int)Patrol.Circle)
			destination = target.position;

		maxHealth = health;
        
        InvokeRepeating("Forwards",0f,0.05f);
	}
    public void Forwards(){
        if (target == null) {
            // the enemy went away!
            Destroy (this);
            return;
        }
        //Debug.Log(target.position);
        Vector3 dir = destination - this.transform.localPosition;
        if (this.flag == 0) {
            float framDist = speed * Time.deltaTime;
            transform.Translate (dir.normalized * framDist, Space.World);
            this.transform.rotation = Quaternion.LookRotation (dir) * Quaternion.Euler (90f, 0f, 0f);
        } else {
            this.transform.rotation = Quaternion.LookRotation (dir) * Quaternion.Euler (90f, 0f, 0f);
        }
        //        if (patrolMode == (int)Patrol.Corner && currentTarget == null && Vector3.Distance(this.transform.position, destination) < 0.5f)
        //            GotoNextPoint();
        //        else if (patrolMode == (int)Patrol.Base && currentTarget == null)
        //            destination = target.position;
        //        else if (patrolMode == (int)Patrol.Circle && currentTarget == null)
        //        {
        //            if (Vector3.Distance(this.transform.position, destination) > radius)
        //                destination = target.position;
        //            else
        //            {
        //                timeCounter += Time.deltaTime;
        //                float x = Mathf.Cos(timeCounter) * radius + destination.x;
        //                float z = Mathf.Sin(timeCounter) * radius + destination.z;
        //                this.transform.position = new Vector3(x, height, z);
        //            }
        //        }
        //        else
        //        {
        //            destination = this.transform.position;
        //        }
    }
    
	// Update is called once per frame
	void OnEnable ()
	{
		SetHealthUI ();
	}

	void SetHealthUI ()
	{
		if (healthSlider)
			healthSlider.value = health / maxHealth * 100;
	}

	void OnTriggerEnter (Collider colliderObject)
	{
		if (colliderObject.tag == "Planet") {
			Victim victim = colliderObject.gameObject.GetComponent<SourcePlanet> ();
			victim.DealDamage (this.damage);
			this.DestorySelf ();
		}
		//        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
		//        dispatcher.RegisteVictim(this);
	}

	void Victim.DealDamage (float damage)
	{
		health -= damage;
		if (health <= 0f) {
			this.DestorySelf ();
		}

		SetHealthUI ();
	}

	void DestorySelf ()
	{
		Dispatcher dispatcher = GameObject.Find ("Dispatcher").GetComponent<Dispatcher> ();
		dispatcher.enemyDeregisteVictim (this);
		dispatcher.enemyDeregisteKiller (this);
		GameObject boom = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		Destroy (gameObject);
		Destroy (boom, 2);
	}

	float Victim.GetHealth ()
	{
		return health;
	}

	int Victim.GetID ()
	{
		return GetInstanceID ();
	}

	public void SetTarget (Transform target)
	{
		this.target = target;
	}

	public Transform GetTarget ()
	{
		return this.target;
	}

	public void SetDamage (float damage)
	{
		this.damage = damage;
	}

	public void SetSpeed (float speed)
	{
		this.speed = speed;
	}

	public void SetHealth (float health)
	{
		this.health = health;
	}

	GameObject Victim.GetGameObject ()
	{
		return this.gameObject;
	}

	abstract public void SetUpDefaultAttributions ();

	//regard as a killer
	public void Attack (Dictionary<int, Victim> turretVictims)
	{
		Dispatcher dispatcher = GameObject.Find ("Dispatcher").GetComponent<Dispatcher>();
		if (turretVictims.Count != 0) {
			float min_dist = float.MaxValue;
			
			if (currentTarget != null && (range < Vector3.Distance (currentTarget.position, transform.position) || currentVictim.GetHealth () <= 0f)) {
				currentTarget = null;
				currentVictim = null;
			}
			if (currentTarget == null) {
				foreach (int id in turretVictims.Keys) {
					if (dispatcher.turretVictims.ContainsKey (id)) {
						
                        GameObject targetObj = turretVictims [id].GetGameObject ();
						Transform target = targetObj.transform;
						float distance = Vector3.Distance (target.position, transform.position);
						if (range < distance)
							continue;
						if (min_dist >= distance) {
							currentTarget = target;
							currentVictim = turretVictims [id];
							min_dist = distance;
						}
					}
				}
			}
			if (currentTarget != null) {
                this.flag =1;
				ShotSpawn();
                currentTarget.gameObject.GetComponent<TurretBase>().SetShootEnemy(this.gameObject);
			}
		}
	}

	public void ShotSpawn ()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		EnemyBullet bullet = shot.GetComponent<EnemyBullet> ();
		bullet.setTarget (currentTarget);
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
	}

	int Killer.GetID ()
	{
		return GetInstanceID ();
	}

	void GotoNextPoint ()
	{
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.

		destination = (Vector3)points [destPoint];

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}

	static public void SetPoints (Transform target, object[] points)
	{
		float x = target.position.x;
		float y = target.position.y;
		float z = target.position.z;

		points [0] = new Vector3 (x - 150, y, z - 150);
		points [1] = new Vector3 (x + 150, y, z - 150);
		points [2] = new Vector3 (x + 150, y, z + 150);
		points [3] = new Vector3 (x - 150, y, z + 150);
	}

}
