using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoiPlayerMob : PlayerMobBase
{
    public override void Fire()
    {
        EnemyBase e = GetTargetEnemy();

        if(e != null)
        {
            GetAnimator().SetTrigger("Throw");
            //e.HP -= Damage();
        }
        
    }

    public override void OnStart()
    {
        DetectorRange = Configs.Player.BigBoiMobSettings.detectorRange;
        HP = Configs.Player.BigBoiMobSettings.maxHP;
    }

    public override void ThrowAnything()
    {
        Anything a = PoolManager.I.GetObject<Anything>();
        a.damaage = Configs.Player.BigBoiMobSettings.damage;
        a.transform.position = HandPoint.position;
        a.Throw(CalculateThrowForce());
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
