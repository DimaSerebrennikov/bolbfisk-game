using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    float TimerStart;
    [SerializeField] Sprite[] TransitionScreenSprites;//60 (21 center)
    public int NumberOfAnimation = 0;
    public Image GameObjectSpriteRenderer;//image which player sees
    public static bool DieScreen;
    public static bool StartScreen = false;
    [SerializeField] RestartSound restartSound;
    public delegate void delegateGameObject();
    public delegateGameObject OnDeathD;
    public AwardsScript awardsScript;
    void Start()
    {
        OnDeathD = ShowDeath;
        DieScreen = false;
        TimerStart = 0f;
        GameObjectSpriteRenderer.enabled = false;
    }
    public void ShowDeath()
    {
        DieScreen = true;
        restartSound.PlayRestartSound();
    }
    public void OnDeathWithSelfRemove()
    {
        OnDeathD();
        OnDeathD = () => { };
    }
    void Update()
    {
        TimerStart += Time.deltaTime;
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - our predator fishes are over and timer is on
        if (DieScreen == false)
        {
            if (GetActiveSceneScript.currentScene == SS.BigFishMode)
            {

            }
            else
            {
                if (ObjectListScript.playerBrutalList.Count < 1
                    && ObjectListScript.playerPassiveList.Count < 1)
                {
                    OnDeathWithSelfRemove();
                }
            }
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - our predator fishes are over and timer is on
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - last part of frames after, availabe only after death or retry func
        if (StartScreen && TimerStart > 0.01666f)
        {
            if (NumberOfAnimation <= 21)
            {
                NumberOfAnimation = 22;
            }
            if (NumberOfAnimation <= 59)
            {
                GameObjectSpriteRenderer.enabled = true;
                GameObjectSpriteRenderer.sprite = TransitionScreenSprites[NumberOfAnimation];
                NumberOfAnimation++;
                TimerStart = 0f;
            }
            else
            {
                GameObjectSpriteRenderer.enabled = false;
                StartScreen = false;
                NumberOfAnimation = 0;
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - last part of frames after, availabe only after death or retry func
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - in dying scene it shows first part of frames and call new scene
        if (DieScreen && TimerStart > 0.01666f)
        {
            if (NumberOfAnimation <= 21)
            {
                GameObjectSpriteRenderer.enabled = true;
                GameObjectSpriteRenderer.sprite = TransitionScreenSprites[NumberOfAnimation];
                NumberOfAnimation++;
                TimerStart = 0f;
            }
            else
            {
                StartScreen = true;
                SceneManager.LoadScene(awardsScript.nextScene);
            }

        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - in dying scene it shows first part of frames and call new scene
    }
    public void ShowOneFullAnimation(ref bool startTransitionP)
    {
        TimerStart += Time.unscaledDeltaTime;
        if (TimerStart > 0.016666f
            && NumberOfAnimation >= 0
            && NumberOfAnimation < TransitionScreenSprites.Length)
        {
            GameObjectSpriteRenderer.enabled = true;
            GameObjectSpriteRenderer.sprite = TransitionScreenSprites[NumberOfAnimation];
            NumberOfAnimation++;
            TimerStart = 0f;
        }
        if (NumberOfAnimation >= TransitionScreenSprites.Length)
        {
            startTransitionP = false;
            NumberOfAnimation = 0;
            GameObjectSpriteRenderer.enabled = false;
        }
    }
}
