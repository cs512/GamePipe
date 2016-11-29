using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding;

class Split : Enemy, Victim
{
	public GameObject ng;

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

    public override void OnTriggerEnter(Collider colliderObject)
    {
        if (colliderObject.tag == "Planet")
        {
            Victim victim = colliderObject.gameObject.GetComponent<SourcePlanet>();
            victim.DealDamage(this.damage);
            this.Destroy();
        }
        //        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        //        dispatcher.RegisteVictim(this);
    }

    public override void DestorySelf()
	{
		Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
		ScoreBoard sc = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
		sc.LoseFund(-this.cost);

		Vector3 pos = this.transform.position;
		Enemy clone1 = (Instantiate(ng, pos, this.transform.rotation) as GameObject).GetComponent<Enemy>();        
		clone1.SetTarget(GameObject.Find("SourcePlanet").transform);
		pos.z += 30f;
		Enemy clone2 = (Instantiate(ng, pos, this.transform.rotation) as GameObject).GetComponent<Enemy>();
		clone2.SetTarget(GameObject.Find("SourcePlanet").transform);

		dispatcher.enemyDeregisteVictim(this);
		dispatcher.enemyDeregisteKiller(this);
		AudioSource audio = GameObject.Find("EnemyDestorySound").GetComponent<AudioSource>();
		audio.Play();
		//Debug.Log("booooom!");
		GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		Destroy(gameObject);
		Destroy(boom, 2);       
	}

    public void Destroy()
    {
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        ScoreBoard sc = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
        sc.LoseFund(-this.cost);
        dispatcher.enemyDeregisteVictim(this);
        dispatcher.enemyDeregisteKiller(this);
        AudioSource audio = GameObject.Find("EnemyDestorySound").GetComponent<AudioSource>();
        audio.Play();
        //Debug.Log("booooom!");
        GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        Destroy(boom, 2);
    }

    public override void SetUpDefaultAttributions() {

	}
}
