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
    [SerializeField] EnemyPlayerDetector detector;

    public float Damage;
    public float AttackSpeed;

    float Speed => Configs.Enemy.speed;

    bool canFollow = false;


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
    
    float _detectorRange;
    public float DetectorRange
    {
        get => _detectorRange;

        set
        {
            _detectorRange = value;
            detector.transform.localScale = Vector3.one * _detectorRange;
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

    public void StopFollow()
    {
        if (!isAlive)
            return;
       
        canFollow = false;
        AnimState = AnimationStates.Idle;
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
        if (GameManager.isRunning &&  !PlayerController.I.isInSafeZone && canFollow)
        {
            rb.transform.position += rb.transform.forward * (Time.deltaTime * Speed);
            rb.transform.SlowLookAt(PlayerController.I.GetPlayerTransform(), 8f);

        }
    }

    List<PlayerMobBase> lsPlayersInTriggerZone = new List<PlayerMobBase>();

    public virtual void OnPlayerEnterRange(PlayerMobBase pmb)
    {
        if (!lsPlayersInTriggerZone.Contains(pmb))
            lsPlayersInTriggerZone.Add(pmb);

        if(lsPlayersInTriggerZone.Count >= 1)
        {
            canFollow = true;
            AnimState = AnimationStates.Run;
        }
    }

    public virtual void OnPlayerExitRange(PlayerMobBase pmb)
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
