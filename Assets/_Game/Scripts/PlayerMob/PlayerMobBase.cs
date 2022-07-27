using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMobBase : MonoBehaviour, IShooter
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] PlayerEnemyDetector detector;

    public Animator GetAnimator() => anim;

    List<Enemy> lsEnemiesInZone = new List<Enemy>();

    Enemy targetEnemy;

    public Enemy GetTargetEnemy() => targetEnemy;

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

    public virtual void OnEnemyEnterRange(Enemy e)
    {
        if (!lsEnemiesInZone.Contains(e))
        {
            lsEnemiesInZone.Add(e);
        }

        if (lsEnemiesInZone.Count >= 1)
            StartShooting();
    }

    public virtual void OnEnemyExitRange(Enemy e)
    {
        if (lsEnemiesInZone.Contains(e))
        {
            if(e == targetEnemy)
            {
                FindNewEnemyAround();
            }

            lsEnemiesInZone.Remove(e);
        }

        if (lsEnemiesInZone.Count == 0)
            StopShooting();
    }

    Enemy FindTargetEnemy()
    {
        Enemy e = null;

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
            lsEnemiesInZone.RemoveAt(lsUnavailables[i]);
        }

        return e;
    }


    public virtual void StartShooting()
    {
        targetEnemy = FindTargetEnemy();

        if (targetEnemy == null) return;

        targetEnemy.OnSelectedAsTarget();
        anim.SetLayerWeight(1, 1f);
        isShooting = true;
    }

    public virtual void StopShooting()
    {
        anim.SetLayerWeight(1, 0f);
        isShooting = false;
        targetEnemy = null;
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

        if(shootTimer >= ShootInterval())
        {
            Fire();
            shootTimer = 0f;
        }
    }

    void CheckEnemyIsAvailable()
    {
    
        if(targetEnemy != null)
        {
            if (!targetEnemy.isAlive)
            {
                targetEnemy = null;
                FindNewEnemyAround();
            }
        }
       
    }

    void FindNewEnemyAround()
    {
        //StopShooting();
        targetEnemy = FindTargetEnemy();

        if (targetEnemy == null) StopShooting();
        else targetEnemy.OnSelectedAsTarget();

    }

    public float ShootInterval()
    {
        return 1.2f;
    }

    public float Damage()
    {
        return 50f;
    }
}
