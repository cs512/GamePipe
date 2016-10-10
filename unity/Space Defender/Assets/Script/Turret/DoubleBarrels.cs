using UnityEngine;

public class DoubleBarrels : TurretBase {

    public Transform shotSpawnR;
    public Transform shotSpawnL;
    public GameObject shot;

    public override void SetUpAttributions() {
        return;
    }

    override public void ShotSpawn() {
        Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation);
        Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation);
		BulletMover bullet = shot.GetComponent<BulletMover>();
		bullet.setTarget(currentTarget);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
