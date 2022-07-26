

public class TrollEnemy : Enemy
{
    public override void InitEnemies()
    {
        HP = 250;
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

