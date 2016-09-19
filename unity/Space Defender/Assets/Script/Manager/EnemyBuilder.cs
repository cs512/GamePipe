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
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        target = GameObject.Find("SourcePlanet");
        sourcePlanet = target.transform;
        Vector3 dir = sourcePlanet.position - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90f, 0f, 0f);
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
                    GameObject enemyGO = Instantiate(wc.enenmyPrefab, this.transform.position, this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                    Enemy b = enemyGO.GetComponent<Enemy>();
                    b.SetTarget(sourcePlanet);
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
