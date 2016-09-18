using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class turret : TurretActionExample {

	Dictionary<int, Victim> victims;


	// Use this for initialization
	void Start () {
		targetArray = new ArrayList ();
		targetArray.Add (target2.GetInstanceID());
		targetArray.Add (target1.GetInstanceID());
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		Attack (victims);
	}
}
