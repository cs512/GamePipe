using UnityEngine;
using System.Collections;

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
            Destroy(gameObject);
            return;
        }
        //Debug.Log(target.position);
        Vector3 dir = target.position - this.transform.position;
        float framDist = speed * Time.deltaTime;
        transform.Translate(dir.normalized * framDist, Space.World);
        this.transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.gameObject.transform.name == target.name) {
            Victim victim = collisionInfo.gameObject.GetComponent<Victim>();
            if (victim != null) {
                victim.DealDamage(this.damage);
                this.DestorySelf();
            }
        }
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.RegisteVictim(this);
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
        Destroy(this);
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

    abstract public void SetUpDefaultAttributions();
}
