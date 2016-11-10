using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class ShieldGenerator : TurretBase {

    public GameObject Shield;
    public float totalShield;
    private float currentShield;
    //Recharge shield in rechargeTime seconds
    public float rechargeTime;
    public GameObject shockHit;
    private bool SetShieldValue = false;

    IEnumerator WaitForRecharge() {
        print(Time.time);
        yield return new WaitForSeconds(rechargeTime);
        ShieldRecharge();
        print(Time.time);
    }

    public float GetShieldRemnant()
    {
        return currentShield;
    }

    private void ShieldRecharge()
    {
        GameObject prefab = Resources.Load("Prefabs/Turrets/Shield", typeof(GameObject)) as GameObject;
        Shield = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        Shield.transform.SetParent(this.transform);
        Shield.transform.localScale = prefab.transform.localScale;
        currentShield = totalShield;
    }

    public override void SetUpAttributions() {
        currentShield = totalShield;
        return;
    }
    override public void ShotSpawn() {
    }
    override public void SetShootEnemy(GameObject enemy) {
    }
    override public void DismissShootEnemy() {
    }

    override public void ReduceShield(float damage, Vector3 hittingPoint)
    {
        if (!SetShieldValue) {
            currentShield = totalShield;
            SetShieldValue = true;
        }
        GameObject shockWave = Instantiate(shockHit, hittingPoint, transform.rotation) as GameObject;
        AudioSource audio = GameObject.Find("BulletDestorySound").GetComponent<AudioSource>();
        audio.Play();
        currentShield -= damage;
        if (currentShield > 0) {
            Shield.GetComponent<Renderer>().material.color = new Color(1, 1, 1, (float)System.Math.Round(currentShield / totalShield, 2));
        } else {
            Destroy(Shield);
            StartCoroutine(WaitForRecharge());
        }
    }



}
