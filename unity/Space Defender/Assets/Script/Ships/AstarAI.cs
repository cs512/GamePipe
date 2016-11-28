using UnityEngine;
using System.Collections;
using Pathfinding;

public class AstarAI : Enemy {
	
	public Vector3 targetPosition;
	
    private Seeker seeker;
    private CharacterController controller;
 
    public Path path;
    public float aiSpeed = 200;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
	
	public void Start () {
		targetPosition = target.transform.position;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
 
        Dispatcher dispatcher = GameObject.Find("Dispatcher").GetComponent<Dispatcher>();
        dispatcher.enemyRegisteVictim(this);
        dispatcher.enemyRegisteKiller(this);
        SetOldSpeed (aiSpeed);
        SetMaxHealth (health);
	}
	
    public void OnPathComplete (Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
 
    public void Update () {
        if (path == null) {
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count) {
            return;
        }
        
        Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= aiSpeed * Time.deltaTime;
        controller.Move (dir);
        Quaternion rotation = Quaternion.LookRotation (dir);
        this.transform.rotation = Quaternion.Lerp (this.transform.rotation, rotation, Time.deltaTime * 5);
        
        
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
    public override void SetUpDefaultAttributions() {

    }
	
}
