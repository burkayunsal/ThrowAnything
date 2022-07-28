using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoiPlayerMob : PlayerMobBase
{
    public override void Fire()
    {
        Enemy e = GetTargetEnemy();

        if(e != null)
        {
            GetAnimator().SetTrigger("Throw");
            e.HP -= Damage();
        }
        
    }

    public override void OnStart()
    {
        DetectorRange = Configs.Player.BigBoiMobSettings.detectorRange;
        HP = Configs.Player.BigBoiMobSettings.maxHP;
    }

    public override float ShootInterval()
    {
        return Configs.Player.BigBoiMobSettings.shootInterval;
    }

    public override float Damage()
    {
        return Configs.Player.BigBoiMobSettings.damage;
    }
}
