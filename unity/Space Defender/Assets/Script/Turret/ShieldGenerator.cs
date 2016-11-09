using UnityEngine;
using System.Collections.Generic;
public class ShieldGenerator : TurretBase {

    public GameObject Shield;
    public float totalShield;
    public float currentShield;
    public float rechargeRate;

    public float GetShieldRemnant()
    {
        return totalShield;
    }

    private void ShieldRecharge()
    {
        if (currentShield == 0)
        {

        }
    }

    public override void SetUpAttributions() {
        return;
    }
    override public void ShotSpawn() {
    }
    override public void SetShootEnemy(GameObject enemy) {
    }
    override public void DismissShootEnemy() {
    }

    override public void ReduceShield(float damage)
    {
        currentShield -= damage;
    }

    void Start ()
    {
        currentShield = totalShield;
    }
    void Update()
    {
        Shield.GetComponent<Renderer>().material.color = new Color(1, 1, 1, (float)System.Math.Round(currentShield / totalShield, 2)); 
    }
}
