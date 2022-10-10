
public class BarbarianEnemyBase : EnemyBase
{
    public override void InitEnemies()
    {
        base.InitEnemies();
        hasInitialized = true;
        initialColor = GetRenderer().material.color;
        HP = Configs.Enemy.BarbarianEnemySettings.maxHP;
        DetectorRange = Configs.Enemy.BarbarianEnemySettings.range;
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
    
    public override float SetShootInterval()
    {
        return Configs.Enemy.BarbarianEnemySettings.shootInterval;
    }
    public override float Damage() 
    {
        return Configs.Enemy.BarbarianEnemySettings.damage;
         
    }

    public override void Attack()
    {
        PlayerMobBase pmb = GetTargetPlayer();
        
        if (pmb != null)
        {
            GetAnimator().SetTrigger("Attack");
            pmb.HP  -= Damage();
        }
        
    }
}
