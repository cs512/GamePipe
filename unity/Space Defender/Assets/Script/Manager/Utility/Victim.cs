using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface Victim {

    float GetHealth();

    void DealDamage(float damage);

    void SlowDown(float precentage);

    int GetID();

    GameObject GetGameObject();
}
