
public class BarbarianEnemyBase : EnemyBase
{
    public override void InitEnemies()
    {
        hasInitialized = true;
        initialColor = GetRenderer().material.color;
        HP = Configs.Enemy.BarbarianEnemySettings.maxHP;
        DetectorRange = Configs.Enemy.BarbarianEnemySettings.range;
        Damage =Configs.Enemy.BarbarianEnemySettings.damage;
        AttackSpeed = Configs.Enemy.BarbarianEnemySettings.attackSpeed;
    }
    

    private bool hasInitialized = false;
    public override void OnDeactivate()
    {
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        if (hasInitialized)
            ResetMe();
        else
            InitEnemies();
    }

    public override void ResetMe()
    {
        base.ResetMe();
        HP = Configs.Enemy.BarbarianEnemySettings.maxHP;
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }
}
