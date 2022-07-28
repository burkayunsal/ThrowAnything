
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : Singleton<EnemySpawner>
{
    List<Enemy> enemies = new List<Enemy>();
    public bool canFollow = false;

    private Path path = new Path();
    
    public Enemy SpawnEnemies ()
    {
        Enemy enemy = null;

        EnemyTypes enemyType = GetRandomEnemyType();
        
        switch (enemyType)
        {
            case EnemyTypes.Barbarian:
                enemy = PoolManager.I.GetObject<BarbarianEnemy>();
                enemies.Add(enemy);
                break;

            case EnemyTypes.Troll:
                enemy = PoolManager.I.GetObject<TrollEnemy>();
                enemies.Add(enemy);
                break;
        }
        return enemy;
    }

    public void StopAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.StopFollow();
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
