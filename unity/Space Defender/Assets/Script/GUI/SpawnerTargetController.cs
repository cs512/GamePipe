using UnityEngine;
using System.Collections;

public class SpawnerTargetController : MonoBehaviour {
    void Update() {
        transform.Rotate(0, 0, 20f * Time.deltaTime);

        transform.localScale = new Vector3(Mathf.PingPong(Time.time, 0.5f) + 1f, Mathf.PingPong(Time.time, 0.5f) + 1f, transform.localScale.z);
    }
}