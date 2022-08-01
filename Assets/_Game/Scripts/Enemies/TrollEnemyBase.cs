
public class TrollEnemyBase : EnemyBase
{
    public override void InitEnemies()
    {
        hasInitialized = true;
        initialColor = GetRenderer().material.color;
        HP = Configs.Enemy.TrollEnemySettings.maxHP;
        DetectorRange = Configs.Enemy.TrollEnemySettings.range;
        Damage =Configs.Enemy.TrollEnemySettings.damage;
        AttackSpeed = Configs.Enemy.TrollEnemySettings.attackSpeed;
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
        HP = Configs.Enemy.TrollEnemySettings.maxHP;
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }
}

