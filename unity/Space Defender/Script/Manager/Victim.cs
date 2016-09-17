using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface Victim {

    int GetHealth();

	void DealDamage(float damage);

    string GetInstanceID();
}
