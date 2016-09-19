using UnityEngine;

public class DoubleBarrels : TurretBase {

    public Transform shotSpawnR;
    public Transform shotSpawnL;
    public GameObject shot;

    public override void SetUpAttributions() {
        this.fireInterval = 1000;
        return;
    }

    override public void ShotSpawn() {
        Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation);
        Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
