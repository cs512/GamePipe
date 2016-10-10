using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;

class LevelManager : MonoBehaviour {

    private List<Level> levels;

    void Awake() {
        print("LevelManager Start");
        TextAsset text = Resources.Load("GameData/Entry") as TextAsset;
        print(text.text);
        var levelList = JSON.Parse(text.text);
        int count = levelList["levelCount"].AsInt;
        for(int i = 0; i < count; ++i) {
            Level level = new Level();
            level.name = levelList["levels"][i]["name"];
            var levelConfig = JSON.Parse(
                (Resources.Load("GameData/"+ levelList["levels"][i]["fileName"])
                as TextAsset).text);
            for(int j = 0; j < levelConfig["waveCount"].AsInt; ++j) {
                var waveJSON = levelConfig["waves"][j];
                Level.Wave wave = new Level.Wave();
                wave.waveDuring = waveJSON["waveDuring"].AsInt;
                for(int k = 0; k < waveJSON["spawnPointCount"].AsInt; ++k) {
                    var spJSON = waveJSON["spawnPoints"][k];
                    Level.Wave.SpawnPoint sp = new Level.Wave.SpawnPoint();
                    sp.interval = spJSON["interval"].AsInt;
                    sp.number = spJSON["number"].AsInt;
                    sp.prefab = spJSON["prefab"];
                    sp.position = new Vector3(spJSON["position"][0].AsFloat, spJSON["position"][1].AsFloat, spJSON["position"][2].AsFloat);
                    wave.spawnPoints.Add(sp);
                }
                level.waves.Add(wave);
            }
        }
    }

    public void JumpLevel(int level) {
        return;
    }

    public List<string> GetLevelNames() {
        List<string> res = new List<string>();
        foreach(Level level in this.levels) {
            res.Add(level.GetName());
        }
        return res;
    }
    
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
