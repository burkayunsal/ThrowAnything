using UnityEngine;

public class StandartPlayerMob : PlayerMobBase
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
        DetectorRange = Configs.Player.StandartMobSettings.detectorRange;
        HP = Configs.Player.StandartMobSettings.maxHP;
    }

    public override void ThrowAnything()
    {
        Anything a = PoolManager.I.GetObject<Anything>();
        a.damaage = Configs.Player.StandartMobSettings.damage;
        a.Throw(CalculateThrowForce());
    }

    public override float ShootInterval()
    {
        return Configs.Player.StandartMobSettings.shootInterval;
    }

    public override float Damage()
    {
        return Configs.Player.StandartMobSettings.damage;
    }
}
