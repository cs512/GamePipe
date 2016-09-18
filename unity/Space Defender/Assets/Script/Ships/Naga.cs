using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Naga: MonoBehaviour, Victim  {

    void Start() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.RegisteVictim(this);
    }

    void Victim.DealDamage(float damage) {
        return;
    }

    float Victim.GetHealth() {
        return 10;
    }

    int Victim.GetID() {
        return GetInstanceID();
    }

}
