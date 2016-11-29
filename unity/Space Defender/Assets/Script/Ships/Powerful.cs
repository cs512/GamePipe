using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding;

public class Powerful : Enemy {
	public new void Start()
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

    /*public new void SetAbility()
    {
        float damage = (Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<EnemyBullet>().getDamage() * 100f;
        (Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<EnemyBullet>().setDamage(damage);
    }*/

	public override void ShotSpawn()
	{
		EnemyBullet eb = (Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject).GetComponent<EnemyBullet>();
		eb.setTarget(currentTarget);
		float damage = eb.getDamage() * 2f;
		eb.setDamage(damage);

	}

	public override void SetUpDefaultAttributions() {

	}
}
