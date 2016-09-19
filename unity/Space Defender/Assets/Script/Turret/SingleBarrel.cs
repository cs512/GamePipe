using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class SingleBarrel : TurretBase {

    public Transform shotSpawn;
    public GameObject shot;

    public override void SetUpAttributions() {
        this.fireInterval = 1000;
        return;
    }
    override public void ShotSpawn() {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
