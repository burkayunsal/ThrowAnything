
using FluffyUnderware.Curvy;
using UnityEngine;
using SBF.Extentions.Transforms;
using SBF.Extentions.Vector;

public class Path : MonoBehaviour
{
    [SerializeField] CurvySpline road;
    public CurvySpline GetRoad() => road;

    [Range(2,20)]
    [SerializeField] float spawnEnemyInterval;

    public Transform SafeZoneEnterPoint, SafeZoneExitPoint;

    private void Start()
    {
        SetSafeZone();
    }


    void SpawnEnemies(float startFrom, float endAt)
    {
        float interval = (endAt - startFrom) / spawnEnemyInterval;
        int enemyCount = Mathf.CeilToInt(interval);

        for (int i = 0; i < enemyCount; i++)
        {
       
            int rnd = Random.Range(0,10);

            Enemy enemy = null;

            if (rnd < 8)
            {
                enemy = EnemySpawner.I.SpawnEnemies(EnemyTypes.Barbarian);
             }
            if (rnd >= 8)
            {
                enemy = EnemySpawner.I.SpawnEnemies(EnemyTypes.Troll);
            }

            enemy.transform.position = (road.InterpolateByDistance(startFrom + (i * spawnEnemyInterval)).GetPointAround(Vector3.up, Random.Range(0f, 360f), Random.Range(5f, 10f), false)).WithY(0) ;

            
            
        }

    }
    void SetSafeZone()
    {
        float tenPercentage = road.Length / 10;
        float startSpawnFrom = 3f * tenPercentage;
        float endSpawnAt = 8.5f * tenPercentage;

        SafeZoneEnterPoint.position = road.Interpolate(0.85f).WithY(3);
        SafeZoneExitPoint.position = road.Interpolate(0.15f).WithY(3);

        float angle = CalculateAngle();

        SafeZoneEnterPoint.rotation = Quaternion.Euler(Vector3.up * angle);
        SafeZoneExitPoint.rotation = Quaternion.Euler(Vector3.up * angle);

        PlaceGround(angle);

        SpawnEnemies(startSpawnFrom, endSpawnAt);

    }

    void PlaceGround(float angle)
    {
        EnvironmentHandler.I.ground.transform.position = (SafeZoneEnterPoint.position + SafeZoneExitPoint.position).WithY(-1f) / 2f;
        EnvironmentHandler.I.ground.transform.rotation = Quaternion.Euler(Vector3.up * angle);
    }

    float CalculateAngle()
    {
        float angle = 0;

        Vector3 dif = SafeZoneEnterPoint.position - SafeZoneExitPoint.position;

        angle = -Mathf.Atan2(dif.z, dif.x);

        angle = (Mathf.Rad2Deg * angle);

        return angle;
    }
}

