using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Vector3 axis;
    [SerializeField] float speed;

    void Update()
    {
        transform.Rotate(axis, speed * Time.deltaTime);
    }
}
