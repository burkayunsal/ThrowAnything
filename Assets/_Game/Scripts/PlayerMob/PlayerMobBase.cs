using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMobBase : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] PlayerEnemyDetector detector;
    

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
    public abstract void OnStart();
    public abstract void Fire();
}
