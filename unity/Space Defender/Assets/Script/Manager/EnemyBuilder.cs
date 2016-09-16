using UnityEngine;
using System.Collections;

public class EnemyBuilder : MonoBehaviour {

    public float nextWaveTime = 5f;
    public float intervelTime = 0.5f;
    public float damage = 0.5f;
    public float speed = 0.5f;
    GameObject target;
    Transform sourcePlanet;

    [System.Serializable]
    public class WaveComponent {
        public GameObject enenmyPrefab;
        public int num;
        public int shooted = 0;
    }
    public WaveComponent[] wave;
    // Use this for initialization
    void Start() {
        target = GameObject.Find("SourcePlanet");
        sourcePlanet = target.transform;
    }

    // Update is called once per frame
    void Update() {
        nextWaveTime -= Time.deltaTime;
        if (nextWaveTime < 0) {
            nextWaveTime = intervelTime;
            bool finised = true;
            //This wave Enemy Comes
            foreach (WaveComponent wc in wave) {
                if (wc.shooted < wc.num) {
                    //shoot it, enenmyPrefab is a prefab object
                    finised = false;
                    wc.shooted++;
                    GameObject enemyGO = (GameObject)Instantiate(wc.enenmyPrefab, this.transform.position, this.transform.rotation);
                    Bullet b = enemyGO.GetComponent<Bullet>();
                    b.target = sourcePlanet;
                    b.damage = damage;
                    b.speed = speed;
                    break;
                }
            }
            if (finised == true) {
                Destroy(gameObject);
                //or Instantiate the next wave enemy
            }
        }
    }
}
