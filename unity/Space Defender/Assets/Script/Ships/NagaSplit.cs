using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding;

class NagaSplit : Enemy, Victim
{
    public GameObject ng;

    /*public void Start()
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
    }*/

    public Vector3 targetPosition;

    private Seeker seeker;
    private CharacterController controller;

    public Path path;
    public float aiSpeed = 50;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;

    public void Start()
    {
        targetPosition = target.transform.position;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);

        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.enemyRegisteVictim(this);
        dispatcher.enemyRegisteKiller(this);
        SetOldSpeed(aiSpeed);
        SetMaxHealth(health);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void Update()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            return;
        }

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= aiSpeed * Time.deltaTime;
        controller.Move(dir);
        Quaternion rotation = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, Time.deltaTime * 5);


        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
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

    

    public override void SetUpDefaultAttributions() {

    }
}
