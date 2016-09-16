using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Dispatcher : MonoBehaviour {

    private TimerManager tm = new TimerManager();
    private Dictionary<string, Killer> killers = new Dictionary<string, Killer>();
    private Dictionary<string, Victim> victims = new Dictionary<string, Victim>();
    // Use this for initialization
    void Start() {
        print("Dispatcher start");
    }

    // Update is called once per frame
    void Update() {

    }

    void registeKiller(string instanceID, Killer killer) {

        return;
    }

    void deregisterAttacker(string instanceID) {

        return;
    }

    void registeVictim(string instanceID, Victim victim) {

        return;
    }

    void deregisterDefender(string instanceID) {

        return;
    }
}