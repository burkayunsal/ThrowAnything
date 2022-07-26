using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : PoolObject
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;

    [SerializeField] float Range;
    [SerializeField] float Damage;
    [SerializeField] float AttackSpeed;

    float _hp;
    public float HP
    {
        get => _hp;
        set
        {
            float newVal = Mathf.Clamp(value, 0, float.MaxValue);
            _hp = newVal;

            if (_hp <= 0)
                DieMF();
        }
    }


    private void Start()
    {
        InitEnemies();
    }

    public virtual void DieMF()
    {
        transform.SetParent(null);
        //Material gri yap
        OnDeactivate();
    }

    public abstract void InitEnemies();
}
