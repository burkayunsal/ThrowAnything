using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SBF.Extentions.Transforms;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy;

public class PlayerController : Singleton<PlayerController>
{
    List<PlayerMobBase> lsMobs = new List<PlayerMobBase>();

    public bool isInSafeZone = true;
    
    public enum AnimStates
    {
        Idle,
        Run
    }

    AnimStates _animState = AnimStates.Idle;
    AnimStates AnimState
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

    private Path crntRoad;
    public Path GetCurrenctRoad() => crntRoad;

    public delegate void OnAnimationChanged(AnimStates _as);
    OnAnimationChanged onAnimChanged;

    public Transform GetPlayerTransform() => spline.transform;

    public void OnGameStarted()
    {
        //Vector3 v = spline.Spline.InterpolateByDistance(15f);
        //go.transform.position = v;
    }

    private void Start()
    {
        PlayerSpawner.I.SpawnPlayerMob(PlayerTypes.BigBoi);
    }

    public void SetRoad(Path newRoad)
    {
        crntRoad = newRoad;
        spline.Spline = crntRoad.GetRoad();

        spline.AbsolutePosition = 5f;

        CurvySplineMoveEvent curvySplineMoveEvent = new CurvySplineMoveEvent();
        curvySplineMoveEvent.AddListener(OnRoadCompleted);

        spline.OnEndReached = curvySplineMoveEvent;

        CameraController.I.Init();
    }
 

    public void StartMove()
    {
        spline.Speed = Speed;
        AnimState = AnimStates.Run;
    }

    public void StopMove()
    {
        spline.Speed = 0f;
        AnimState = AnimStates.Idle;
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
    
    void OnRoadCompleted(CurvySplineMoveEventArgs args)
    {
        Debug.Log("Road Completed");
    }
    
}
