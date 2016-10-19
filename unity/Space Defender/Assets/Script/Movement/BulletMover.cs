using UnityEngine;
using System.Collections;

public abstract class BulletMover : MonoBehaviour {
    public float speed;
    public float damage;
    public float bulletRotateSpeed = 100f;
    public Transform target;
    void Start() {
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //Destroy(gameObject, 10);
        InvokeRepeating("changeDirection", 0.1f, 0.05f);
    }
    public void changeDirection() {
        if (target == null) {
            // the enemy went away!
            Destroy(this.gameObject);
            return;
        }
        //Debug.Log(target.position);
        Vector3 dir = target.position - this.transform.localPosition;
        float framDist = speed * Time.deltaTime;
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
    public void setTarget(Transform inputTarget) {
        target = inputTarget;
    }
    public Transform getTarget() {
        return this.target;
    }
    abstract public void OnTriggerEnter(Collider other);

}
