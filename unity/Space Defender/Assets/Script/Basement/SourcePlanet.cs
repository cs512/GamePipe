using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SourcePlanet : MonoBehaviour, Victim {

    public float rotateSpeed;
    public float health;
    public GameObject shockHit;
    public GameObject explosion;
    public float fund;
    public int fundRate;
    public Slider healthSlider;
    public int startTime;

    private float maxHealth;

    // Use this for initialization
    void Start() {
        fundRate = 1;
        fund = -1.0f;

        maxHealth = health;

        InvokeRepeating("GenerateFund", 0, 1.0f);
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed, Space.World);
    }

    void OnEnable() {
        SetHealthUI();
    }

    void SetHealthUI() {
        if (healthSlider)
            healthSlider.value = health / maxHealth * 100;
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
            GetComponent<Renderer>().enabled = false;
        }
        SetHealthUI();
    }

    float Victim.GetHealth() {
        return this.health;
    }

    GameObject Victim.GetGameObject() {
        return this.gameObject;
    }

    void GenerateFund() {
        GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().LoseFund(fund);
    }


}
