using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetActiveSceneScript : MonoBehaviour
{
    public static string currentScene;
    private void Awake()
    {
        if (Application.isEditor)
        {

        }
        else
        {
            Application.targetFrameRate = 65;
        }
        currentScene = SceneManager.GetActiveScene().name;
    }
}