using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDetector : MonoBehaviour
{
    [SerializeField] PlayerMobBase parentBase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            OnEnemyTriggerEnter(other);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            OnEnemyTriggerExit(other);
    }

    void OnEnemyTriggerExit(Collider col)
    {
        if (!parentBase.IsAlive()) return;

        Enemy e = col.gameObject.GetComponent<Enemy>();

        if (e == null) return;

        e.OnPlayerTriggerExit(parentBase);

        parentBase.OnEnemyExitRange(e);
    }

    void OnEnemyTriggerEnter(Collider col)
    {
        if (!parentBase.IsAlive()) return;

        Enemy e = col.gameObject.GetComponent<Enemy>();

        if (e == null) return;

        e.OnPlayerTriggerEnter(parentBase);

        parentBase.OnEnemyEnterRange(e);
    }
}
