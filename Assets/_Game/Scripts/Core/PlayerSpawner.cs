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

    List<PlayerMobBase> lsAllPlayers = new List<PlayerMobBase>();
    
    public void SpawnPlayerMob(PlayerTypes pt)
    {
        PlayerMobBase pmb = Instantiate(playerPrefabs[(int)pt]);
        lsAllPlayers.Add(pmb);
    }

    public void StopShooting()
    {
        foreach (PlayerMobBase pmb in lsAllPlayers)
        {
            pmb.StopShooting();
        }
    }
}
