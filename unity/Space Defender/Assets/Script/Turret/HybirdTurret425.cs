using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HybirdTurret425: MonoBehaviour, Killer {

    void Start() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.RegisteKiller(this);
    }

    int Killer.GetFireInterval() {
        return 500;
    }

    int Killer.GetID() {
        return GetInstanceID();
    }

    void Killer.Attack(Dictionary<int, Victim> victims) {
        return;
    }

    void Killer.ShotSpawn() {
        return;
    }
}
