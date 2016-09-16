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

    public void RegisteKiller(string instanceID, Killer killer) {
        if (this.killers.ContainsKey(instanceID)) {
            return;
        } else {
            this.killers.Add(instanceID, killer);
            return;
        }
    }

    public void DeregisterKiller(string instanceID) {
        if (this.killers.ContainsKey(instanceID)) {
            this.killers.Remove(instanceID);
        }
        return;
    }

    public void RegisteVictim(string instanceID, Victim victim) {
        if (this.victims.ContainsKey(instanceID)) {
            return;
        } else {
            this.victims.Add(instanceID, victim);
            return;
        }
    }

    public void DeregisterVictim(string instanceID) {
        if (this.victims.ContainsKey(instanceID)) {
            this.victims.Remove(instanceID);
        }
        return;
    }
}