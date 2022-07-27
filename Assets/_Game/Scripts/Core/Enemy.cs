using System.Collections;
using System.Collections.Generic;
using SBF.Extentions.Transforms;
using SBF.Extentions.Vector;
using UnityEngine;

public abstract class Enemy : PoolObject
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] Renderer rd;

    [SerializeField] float Range;
    [SerializeField] float Damage;
    [SerializeField] float AttackSpeed;

    float Speed => Configs.Enemy.speed;

    bool canFollow = false;

    Vector3 MovementDirection;

    enum AnimationStates
    {
        Idle,
        Run
    }
    AnimationStates _animState = AnimationStates.Idle;
    AnimationStates AnimState
    {
        get => _animState;
        set
        {
            if (_animState != value)
            {
                _animState = value;
                anim.SetTrigger(_animState.ToString());
            }
        }
    }

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

    public bool isAlive => HP > 0;


    private void Start()
    {
        InitEnemies();

    }

    public virtual void DieMF()
    {
        transform.SetParent(null);
        canFollow = false;
        //Material gri yap
        rd.material.color = Color.gray;
        AnimState = AnimationStates.Idle;
        //OnDeactivate();
    }

    public void OnSelectedAsTarget()
    {
        //rd.material.color = Color.green;
    }

    public abstract void InitEnemies();

    private void Update()
    {
        if (GameManager.isRunning && EnemySpawner.I.canFollow && canFollow)
        {
            rb.transform.position += rb.transform.forward * (Time.deltaTime * Speed);
            rb.transform.SlowLookAt(PlayerController.I.GetPlayerTransform(), 8f);

        }
    }

 

    List<PlayerMobBase> lsPlayersInTriggerZone = new List<PlayerMobBase>();

    public virtual void OnPlayerTriggerEnter(PlayerMobBase pmb)
    {
        if (!lsPlayersInTriggerZone.Contains(pmb))
            lsPlayersInTriggerZone.Add(pmb);

        if(lsPlayersInTriggerZone.Count >= 1)
        {
            canFollow = true;
            AnimState = AnimationStates.Run;
        }
    }

    public virtual void OnPlayerTriggerExit(PlayerMobBase pmb)
    {
        if (lsPlayersInTriggerZone.Contains(pmb))
            lsPlayersInTriggerZone.Remove(pmb);

        if (lsPlayersInTriggerZone.Count == 0)
        {
            canFollow = false;
            AnimState = AnimationStates.Idle;
        }
    }
}
