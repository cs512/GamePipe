using UnityEngine;
using System.Collections;

public class EnemyBuilderPath : MonoBehaviour {

    public float nextWaveTime;
    public float intervelTime;
    public float speed;
    public bool Pause {
        set {
            pause = value;
        }
    }

    GameObject target;
    Transform sourcePlanet;

    private bool pause = false;

    [System.Serializable]
    public class WaveComponent {
        public GameObject enenmyPrefab;
        public int num;
        public int shooted = 0;
    }
    public WaveComponent[] wave;
    // Use this for initialization
    void Start() {
        //gameObject.transform.Rotate(0,90 * Time.deltaTime,0);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        target = GameObject.Find("SourcePlanet");
        sourcePlanet = target.transform;
        Vector3 dir = sourcePlanet.position - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update() {
        if (pause) {
            return;
        }
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
                    enemyGO.GetComponent<AstarAI>().target = sourcePlanet;
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
