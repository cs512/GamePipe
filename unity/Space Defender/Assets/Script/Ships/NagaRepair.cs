using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class NagaRepair : Enemy {
    public void Start()
    {
        oldSpeed = speed; // slow
        this.SetUpDefaultAttributions();
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.enemyRegisteVictim(this);
        dispatcher.enemyRegisteKiller(this);

        float x = target.position.x;
        float z = target.position.z;
        sourceTarget = new Vector3(x, this.transform.localPosition.y, z);

        maxHealth = health; // slider
        InvokeRepeating("Forwards", 0f, 0.05f);
        InvokeRepeating("RecoverSpeed", 0f, 2f);
        SetAbility();
    }

    public new void SetAbility()
    {       
        InvokeRepeating("RecoverHealth", 0f, 1f);
        print("Repair: " + speed);
    }

    

    public override void SetUpDefaultAttributions() {

    }
}
