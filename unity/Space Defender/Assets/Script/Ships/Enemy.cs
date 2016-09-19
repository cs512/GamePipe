using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, Victim {

    private Transform target;
    protected float speed;
    protected float damage;
    protected float health;

    // Use this for initialization
    void Start() {
        this.SetUpDefaultAttributions();
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.RegisteVictim(this);
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
        dispatcher.DeregisteVictim(this);
		Destroy(gameObject);
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
}
