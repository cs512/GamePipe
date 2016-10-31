using UnityEngine;
using System.Collections;

public class ShotLaser : MonoBehaviour {

	public Transform target;
	public Transform shotSpwan ;
	public LineRenderer laser;

	void Start () {
		
	}

	void Update () {
		FireLaser();
	}

	void FireLaser() {
		laser.SetPosition(0, new Vector3(shotSpwan.position.x, shotSpwan.position.y, shotSpwan.position.z));
		laser.SetPosition(1, new Vector3(target.position.x, shotSpwan.position.y, target.position.z));
	}
	

}
