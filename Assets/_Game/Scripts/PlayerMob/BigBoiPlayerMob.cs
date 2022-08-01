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
        InitPlayer();
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
        return Configs.Player.BigBoiMobSettings.shootInterval + Configs.UpgradePlayer.shootIntervalChange[SaveLoadManager.GetAttackSpeedLevel()];
    }

    public override float Damage()
    {
        return Configs.Player.BigBoiMobSettings.damage + Configs.UpgradePlayer.damageChange[SaveLoadManager.GetDamageLevel()];
    }
    
    public void InitPlayer()
    {
        DetectorRange = Configs.Player.BigBoiMobSettings.detectorRange + Configs.UpgradePlayer.detectorRangeChange[SaveLoadManager.GetRangeLevel()];
        HP = Configs.Player.BigBoiMobSettings.maxHP+ Configs.UpgradePlayer.maxHPChange[SaveLoadManager.GetHPLevel()];
    }
}
