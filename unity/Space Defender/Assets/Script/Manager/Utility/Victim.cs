using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface Victim {

    float GetHealth();

    void DealDamage(float damage);

    void SlowDown(float precentage);

    void ReduceShield(float damage, Vector3 hittingPoint);

    int GetID();

    GameObject GetGameObject();
}
