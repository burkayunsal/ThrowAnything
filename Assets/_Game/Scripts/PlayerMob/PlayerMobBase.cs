using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Packages.Rider.Editor.UnitTesting;
using SBF.Extentions.Transforms;
using SBF.Extentions.Vector;
using UnityEngine;

public abstract class PlayerMobBase : MonoBehaviour, IShooter
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] PlayerEnemyDetector detector;
    
    public Animator GetAnimator() => anim;

    List<EnemyBase> lsEnemiesInZone = new List<EnemyBase>();

    EnemyBase _targetEnemyBase;

    [SerializeField] public Transform HandPoint;
    public PlayerTypes pType;
    public EnemyBase GetTargetEnemy() => _targetEnemyBase;

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
            _hp = Mathf.Clamp(value, 0, float.MaxValue);
            //BAR ?

            if(_hp <= 0f)
            {
                OnDeath();
            }
        }
    }

    public bool IsAlive() => HP > 0;

    bool isShooting;

    void Update()
    {
        if(GameManager.isRunning)
            OnUpdate();
    }

    private void Start()
    {
        AssignEvents();
        OnStart();
        shootTimer = ShootInterval();
    }

    void AssignEvents()
    {
        PlayerController.I.AddNewMob(this);
    }

    public void ChangeAnimationState(PlayerController.AnimStates _as)
    {
        if (!IsAlive()) return;

        anim.SetTrigger(_as.ToString());
    }

    public void IncreaseDetectorRange(float incAmount)
    {
        DetectorRange *= (1 + incAmount);
    }

    void OnDeath()
    {
        PlayerController.I.RemovePlayer(this);
        
    }

    public virtual void OnEnemyEnterRange(EnemyBase e)
    {
        if (!lsEnemiesInZone.Contains(e))
            lsEnemiesInZone.Add(e);

        if (lsEnemiesInZone.Count >= 1)
            StartShooting();
    }

    public virtual void OnEnemyExitRange(EnemyBase e)
    {
        if (lsEnemiesInZone.Contains(e))
        {
            if(e == _targetEnemyBase)
            {
                FindNewEnemyAround();
            }

            lsEnemiesInZone.Remove(e);
        }

        if (lsEnemiesInZone.Count == 0)
            StopShooting();
    }

    EnemyBase FindTargetEnemy()
    {
        EnemyBase e = null;

        List<int> lsUnavailables = new List<int>();

        for (int i = 0; i < lsEnemiesInZone.Count; i++)
        {
            if(e == null)
            {
                if (lsEnemiesInZone[i] != null)
                {
                    if (lsEnemiesInZone[i].isAlive)
                    {
                        e = lsEnemiesInZone[i];
                        break;
                    }
                    else
                        lsUnavailables.Add(i);
                }
                else
                    lsUnavailables.Add(i);
            }

        }

        for (int i = 0; i < lsUnavailables.Count; i++)
        {
            if (i<lsEnemiesInZone.Count)
            {
                lsEnemiesInZone.RemoveAt(lsUnavailables[i]);
            }
        }

        return e;
    }

    public virtual void StartShooting()
    {
        if (PlayerController.I.isInSafeZone) return;
        
        _targetEnemyBase = FindTargetEnemy();

        if (_targetEnemyBase == null) return;
        if (rotationTween != null)
            rotationTween.Kill();
        
        anim.SetLayerWeight(1, 1f);
        shootTimer = ShootInterval();
        isShooting = true;
    }

    Tween rotationTween = null;
    public virtual void StopShooting()
    {
        anim.SetLayerWeight(1, 0f);
        isShooting = false;
        _targetEnemyBase = null;
        if (rotationTween != null)
            rotationTween.Kill();
        rotationTween = transform.DOLocalRotate(Vector3.zero, .5f);
        //shootTimer = ShootInterval();
    }
    
    public abstract void OnStart();
    public abstract void Fire();

    public virtual void OnUpdate()
    {
        if (isShooting)
            HandleShootingTimer();
    }

    float shootTimer = 0f;

    void HandleShootingTimer()
    {
        shootTimer += Time.deltaTime;

        CheckEnemyIsAvailable();

        LookAtEnemy();
        
        if(shootTimer >= ShootInterval())
        {
            Fire();
            shootTimer = 0f;
        }
    }

    void CheckEnemyIsAvailable()
    {
        if(_targetEnemyBase != null)
        {
            if (!_targetEnemyBase.isAlive)
            {
                _targetEnemyBase = null;
                FindNewEnemyAround();
            }
        }
    }

    void LookAtEnemy()
    {
        if(_targetEnemyBase == null || !_targetEnemyBase.isAlive) return;
       
        transform.SlowLookAt(_targetEnemyBase.transform.position.WithY(transform.position),8f);
    }

    void FindNewEnemyAround()
    {
        _targetEnemyBase = FindTargetEnemy();
        if (_targetEnemyBase == null) StopShooting();
    }

    public Vector3 CalculateThrowForce()
    {
        Vector3 forceVector;
        
        forceVector = _targetEnemyBase.transform.position;
        return forceVector;
    }

    public abstract void ThrowAnything();
    
    public abstract float ShootInterval();
    
    public abstract float Damage();
    public abstract void InitPlayer();


}
