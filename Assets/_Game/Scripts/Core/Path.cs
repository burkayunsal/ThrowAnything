
using FluffyUnderware.Curvy;
using UnityEngine;
using SBF.Extentions.Transforms;
using SBF.Extentions.Vector;
using UnityEngine.Serialization;
using MyBox;

public class Path : MonoBehaviour
{
    [SerializeField] CurvySpline road;
    public CurvySpline GetRoad() => road;
    
    [Range(0,1f)]
    [SerializeField] float safeZoneAreaPer = .3f;

    [Range(2,20)][SerializeField] public float spawnEnemyInterval;

    public Transform safeZoneEnterPoint, safeZoneExitPoint, respawnPoint;

    public bool useRecruitZone;
    [ConditionalField(nameof(useRecruitZone),false)]
    [Range(0,1f)]
    public float recruitZoneSplinePos;
    
    public bool useUpgradeZone;
    [ConditionalField(nameof(useUpgradeZone),false)]
    [Range(0,1f)]
    public float upgradeZoneSplinePos;

    public bool useRegenerationZone;
    [ConditionalField(nameof(useRegenerationZone),false)]
    [Range(0,1f)]
    public float regenerationZoneSplinePos;
    
    private int loopCount;
    
    private void Start()
    {
        loopCount = 1;
        spawnEnemyInterval = 20f;
        respawnPoint.position = road.Interpolate(Configs.PathConfigs.respawnPoint).WithY(3f);
        EnemySpawner.I.CreateSpawnPool(spawnRules);
        SetSafeZone();
    }

    private float startFrom, endAt;
    
     void SetEnemyPositions()
    {
        float interval = (endAt - startFrom) / spawnEnemyInterval;
        int enemyCount = Mathf.CeilToInt(interval);

        for (int i = 0; i < enemyCount; i++)
        {
            EnemyBase enemyBase = null;
            enemyBase = EnemySpawner.I.SpawnEnemies();

            enemyBase.transform.position = (road.InterpolateByDistance(startFrom + (i * spawnEnemyInterval)).GetPointAround(Vector3.up, Random.Range(0f, 360f), Random.Range(5f, 10f), false)).WithY(0) ;
        }

    }
    void SetSafeZone()
    {
        float tenPercentage = road.Length / 10;
        float startSpawnFrom = safeZoneAreaPer * tenPercentage * 10f;
        float endSpawnAt = (1f - (safeZoneAreaPer / 2f)) * tenPercentage * 10f;

        safeZoneEnterPoint.position = road.Interpolate((1-(safeZoneAreaPer/2f))).WithY(3);
        safeZoneExitPoint.position = road.Interpolate((safeZoneAreaPer/2f)).WithY(3);

        float angle = CalculateAngle();

        safeZoneEnterPoint.rotation = Quaternion.Euler(Vector3.up * angle);
        safeZoneExitPoint.rotation = Quaternion.Euler(Vector3.up * angle);

        PlaceGround(angle);
        
        startFrom = startSpawnFrom;
        endAt = endSpawnAt;
        SetEnemyPositions();
    }

    void PlaceGround(float angle)
    {
        EnvironmentHandler.I.ground.transform.position = (safeZoneEnterPoint.position + safeZoneExitPoint.position).WithY(-1f) / 2f;
        EnvironmentHandler.I.ground.transform.rotation = Quaternion.Euler(Vector3.up * angle);
    }
    

    float CalculateAngle()
    {
        float angle = 0;

        Vector3 dif = safeZoneEnterPoint.position - safeZoneExitPoint.position;

        angle = -Mathf.Atan2(dif.z, dif.x);

        angle = (Mathf.Rad2Deg * angle);

        return angle;
    }
    
    public int GetLoopCount() => loopCount;

    public void PlayerInSafeZone(bool isIn)
    {
        PlayerController.I.isInSafeZone = isIn;
        if (isIn)
        {
            EnemySpawner.I.StopAllEnemies();
            PlayerSpawner.I.StopShooting();
        }
    }
    
    public void PlayerAtRespawnPoint()
    {
        EnemySpawner.I.ClearAllEnemies();
        
        if (loopCount <= 6)
        {
            loopCount++;
            spawnEnemyInterval -= Configs.PathConfigs.spawnEnemyIntervalDecrease;
        }
        
        SetEnemyPositions();
    }

    public SpawnRule spawnRules;
    [System.Serializable]
    public class SpawnRule
    {
        public SingleRule[] rules;
    }

    [System.Serializable]
    public class SingleRule
    {
        public EnemyTypes enemyType;
        public int amount;
    }

}

