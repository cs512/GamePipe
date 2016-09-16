using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface Attacker {

    Guid[] attack(Dictionary<String, Defender> attackables);

}
