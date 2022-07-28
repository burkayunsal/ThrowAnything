
public class BarbarianEnemy : Enemy
{
    public override void InitEnemies()
    {
        HP = Configs.Enemy.BarbarianEnemySettings.maxHP;
        DetectorRange = Configs.Enemy.BarbarianEnemySettings.range;
        Damage =Configs.Enemy.BarbarianEnemySettings.damage;
        AttackSpeed = Configs.Enemy.BarbarianEnemySettings.attackSpeed;
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
