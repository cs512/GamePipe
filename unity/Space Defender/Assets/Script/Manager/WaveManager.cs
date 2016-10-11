using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaveManager : MonoBehaviour {

    private Level level;
    private int currentWave = 0;
    private List<GameObject> ebs = new List<GameObject>();
    private int time = 0;
    private int waveDuring = 0;
    private int counter = 0;
    private bool wavesCompleted = false;

    // Use this for initialization
    void Start() {
        this.level = Toolbox.Instance.GetComponent<LevelManager>().GetCurrentLevel();
        this.time = (int)Time.time;
        this.SetWave(currentWave);
    }

    public int GetCurrentWave() {
        return this.currentWave;
    }

    void SetWave(int n) {
        foreach(GameObject eb in this.ebs) {
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
                eb.damage = sp.damage;
                eb.speed = sp.speed;
                eb.wave = new EnemyBuilder.WaveComponent[1];
                eb.wave[0] = new EnemyBuilder.WaveComponent();
                eb.wave[0].num = sp.number;
                eb.wave[0].enenmyPrefab = Resources.Load("Prefabs/" + sp.prefab, typeof(GameObject)) as GameObject;
                ebs.Add(go);
                Instantiate(go);
            }
            this.waveDuring = level.waves[n].waveDuring;
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
        if (waveDuring != 0) {
            if ((Time.time - this.time) > this.waveDuring) {
                this.time = (int)Time.time;
                this.currentWave++;
                this.SetWave(this.currentWave);
            }
        }
    }
}
