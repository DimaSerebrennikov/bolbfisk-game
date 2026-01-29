using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;

public class EnablerUnityServices : MonoBehaviour
{
    void Awake()
    {
        UnityServices.InitializeAsync();
    }
}
