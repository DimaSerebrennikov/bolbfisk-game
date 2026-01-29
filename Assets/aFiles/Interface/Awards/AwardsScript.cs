using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class AwardsScript : MonoBehaviour
{
    public GameObject award1;
    public GameObject award2;
    public GameObject premium;
    public GameObject win;
    public Restart restart;
    public GameObject backAward;
    bool startTransition;
    public RestartSound restartSound;
    public GameObject nextTypeOfAward;
    public GameObject modeMenu;
    public string nextScene;
    public bool cancelNotRestart;
    bool willReturnToMenu;

    public GameObject[] lockies;
    private void Awake()
    {
        willReturnToMenu = false;
        nextScene = GetActiveSceneScript.currentScene;
        nextTypeOfAward = new GameObject();
        startTransition = false;
    }
    public void ShowAward()
    {
        cancelNotRestart = false;
        restart.NumberOfAnimation = 0;
        startTransition = true;
        restartSound.PlayRestartSound();
        Time.timeScale = 0f;
    }
    public void ShowPremiumForIap()
    {
        nextTypeOfAward = premium;
        cancelNotRestart = true;
        restart.NumberOfAnimation = 0;
        startTransition = true;
        restartSound.PlayRestartSound();
        Time.timeScale = 0f;
        willReturnToMenu = true;
    }
    public void ShowAwardWitchCancel()//только для win при нажатии кнопки
    {
        if (!lockies[0].activeSelf
            && !lockies[1].activeSelf
            && !lockies[2].activeSelf)
        {
            nextTypeOfAward = win;
            cancelNotRestart = true;
            restart.NumberOfAnimation = 0;
            startTransition = true;
            restartSound.PlayRestartSound();
            Time.timeScale = 0f;
        }
    }
    public void AnimationFinished()
    {
        if (!willReturnToMenu)
        {
            Time.timeScale = 1f;
            restart.OnDeathD = restart.ShowDeath;
            restart.OnDeathWithSelfRemove();
        }
        else
        {
            willReturnToMenu = false;
            backAward.SetActive(false);
            nextTypeOfAward.SetActive(false);
        }
    }
    private void Update()
    {
        if (startTransition)
        {
            restart.ShowOneFullAnimation(ref startTransition);
            if (restart.NumberOfAnimation > 21)
            {
                backAward.SetActive(true);
                nextTypeOfAward.SetActive(true);
            }
        }
    }
    public void CloseAnimation()
    {
        if (nextTypeOfAward == win)
        {
            if (!cancelNotRestart)
            {
                AnimationFinished();
            }
            else
            {
                backAward.SetActive(false);
                nextTypeOfAward.SetActive(false);
            }
        }
    }
}