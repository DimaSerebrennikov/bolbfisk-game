using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnomLipsScript : MonoBehaviour
{
    public static Action onEating;
    private void Awake()
    {
        onEating = () => { };
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHer")
        {
            ObjectListScript.DestroyWithCounter(collision.gameObject);
            EnomAnimationScript.Size++;
            onEating?.Invoke();
            EnomAnimationScript.nowEating = true;

        }
        else if (collision.gameObject.tag == "PlayerHer")
        {
            ObjectListScript.DestroyWithCounter(collision.gameObject);
            EnomAnimationScript.Size--;
            onEating?.Invoke();
            EnomAnimationScript.nowEating = true;
        }
    }
}
