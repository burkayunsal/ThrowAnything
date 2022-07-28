using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour
{
    [SerializeField] EnemyBase parent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerTriggerEnter(other);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerTriggerExit(other);
    } 
    
    void OnPlayerTriggerEnter(Collider other)
    {
        if (!parent.isAlive) return;

        PlayerMobBase pmb = other.gameObject.GetComponent<PlayerMobBase>();
        
        if (pmb == null) return;

        parent.OnPlayerEnterRange(pmb);
    }
    
    void OnPlayerTriggerExit(Collider other)
    {
        if (!parent.isAlive) return;

        PlayerMobBase pmb = other.gameObject.GetComponent<PlayerMobBase>();
        
        if (pmb == null) return;

        parent.OnPlayerExitRange(pmb);
    }
}
