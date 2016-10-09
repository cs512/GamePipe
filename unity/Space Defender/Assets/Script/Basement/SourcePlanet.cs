using UnityEngine;
using System.Collections;

public class SourcePlanet : MonoBehaviour, Victim {

    public float rotateSpeed;
    public float health;
    public GameObject shockHit;
    public GameObject explosion;
    public float fund;
    public float fund_lost;
    public int intervalRate;

    // Use this for initialization
    void Start() {
        intervalRate = 3;
        fund_lost = 5.0f;
        fund = 100.0f;
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed, Space.World);
        loseFund();
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

    void loseFund() 
    {
        if (Time.frameCount % intervalRate == 0)
        {
            fund -= fund_lost;
        }
    }

    public float getFund()
    {
        return fund;
    }
}
