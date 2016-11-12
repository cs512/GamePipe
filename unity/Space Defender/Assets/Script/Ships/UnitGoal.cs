using UnityEngine;
using System.Collections;

public class UnitGoal : MonoBehaviour {
	
	void OnTriggerEnter(Collider c) {
		Destroy(c.gameObject);
	}
	
}
