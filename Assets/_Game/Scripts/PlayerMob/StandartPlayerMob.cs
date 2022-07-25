public class StandartPlayerMob : PlayerMobBase
{
    public override void Fire()
    {
    }

    public override void OnStart()
    {
        DetectorRange = Configs.Player.StandartMobSettings.detectorRange;
        HP = Configs.Player.StandartMobSettings.maxHP;
    }

}
