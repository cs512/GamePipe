using UnityEngine;
using System.Collections;

public class SourcePlanet : MonoBehaviour, Victim {

    public float rotateSpeed;
    public float health;
    public GameObject shockHit;
    public GameObject explosion;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed, Space.World);
    }

    int Victim.GetID() {
        return GetInstanceID();
    }

    void Victim.DealDamage(float damage) {
        this.health -= damage;
        GameObject shockWave = Instantiate(shockHit, transform.position, transform.rotation) as GameObject;
        Destroy(shockWave, 2);
        if (health <= 0f) {
            GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            Destroy(boom, 2);
            GetComponent<Renderer>().enabled =false;
        }
    }

    float Victim.GetHealth() {
        return this.health;
    }

    GameObject Victim.GetGameObject() {
        return this.gameObject;
    }
}
