using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Naga: Enemy  {

    public override void SetUpDefaultAttributions() {
        this.SetSpeed(300f);
        this.SetDamage(1f);
        this.SetHealth(5f);
    }
}
