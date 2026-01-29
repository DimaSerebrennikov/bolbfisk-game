using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class backScript : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    private void Update()
    {
        if (Timer.TimerCanStart == true)
        {
            animator.enabled = true;
            Destroy(gameObject.GetComponent<backScript>());
        }
    }
}