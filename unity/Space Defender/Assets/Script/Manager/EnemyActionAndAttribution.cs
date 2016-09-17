using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class Constants {
    public const float HP = 1; // ship's initial health.
}


class EnemyActionAndAttribution : MonoBehaviour, Victim {
    private float health;

    public EnemyActionAndAttribution() {
        health = Constants.HP;
    }

    void Start() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.RegisteVictim(this);
    }

    void Victim.DealDamage(float damage) {
        health -= damage;
    }

    float Victim.GetHealth() {
        return health;
    }

    int Victim.GetID() {
        return GetInstanceID();
    }
}
