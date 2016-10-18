using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Dispatcher : MonoBehaviour {

    private TimerManager tm = new TimerManager();
    public Dictionary<int, Killer> enemyKillers = new Dictionary<int, Killer>();
    public Dictionary<int, Victim> enemyVictims = new Dictionary<int, Victim>();
    public Dictionary<int, Killer> turretKillers = new Dictionary<int, Killer>();
    public Dictionary<int, Victim> turretVictims = new Dictionary<int, Victim>();
    // Use this for initialization
    void Start() {
        print("Dispatcher start");
    }

    // Update is called once per frame
    void Update() {
        foreach (IAnimatable animatable in tm.timerList) {
            animatable.AdvanceTime();
        }
    }

    public int GetEnemyCount() {
        return this.enemyKillers.Count;
    }

    public void enemyRegisteKiller(Killer killer) {
        print("enemyRegister: killer" + killer.GetID().ToString());
        if (!this.enemyKillers.ContainsKey(killer.GetID())) {
            this.enemyKillers.Add(killer.GetID(), killer);
            this.tm.doOnce<int, Killer>(killer.GetFireInterval(), this.enemyTriggerAttack, killer.GetID(), killer);
        }
        return;
    }

    public void enemyDeregisteKiller(Killer killer) {
        if (this.enemyKillers.ContainsKey(killer.GetID())) {
            this.enemyKillers.Remove(killer.GetID());
        }
        return;
    }

    public void enemyRegisteVictim(Victim victim) {
        print("Register: " + victim.GetID().ToString());
        if (!this.enemyVictims.ContainsKey(victim.GetID())) {
            this.enemyVictims.Add(victim.GetID(), victim);
        }
        return;
    }

    public void enemyDeregisteVictim(Victim victim) {
        if (this.enemyVictims.ContainsKey(victim.GetID())) {
            this.enemyVictims.Remove(victim.GetID());
        }
        return;
    }
    
    public void turretRegisteKiller(Killer killer) {
        print("enemyRegister: killer" + killer.GetID().ToString());
        if (!this.turretKillers.ContainsKey(killer.GetID())) {
            this.turretKillers.Add(killer.GetID(), killer);
            this.tm.doOnce<int, Killer>(killer.GetFireInterval(), this.turretTriggerAttack, killer.GetID(), killer);
        }
        return;
    }

    public void turretDeregisteKiller(Killer killer) {
        if (this.turretKillers.ContainsKey(killer.GetID())) {
            this.turretKillers.Remove(killer.GetID());
        }
        return;
    }

    public void turretRegisteVictim(Victim victim) {
        print("Register: " + victim.GetID().ToString());
        if (!this.turretVictims.ContainsKey(victim.GetID())) {
            this.turretVictims.Add(victim.GetID(), victim);
        }
        return;
    }

    public void turretDeregisteVictim(Victim victim) {
        if (this.turretVictims.ContainsKey(victim.GetID())) {
            this.turretVictims.Remove(victim.GetID());
        }
        return;
    }

    private void enemyTriggerAttack(int instanceID, Killer killer) {
        if (this.enemyKillers.ContainsKey(instanceID)) {
            killer.Attack(this.turretVictims);
            this.tm.doOnce<int, Killer>(killer.GetFireInterval(), this.enemyTriggerAttack, instanceID, killer);
        }
        return;
    }
    private void turretTriggerAttack(int instanceID, Killer killer) {
        if (this.turretKillers.ContainsKey(instanceID)) {
            killer.Attack(this.enemyVictims);
            this.tm.doOnce<int, Killer>(killer.GetFireInterval(), this.turretTriggerAttack, instanceID, killer);
        }
        return;
    }
}