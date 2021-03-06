﻿using UnityEngine;
using System.Collections;

public abstract class BulletMover : MonoBehaviour {
    public float speed;
    public float damage;
    public float slowRate;
    public float bulletRotateSpeed = 100f;
    public Transform target;
    private Vector3 lastTargetPosition;
    void Start() {
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //Destroy(gameObject, 10);
        InvokeRepeating("changeDirection", 0.1f, 0.05f);
    }
    public void changeDirection() {
        float framDist;
        Vector3 dir;
        if (target == null) {
            transform.position += transform.forward * Time.deltaTime * speed;
            Destroy(this.gameObject, 5);

            // the enemy went away!
            return;
        }
        //Debug.Log(target.position);
        lastTargetPosition = target.position;
        dir = target.position - this.transform.localPosition;
        framDist = speed * Time.deltaTime;
        transform.Translate(dir.normalized * framDist, Space.World);
        this.transform.rotation = Quaternion.LookRotation(dir);
    }

    public float setDamage(float inputDamage) {
        damage = inputDamage;
        return damage;
    }
    public float getDamage() {
        return this.damage;
    }
    public float setSlowRate(float inputSlowRate)
    {
        slowRate = inputSlowRate;
        return slowRate;
    }
    public float getSlowRate()
    {
        return this.slowRate;
    }
    public void setTarget(Transform inputTarget) {
        target = inputTarget;
    }
    public Transform getTarget() {
        return this.target;
    }
    abstract public void OnTriggerEnter(Collider other);

}
