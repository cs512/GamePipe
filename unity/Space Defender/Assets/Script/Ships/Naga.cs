using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Naga: Enemy  {

    public override void SetUpDefaultAttributions() {
        this.SetSpeed(10f);
        this.SetDamage(1f);
    }

}
