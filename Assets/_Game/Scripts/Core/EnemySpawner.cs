
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : Singleton<EnemySpawner>
{
    List<EnemyBase> enemies = new List<EnemyBase>();

    public EnemyBase SpawnEnemies ()
    {
        EnemyBase enemyBase = null;

        EnemyTypes enemyType = GetRandomEnemyType();
        
        switch (enemyType)
        {
            case EnemyTypes.Barbarian:
                enemyBase = PoolManager.I.GetObject<BarbarianEnemyBase>();
                enemies.Add(enemyBase);
                break;

            case EnemyTypes.Troll:
                enemyBase = PoolManager.I.GetObject<TrollEnemyBase>();
                enemies.Add(enemyBase);
                break;
        }
        return enemyBase;
    }

    public void StopAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.StopFollow();
        }
    }

    public void ClearAllEnemies()
    {
            foreach (var enemy in enemies)
            {
                enemy.OnDeactivate();
            }
            enemies.Clear();
    }
    
    

    EnemyTypes GetRandomEnemyType()
    {
        return  lsSpawnChances[Random.Range(0, lsSpawnChances.Count)];
    }

    private List<EnemyTypes> lsSpawnChances = new List<EnemyTypes>();
    public void CreateSpawnPool(Path.SpawnRule sr)
    {
        for (int i = 0; i < sr.rules.Length; i++)
        {
            for (int j = 0; j < sr.rules[i].amount; j++)
            {
                lsSpawnChances.Add(sr.rules[i].enemyType);
            }
        }
    }
}


public enum EnemyTypes
{
    Barbarian = 0,
    Troll = 1
}
