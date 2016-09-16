using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface Killer {

    int getFireInterval();

    void attack(Dictionary<string, Victim> victims);

    void shotSpawn();

}