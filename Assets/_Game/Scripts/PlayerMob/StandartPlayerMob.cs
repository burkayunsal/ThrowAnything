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
        InitPlayer();
    }

    public override void ThrowAnything()
    {
        Anything a = PoolManager.I.GetObject<Anything>();
        a.damaage = Damage();
        a.transform.position = HandPoint.position;
        a.Throw(CalculateThrowForce());
    }

    public override float ShootInterval()
    {
        return Configs.Player.StandartMobSettings.shootInterval + Configs.UpgradePlayer.shootIntervalChange[SaveLoadManager.GetAttackSpeedLevel()];
    }

    public override float Damage()
    {
        return Configs.Player.StandartMobSettings.damage + Configs.UpgradePlayer.damageChange[SaveLoadManager.GetDamageLevel()];
         
    }

    public void InitPlayer()
    {
        HP = Configs.Player.StandartMobSettings.maxHP + Configs.UpgradePlayer.maxHPChange[SaveLoadManager.GetHPLevel()];
        DetectorRange = Configs.Player.StandartMobSettings.detectorRange + Configs.UpgradePlayer.detectorRangeChange[SaveLoadManager.GetRangeLevel()];
    }
}
