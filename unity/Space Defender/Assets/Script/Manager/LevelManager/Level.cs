using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Level {

    public class Wave {
        public class SpawnPoint {
            Vector3 position;
            string prefab;
            float interval;
            int number;
        }
        int waveDuring;
        int spawnPointCount;
        List<SpawnPoint> spawnPoints;
    }

    public string name;
    public List<Wave> waves;

    public string GetName() {
        return this.name;
    }
}
