using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTypes
{
    Standart = 0,
    BigBoi = 1
}

public class PlayerSpawner : Singleton<PlayerSpawner>
{

    [SerializeField] string[] playerNames;
    [SerializeField] PlayerMobBase[] playerPrefabs;

    public void SpawnPlayerMob(PlayerTypes pt)
    {
        PlayerMobBase pmb = Instantiate(playerPrefabs[(int)pt]);
    }

}
