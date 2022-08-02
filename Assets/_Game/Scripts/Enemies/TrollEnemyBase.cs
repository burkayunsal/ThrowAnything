
public class TrollEnemyBase : EnemyBase
{
    public override void InitEnemies()
    {
        base.InitEnemies();
        hasInitialized = true;
        initialColor = GetRenderer().material.color;
        HP = Configs.Enemy.TrollEnemySettings.maxHP;
        DetectorRange = Configs.Enemy.TrollEnemySettings.range;
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
    
    public override float SetShootInterval()
    {
        return Configs.Enemy.TrollEnemySettings.shootInterval;
    }
    public override float SetDamage()
    {
        return Configs.Enemy.TrollEnemySettings.damage;
         
    }

    public override void Attack()
    {
        GetAnimator().SetTrigger("Attack");
    }
    
    
}

