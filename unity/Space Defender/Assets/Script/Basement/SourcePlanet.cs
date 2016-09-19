using UnityEngine;
using System.Collections;

public class SourcePlanet : MonoBehaviour, Victim {

    public float rotateSpeed;
    public float health;
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
    }

    float Victim.GetHealth() {
        return this.health;
    }

    GameObject Victim.GetGameObject() {
        return this.gameObject;
    }
}
