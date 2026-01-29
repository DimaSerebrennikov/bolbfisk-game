using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public void RemoveBubble()//calls when bubble animation is over
    {
        ObjectListScript.DestroyWithCounter(gameObject);
    }
     
}