using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {

    // Use this for initialization
    void Start() {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    // Update is called once per frame
    void Update() {

    }
}
