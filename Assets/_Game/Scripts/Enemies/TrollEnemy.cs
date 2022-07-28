
public class TrollEnemy : Enemy
{
    public override void InitEnemies()
    {
        HP = Configs.Enemy.TrollEnemySettings.maxHP;
        DetectorRange = Configs.Enemy.TrollEnemySettings.range;
        Damage =Configs.Enemy.TrollEnemySettings.damage;
        AttackSpeed = Configs.Enemy.TrollEnemySettings.attackSpeed;
    }

    public override void DieMF()
    {
        base.DieMF();
    }

    public override void OnDeactivate()
    {
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }
}

