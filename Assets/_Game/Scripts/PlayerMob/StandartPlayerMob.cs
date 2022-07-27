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

}
