using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Dispatcher : MonoBehaviour {

    private TimerManager tm = new TimerManager();
    private Dictionary<int, Killer> killers = new Dictionary<int, Killer>();
    private Dictionary<int, Victim> victims = new Dictionary<int, Victim>();
    // Use this for initialization
    void Start() {
        print("Dispatcher start");
    }

    // Update is called once per frame
    void Update() {
        foreach (IAnimatable animatable in TimerManager.timerList) {
            animatable.AdvanceTime();
        }
    }

    public void RegisteKiller(Killer killer) {
        if (!this.killers.ContainsKey(killer.GetID())) {
            this.killers.Add(killer.GetID(), killer);
            this.tm.doOnce<int, Killer>(killer.GetFireInterval(), this.TriggerAttack, killer.GetID(), killer);
        }
        return;
    }

    public void DeregisteKiller(Killer killer) {
        if (this.killers.ContainsKey(killer.GetID())) {
            this.killers.Remove(killer.GetID());
        }
        return;
    }

    public void RegisteVictim(Victim victim) {
        print("Register: " + victim.GetID().ToString());
        if (!this.victims.ContainsKey(victim.GetID())) {
            this.victims.Add(victim.GetID(), victim);
        }
        return;
    }

    public void DeregisteVictim(Victim victim) {
        if (this.victims.ContainsKey(victim.GetID())) {
            this.victims.Remove(victim.GetID());
        }
        return;
    }

    private void TriggerAttack(int instanceID, Killer killer) {
        if (this.killers.ContainsKey(instanceID))
        killer.Attack(this.victims);
        this.tm.doOnce<int, Killer>(killer.GetFireInterval(), this.TriggerAttack, instanceID, killer);
        return;
    }
}