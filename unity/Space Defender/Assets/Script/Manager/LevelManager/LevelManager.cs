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
                (Resources.Load("GameData/"+ levelList["levels"][i]["fileName"])as TextAsset)
                .text);
            
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
