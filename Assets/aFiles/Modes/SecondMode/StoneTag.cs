using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTag : MonoBehaviour
{
    public TrailRenderer tail;
    private void Awake()
    {
        tail = GetComponent<TrailRenderer>();
    }
    private void OnDisable()
    {
        tail.Clear();
    }
}
