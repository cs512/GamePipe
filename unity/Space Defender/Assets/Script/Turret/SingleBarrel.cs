using UnityEngine;

public class SingleBarrel : TurretBase {

    public Transform shotSpawn;
    public GameObject shot;

    public override void SetUpAttributions() {
        return;
    }
    override public void ShotSpawn() {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        TurretBullet bullet = shot.GetComponent<TurretBullet>();
		bullet.setTarget(currentTarget);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
