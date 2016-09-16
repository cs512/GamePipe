using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour {

    public float speed = 20f;
    void Start() {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
