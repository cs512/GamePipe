using UnityEngine;
using System;
using System.Collections;

public class FloatingLogo : MonoBehaviour {
    float originalY;

    public float floatStrength = 1;

    void Start() {
        this.originalY = this.transform.position.y;
    }

    void Update() {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}
