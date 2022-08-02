using System;
using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using SBF.Extentions.Transforms;
using SBF.Extentions.Vector;
using Unity.Mathematics;
using UnityEngine;

public abstract class EnemyBase : PoolObject
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] Renderer rd;

    public Coin coin;
    public int coinAmount;
    
    public Animator GetAnimator() => anim;
    
    public Renderer GetRenderer() => rd;
    [SerializeField] EnemyPlayerDetector detector;

    List<PlayerMobBase> lsPlayersInTriggerZone = new List<PlayerMobBase>();
    PlayerMobBase _targetPlayerMobBase;

    //public PlayerMobBase GetTargetPlayer() => _targetPlayerMobBase;
   // [HideInInspector]
    public Color initialColor;

    delegate void OnUpdate();
    OnUpdate onUpdate;
    
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
    
    private void ChangeColor(Color c)
    {
        rd.material.color = c;
    }
    
    public void SetInitialColor()
    {
        ChangeColor(initialColor);
    }

    public virtual void ResetMe()
    {
        SetInitialColor();
        anim.applyRootMotion = false;
        canFollow = false;
        AnimState = AnimationStates.Idle;
        onUpdate = FollowPlayer;
        //Ragdoll u kapat;
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
        if(!canFollow) return;
        
        transform.SetParent(null);
        canFollow = false;
        ChangeColor(Color.gray);
        anim.applyRootMotion = true;
        anim.SetTrigger("Death");
        GiveCoin();
       
       //Ragdoll a gir

    }

    public virtual void InitEnemies()
    {
        onUpdate = FollowPlayer;
    }

    bool IsAbleToFollow() => GameManager.isRunning && !PlayerController.I.isInSafeZone && canFollow && isAlive && _targetPlayerMobBase != null;
    
    private void Update()
    {
       onUpdate?.Invoke();
    }

    void FollowPlayer()
    {
        if (IsAbleToFollow())
        {
            rb.transform.position += rb.transform.forward * (Time.deltaTime * Speed);
            rb.transform.SlowLookAt(_targetPlayerMobBase.transform.position, 8f);

            float distance = Vector3.Distance(_targetPlayerMobBase.transform.position, rb.transform.position);
            
            if (distance <= 1f)
            {
                onUpdate = AttackPlayer;
                AnimState = AnimationStates.Idle;
            }
        }
    }

    void AttackPlayer()
    {
        HandleShootingTimer();
        
        float distance = Vector3.Distance(_targetPlayerMobBase.transform.position, rb.transform.position);
            
        if (distance > 1.1f)
        {
            onUpdate = FollowPlayer;
            AnimState = AnimationStates.Run;
        }
    }
    
    float shootTimer = 0f;

    void HandleShootingTimer()
    {
        shootTimer += Time.deltaTime;
        
        if(shootTimer >= SetShootInterval())
        {
            Attack();
            shootTimer = 0f;
        }
    }
    
    public virtual void OnPlayerEnterRange(PlayerMobBase pmb)
    {
        if (!lsPlayersInTriggerZone.Contains(pmb))
            lsPlayersInTriggerZone.Add(pmb);
        
        if(lsPlayersInTriggerZone.Count >= 1)
        {
           
            _targetPlayerMobBase = FindTargetPlayer();
            if (_targetPlayerMobBase != null)
            {
                canFollow = true;
                AnimState = AnimationStates.Run;
            }
            
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

    PlayerMobBase FindTargetPlayer()
    {
        PlayerMobBase pmb = null;
        
        List<int> lsUnavailables = new List<int>();
        
        for (int i = 0; i < lsPlayersInTriggerZone.Count; i++)
        {
            if(pmb == null)
            {
                if (lsPlayersInTriggerZone[i] != null)
                {
                    if (lsPlayersInTriggerZone[i].IsAlive())
                    {
                        pmb = lsPlayersInTriggerZone[i];
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
            if (i<lsPlayersInTriggerZone.Count)
            {
                lsPlayersInTriggerZone.RemoveAt(lsUnavailables[i]);
            }
        }
        
        return pmb;
    }

    void GiveCoin()
    {
        for (int i = 0; i < coinAmount * 10; i++)
        {
            var currentCoin = Instantiate(coin, transform.position,quaternion.identity);
            var pos = currentCoin.transform.position;
            pos.y = 1.5f;
            currentCoin.transform.position =pos;
            
            currentCoin.CoinMovement();
        }
    }
    
    public abstract void Attack();
    public abstract float SetShootInterval();
    public abstract float SetDamage();


}
