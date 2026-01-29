using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LastPositionScript : MonoBehaviour
{
    public Vector3 lastPostion;
    private void Awake()
    {
        lastPostion = transform.position;
    }
    private void FixedUpdate()
    {
        lastPostion = transform.position;
    }
}