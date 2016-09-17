using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface Killer {

    int GetFireInterval();

    void Attack(Dictionary<string, Victim> victims);

    void ShotSpawn();

}