using UnityEngine;
using System.Collections;

public class EnemyBuilder : MonoBehaviour {

    public float nextWaveTime;
    public float intervelTime;
    public float speed;
    
    private int gameMode;
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
    
    [HideInInspector] // Hides var below
    public WaveComponent wave;
    // Use this for initialization
    void Start() {
        //gameObject.transform.Rotate(0,90 * Time.deltaTime,0);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        target = GameObject.Find("SourcePlanet");
        sourcePlanet = target.transform;
        Vector3 dir = sourcePlanet.position - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90f, 0f, 0f);
        this.gameMode = Toolbox.Instance.GetComponent<LevelManager>().GetMode();
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
            if (this.wave.shooted < this.wave.num) {
                finised = false;
                this.wave.shooted++;                
                if (this.gameMode == 0) {
                    GameObject enemyGO = Instantiate(this.wave.enenmyPrefab, this.transform.position, this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                    Enemy b = enemyGO.GetComponent<Enemy>();
                    b.SetTarget(sourcePlanet);
                }else if(this.gameMode == 1){
                    GameObject go = (GameObject)Instantiate(this.wave.enenmyPrefab, this.transform.position, this.transform.rotation);
                    go.GetComponent<AstarAI>().target = sourcePlanet;
                }
            }
            if (finised == true) {
                Destroy(gameObject);
                //or Instantiate the next wave enemy
            }
        }
    }
}
