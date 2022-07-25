using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SBF.Extentions.Transforms;
using FluffyUnderware.Curvy.Controllers;

public class PlayerController : Singleton<PlayerController>
{
    List<PlayerMobBase> lsMobs = new List<PlayerMobBase>();

    public enum AnimStates
    {
        Idle,
        Run
    }

    AnimStates _animState = AnimStates.Idle;
    AnimStates AnimationState
    {
        get => _animState;
        set
        {
            if(_animState != value)
            {
                _animState = value;
                onAnimChanged(_animState);
            }
        }
    }

    float Speed => Configs.Player.speed;

    [SerializeField] SplineController spline;

    public delegate void OnAnimationChanged(AnimStates _as);
    OnAnimationChanged onAnimChanged;

    public void OnGameStarted()
    {

        //Vector3 v = spline.Spline.InterpolateByDistance(15f);
        //go.transform.position = v;
    }

    private void Start()
    {
        SetRoad(spline);
        
        PlayerSpawner.I.SpawnPlayerMob(PlayerTypes.Standart);
        PlayerSpawner.I.SpawnPlayerMob(PlayerTypes.BigBoi);
       
    }

    void SetRoad(SplineController newRoad)
    {
        spline = newRoad;
        spline.AbsolutePosition = 5f;

        CurvySplineMoveEvent curvySplineMoveEvent = new CurvySplineMoveEvent();
        curvySplineMoveEvent.AddListener(OnRoadCompleted);

        spline.OnEndReached = curvySplineMoveEvent;

        //Vector3 v = spline.Spline.InterpolateByDistance(5f);
        //go.transform.position = v;

    }

    public void StartMove()
    {
        spline.Speed = Speed;
        AnimationState = AnimStates.Run;
    }

    public void StopMove()
    {
        spline.Speed = 0f;
        AnimationState = AnimStates.Idle;
    }

    public void AddNewMob(PlayerMobBase pmb)
    {
        if (lsMobs.Contains(pmb)) return;

        lsMobs.Add(pmb);
        pmb.transform.SetParent(spline.transform);
        pmb.transform.localRotation = Quaternion.identity;
        onAnimChanged += pmb.ChangeAnimationState;

        RepositionMobs();
    }

    public void RemovePlayer(PlayerMobBase pmb)
    {
        if (!lsMobs.Contains(pmb)) return;

        lsMobs.Remove(pmb);
        onAnimChanged -= pmb.ChangeAnimationState;
        pmb.transform.SetParent(null);

        RepositionMobs();
    }

    void RepositionMobs()
    {
        if(lsMobs.Count == 1)
        {
            lsMobs[0].transform.localPosition = Vector3.zero; 
        }
        else
        {
            float angle = 360f / lsMobs.Count;
            for (int i = 0; i < lsMobs.Count; i++)
            {
                lsMobs[i].transform.position =
                    spline.transform.GetPointAround(Vector3.up, angle * i, Configs.Player.distanceBetweenMobs);
            }
        }
    }

    float _spawnPoint;
    public float SpawnPoint
    {
        get => _spawnPoint;
        set
        {
            _spawnPoint = spline.Position;
        }
    }

    public void AddEnemy(Enemy enemy, float position)
    {
        
    }

    void OnRoadCompleted(CurvySplineMoveEventArgs args)
    {
        Debug.Log("Road Completed");
    }
    
}
