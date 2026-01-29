using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class noDestroyForLogoScript : MonoBehaviour
{
    bool logoWasShow = true;
    void Awake()
    {
        Debug.Log(logoWasShow);
        logoWasShow = false;
    }
}