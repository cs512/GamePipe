using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaveManager : MonoBehaviour {


    public bool Pause {
        get {
            return pause;
        }
        set {
            pause = value;
        }
    }

    private Level level;
    private int currentWave = 0;
    private List<GameObject> ebs = new List<GameObject>();
    private float time = 0;
    private int waveDuring = 0;
    private int counter = 0;
    private bool wavesCompleted = false;
    private bool pause;

    // Use this for initialization
    void Start() {
        this.level = Toolbox.Instance.GetComponent<LevelManager>().GetCurrentLevel();
        this.time = Time.time;
        Time.timeScale = 1f;
        this.SetWave(currentWave);
    }

    public int GetCurrentWave() {
        return this.currentWave + 1;
    }

    public int GetRemainedWave() {
        if (level == null) return 0;
        return level.waves.Count - this.currentWave;
    }

    void SetWave(int n) {
        foreach (GameObject eb in this.ebs) {
            Destroy(eb);
        }
        this.ebs = new List<GameObject>();
        if (n < level.waves.Count) {
            foreach (Level.Wave.SpawnPoint sp in level.waves[n].spawnPoints) {
                GameObject go = Resources.Load("Prefabs/EnemySpawnPoint", typeof(GameObject)) as GameObject;
                print(go);
                go.transform.position = sp.position;
                EnemyBuilder eb = go.GetComponent<EnemyBuilder>();
                eb.nextWaveTime = 3.0f;
                eb.intervelTime = sp.interval;
                print(sp.interval);
                eb.speed = sp.speed;
                eb.wave = new EnemyBuilder.WaveComponent[1];
                eb.wave[0] = new EnemyBuilder.WaveComponent();
                eb.wave[0].num = sp.number;
                eb.wave[0].enenmyPrefab = Resources.Load("Prefabs/" + sp.prefab, typeof(GameObject)) as GameObject;
                ebs.Add(Instantiate(go));
            }
            this.waveDuring = level.waves[n].waveDuring;
            print("waveDuring:" + this.waveDuring.ToString());
        } else {
            this.wavesCompleted = true;
            this.waveDuring = 0;
        }
    }

    public bool HasComplete() {
        return this.wavesCompleted;
    }

    // Update is called once per frame
    void Update() {
        if (pause) {
            this.time = Time.time;
            return;
        }
        if (waveDuring != 0) {
            if ((Time.time - this.time) > this.waveDuring) {
                this.time = (int)Time.time;
                this.currentWave++;
                this.SetWave(this.currentWave);
                print(this.currentWave);
            }
        }
    }
}
