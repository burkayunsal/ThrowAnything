using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianEnemy : Enemy
{
    public override void InitEnemies()
    {
        HP = 100;
       
    }

    public override void DieMF()
    {
        base.DieMF();
    }
}
