
public class BarbarianEnemyBase : EnemyBase
{
    public override void InitEnemies()
    {
        
        initialColor = GetRenderer().material.color;
        HP = Configs.Enemy.BarbarianEnemySettings.maxHP;
        DetectorRange = Configs.Enemy.BarbarianEnemySettings.range;
        Damage =Configs.Enemy.BarbarianEnemySettings.damage;
        AttackSpeed = Configs.Enemy.BarbarianEnemySettings.attackSpeed;
    }
    
    public override void DieMF()
    {
        base.DieMF();
    }

    private bool hasInitialized = false;
    public override void OnDeactivate()
    {
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        InitEnemies();
        gameObject.SetActive(true);
        if (hasInitialized)
            ResetMe();
        else
            hasInitialized = true;
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
