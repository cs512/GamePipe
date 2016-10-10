using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Level {

    public class Wave {
        public class SpawnPoint {
            public Vector3 position;
            public string prefab;
            public float interval;
            public int number;
        }
        public int waveDuring;
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    }

    public string name;
    public List<Wave> waves = new List<Wave>();

    public string GetName() {
        return this.name;
    }
}
