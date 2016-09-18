using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HybirdTurret425: DragTurret {

    void Start() {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.RegisteKiller(this);
        this.fireInterval = 2000;
    }

    override public void Attack(Dictionary<int, Victim> victims) {
        Debug.Log("Attack!");
        return;
    }

    override public void ShotSpawn() {
        return;
    }
}
