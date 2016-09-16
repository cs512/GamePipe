using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface Victim {

    int GetHealth();

    void DealDamage(int damage);

    string GetInstanceID();
}
