using UnityEngine;
using System.Collections;

public class SourcePlanet : MonoBehaviour, Victim {

    public float rotateSpeed;
    public float health;
    public GameObject explosion;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
    }

    int Victim.GetID() {
        return GetInstanceID();
    }

    void Victim.DealDamage(float damage) {
        this.health -= damage;
        GameObject shockWave = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(shockWave, 2);
    }

    float Victim.GetHealth() {
        return this.health;
    }

    GameObject Victim.GetGameObject() {
        return this.gameObject;
    }
}
