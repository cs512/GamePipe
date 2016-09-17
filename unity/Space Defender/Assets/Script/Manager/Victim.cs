using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface Victim {

    float GetHealth();

    void DealDamage(float damage);

    int GetID();
}
