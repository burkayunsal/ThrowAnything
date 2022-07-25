using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Barbarian = 0,
    Troll = 1,
    Archer = 2,
    Swordsman = 3
}


public class EnemySpawner : Singleton<EnemySpawner>
{

    [SerializeField] string[] enemies;
    [SerializeField] Enemy[] enemyPrefabs;


    public Enemy SpawnEnemies (EnemyTypes et)
    {
        return Instantiate(enemyPrefabs[(int)et]);

        PlayerController.I.
    }
}
