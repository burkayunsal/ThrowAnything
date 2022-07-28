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
    
    void OnEnemyTriggerEnter(Collider col)
    {
        if (!parentBase.IsAlive()) return;

        EnemyBase e = col.gameObject.GetComponent<EnemyBase>();

        if (e == null) return;
        
        parentBase.OnEnemyEnterRange(e);
    }

    void OnEnemyTriggerExit(Collider col)
    {
        if (!parentBase.IsAlive()) return;

        EnemyBase e = col.gameObject.GetComponent<EnemyBase>();

        if (e == null) return;
        
        parentBase.OnEnemyExitRange(e);
    }
    
}


