using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding;

public class NagaRepair : Enemy {
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

    public Transform target;
    public Vector3 targetPosition;

    private Seeker seeker;
    private CharacterController controller;

    public Path path;
    public float aiSpeed = 200;
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
        SetAbility();
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

    public new void SetAbility()
    {       
        InvokeRepeating("RecoverHealth", 0f, 1f);
        print("Repair: " + speed);
    }

    

    public override void SetUpDefaultAttributions() {

    }
}
