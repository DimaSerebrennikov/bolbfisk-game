using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
    PolygonCollider2D edgeCollider;
    private void Awake()
    {  
        edgeCollider = GetComponent<PolygonCollider2D>();
        StartCoroutine(EnableEdgeCollider());
    }
    IEnumerator EnableEdgeCollider()//enable collider with delay after init
    {
        edgeCollider.enabled = false;
        yield return new WaitForSeconds(0.75f);
        edgeCollider.enabled = true;
    }
}
