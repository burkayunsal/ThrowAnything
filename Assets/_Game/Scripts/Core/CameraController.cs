using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Transform target;

    [SerializeField] bool useEditorOffset;

    [ConditionalField(nameof(useEditorOffset),true)]
    [SerializeField] Vector3 offset;

    bool isInitialized = false;


    delegate void OnUpdate();
    OnUpdate onUpdate;

    void Start()
    {
        //Init();
    }

    public void Init()
    {
        if (target == null) return;

        if(useEditorOffset)
        {
            offset = target.position - transform.position;
        }

        onUpdate = FollowGameIn;
        isInitialized = true;
    }

    private void LateUpdate()
    {
        if(isInitialized)
        {
            onUpdate?.Invoke();
        }
    }

    void FollowGameIn()
    {
        transform.position = target.position - offset;
    }
}
