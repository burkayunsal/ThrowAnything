
using UnityEngine;



public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] string[] enemies;
    public bool canFollowPlayer = false;

    public Enemy SpawnEnemies (EnemyTypes enemyType)
    {
        Enemy enemy = null;

        switch (enemyType)
        {
            case EnemyTypes.Barbarian:
                enemy = PoolManager.I.GetObject<BarbarianEnemy>();
                break;

            case EnemyTypes.Troll:
                enemy = PoolManager.I.GetObject<TrollEnemy>();
                break;
        }

        return enemy;

    }
}

public enum EnemyTypes
{
    Barbarian = 0,
    Troll = 1
}
