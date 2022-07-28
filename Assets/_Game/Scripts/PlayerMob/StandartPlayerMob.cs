public class StandartPlayerMob : PlayerMobBase
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
        DetectorRange = Configs.Player.StandartMobSettings.detectorRange;
        HP = Configs.Player.StandartMobSettings.maxHP;
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
