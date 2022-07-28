using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Anything : PoolObject
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject[] anythings;
    public override void OnDeactivate()
    {
        gameObject.SetActive(false);
        rb.isKinematic = true;
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        rb.isKinematic = false;
        ShowRandomAnything();
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }

    public void Throw(Vector3 v)
    {
        rb.AddForce(v, ForceMode.Impulse);
    }

    private void ShowRandomAnything()
    {
        int rnd = Random.Range(0, anythings.Length);
        for (int i = 0; i < anythings.Length; i++)
        {
            anythings[i].SetActive(i == rnd);
        }
    }

    public float damaage;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyBase e = collision.collider.GetComponent<EnemyBase>();
            if (e)
            {
                e.HP -= damaage;
                OnDeactivate();
            }
        }
    }
}
