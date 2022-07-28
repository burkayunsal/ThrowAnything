
using FluffyUnderware.Curvy;
using UnityEngine;
using SBF.Extentions.Transforms;
using SBF.Extentions.Vector;
using UnityEngine.Serialization;

public class Path : MonoBehaviour
{
    [SerializeField] CurvySpline road;
    public CurvySpline GetRoad() => road;

    [Range(2,20)][SerializeField] float spawnEnemyInterval;

    public Transform safeZoneEnterPoint, safeZoneExitPoint;

    private void Start()
    {
        EnemySpawner.I.CreateSpawnPool(spawnRules);
        SetSafeZone();
    }


    public void SetEnemyPositions(float startFrom, float endAt)
    {
        float interval = (endAt - startFrom) / spawnEnemyInterval;
        int enemyCount = Mathf.CeilToInt(interval);

        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemy = null;
            enemy = EnemySpawner.I.SpawnEnemies();

            enemy.transform.position = (road.InterpolateByDistance(startFrom + (i * spawnEnemyInterval)).GetPointAround(Vector3.up, Random.Range(0f, 360f), Random.Range(5f, 10f), false)).WithY(0) ;
            
        }

    }
    void SetSafeZone()
    {
        float tenPercentage = road.Length / 10;
        float startSpawnFrom = 3f * tenPercentage;
        float endSpawnAt = 8.5f * tenPercentage;

        safeZoneEnterPoint.position = road.Interpolate(0.85f).WithY(3);
        safeZoneExitPoint.position = road.Interpolate(0.15f).WithY(3);

        float angle = CalculateAngle();

        safeZoneEnterPoint.rotation = Quaternion.Euler(Vector3.up * angle);
        safeZoneExitPoint.rotation = Quaternion.Euler(Vector3.up * angle);

        PlaceGround(angle);

        SetEnemyPositions(startSpawnFrom, endSpawnAt);
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


    public void PlayerInSafeZone(bool isIn)
    {
        PlayerController.I.isInSafeZone = isIn;
        if (isIn)
        {
            EnemySpawner.I.StopAllEnemies();
            PlayerSpawner.I.StopShooting();
        }
     
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

