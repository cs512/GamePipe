﻿using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;

class LevelManager : MonoBehaviour {

    private List<Level> levels = new List<Level>();
    private List<Level> skiLevels = new List<Level>();
    private int level;
    private int skiLevel;
    
    private int mode;//0--story mode, 1--ski mode

    void Awake() {
        //print("LevelManager Start");
    }
    
    public void LoadData(int modeName){
        TextAsset text;
        if (modeName==0) {
            text = Resources.Load ("GameData/Entry") as TextAsset;
            this.mode = 0;
        } else if (modeName==1) {
            text = Resources.Load ("SkiData/Entry") as TextAsset;
            this.mode = 1;
        } else {
            return;
        }
        //print(text.text);
        var levelList = JSON.Parse(text.text);
        int count = levelList["levelCount"].AsInt;
        for (int i = 0; i < count; ++i) {
            Level level = new Level();
            level.name = levelList["levels"][i]["name"];
            if (this.mode == 0) {
                var levelConfig = JSON.Parse(
                    (Resources.Load("GameData/" + levelList["levels"][i]["fileName"])
                        as TextAsset).text);
                level.turretMask = levelConfig["turretMask"].AsInt;
                for (int j = 0; j < levelConfig["waveCount"].AsInt; ++j) {
                    var waveJSON = levelConfig["waves"][j];
                    Level.Wave wave = new Level.Wave();
                    wave.waveDuring = waveJSON["waveDuring"].AsInt;
                    for (int k = 0; k < waveJSON["spawnPointCount"].AsInt; ++k) {
                        var spJSON = waveJSON["spawnPoints"][k];
                        Level.Wave.SpawnPoint sp = new Level.Wave.SpawnPoint();
                        sp.interval = spJSON["interval"].AsFloat;
                        sp.number = spJSON["number"].AsInt;
                        sp.prefab = spJSON["prefab"];
                        sp.speed = spJSON["speed"].AsFloat;
                        sp.position = new Vector3(spJSON["position"][0].AsFloat, spJSON["position"][1].AsFloat, spJSON["position"][2].AsFloat);
                        wave.spawnPoints.Add(sp);
                    }
                    level.waves.Add(wave);
                }
                this.levels.Add(level);
            
            } else if (this.mode == 1) {   
                var levelConfig = JSON.Parse(
                    (Resources.Load("SkiData/" + levelList["levels"][i]["fileName"])
                        as TextAsset).text);
                level.turretMask = levelConfig["turretMask"].AsInt;
                level.terrain = levelConfig["terrain"];
                print(levelList["levels"][i]["fileName"]);
                for (int j = 0; j < levelConfig["waveCount"].AsInt; ++j) {
                    var waveJSON = levelConfig["waves"][j];
                    Level.Wave wave = new Level.Wave();
                    wave.waveDuring = waveJSON["waveDuring"].AsInt;
                    for (int k = 0; k < waveJSON["spawnPointCount"].AsInt; ++k) {
                        var spJSON = waveJSON["spawnPoints"][k];
                        Level.Wave.SpawnPoint sp = new Level.Wave.SpawnPoint();
                        sp.interval = spJSON["interval"].AsFloat;
                        print(spJSON);
                        sp.number = spJSON["number"].AsInt;
                        sp.prefab = spJSON["prefab"];
                        sp.speed = spJSON["speed"].AsFloat;
                        sp.position = new Vector3(spJSON["position"][0].AsFloat, spJSON["position"][1].AsFloat, spJSON["position"][2].AsFloat);
                        wave.spawnPoints.Add(sp);
                    }
                    level.waves.Add(wave);
                }
                this.skiLevels.Add(level);
            }
        }
    }
    public void JumpLevel(int number) {
        this.level = number;
        //print("JUMP TO " + number.ToString());
        SceneManager.LoadScene("AutoGeneratedLevelWithGrid");
        return;
    }
    
    public void JumpLevelSki(int number) {
        this.skiLevel = number;
        //print("JUMP TO " + number.ToString());
        SceneManager.LoadScene("FindingPath");
        return;
    }

    public void JumpToTutorial() {
        this.level = 0;
        SceneManager.LoadScene("Tutorial");
        return;
    }

    public Level GetCurrentLevel() {
        return this.levels[this.level];
    }
    
    public Level GetCurrentLevelSki() {
        return this.skiLevels[this.skiLevel];
    }
    public void ReloadLevel() {
        if (this.mode == 0)
        {
            this.JumpLevel(this.level);
        }
        else
            this.JumpLevelSki(this.skiLevel);
    }
    
    public void ReloadLevelSki() {
        this.JumpLevelSki(this.skiLevel);
    }

    public bool JumpToNextLevel() {
        if (this.mode == 0)
        {
            if (this.level >= this.levels.Count)
                return false;
            else
            {
                this.JumpLevel(this.level+1);
                return true;
            }
        }
        else
        {
            if (this.skiLevel >= this.skiLevels.Count)
                return false;
            else
            {
                this.JumpLevelSki(this.skiLevel+1);
                return true;
            }

        }
    }
    
    public bool JumpToNextLevelSki() {
        if (this.skiLevel >= this.skiLevels.Count)
            return false;
        else {
            this.JumpLevelSki(this.skiLevel);
            return true;
        }
    }

    public List<string> GetLevelNames() {
        List<string> res = new List<string>();
        foreach (Level level in this.levels) {
            res.Add(level.GetName());
        }
        return res;
    }
    
    public List<string> GetLevelNamesSki() {
        List<string> res = new List<string>();
        foreach (Level level in this.skiLevels) {
            res.Add(level.GetName());
        }
        return res;
    }
    
    public int GetMode(){
        return this.mode;
        
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
