using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialControllerScript : MonoBehaviour
{
    public Swipe swipeObject;

    public GameObject FirstTutorial;
    public GameObject SecondTutorial;
    public GameObject ThirdTutorial;

    public Sprite FirstTutorialSprite;
    public Sprite SecondModeFirstTutorial;
    public Sprite ThirdModeFirstTutorial;
    public Sprite SecondTutorialSprite;
    public Sprite SecondModeSecondTutorial;
    public Sprite ThirdModeSecondTutorial;
    public Sprite ThirdModeThirdTutorial;
    float timer;
    int sequenceTutorial;

    public delegate void DelegateGameObject(GameObject obj);
    DelegateGameObject NowShowingTutorial;
    private void Awake()
    {
        sequenceTutorial = 1;
        timer = 0f;
        NowShowingTutorial = delegate { };
        NowShowingTutorial += CheckSequence;
        if (GetActiveSceneScript.currentScene == SS.SwipeMode)
        {
            FirstTutorial.GetComponent<Image>().sprite = FirstTutorialSprite;
            SecondTutorial.GetComponent<Image>().sprite = SecondTutorialSprite;
        }
        else if (GetActiveSceneScript.currentScene == SS.StoneMode)
        {
            FirstTutorial.GetComponent<Image>().sprite = SecondModeFirstTutorial;
            SecondTutorial.GetComponent<Image>().sprite = SecondModeSecondTutorial;
        }
        else
        {
            FirstTutorial.GetComponent<Image>().sprite = ThirdModeFirstTutorial;
            SecondTutorial.GetComponent<Image>().sprite = ThirdModeSecondTutorial;
            ThirdTutorial.GetComponent<Image>().sprite = ThirdModeThirdTutorial;
        }
    }
    void CheckSequence(GameObject tutorial)
    {
        NowShowingTutorial -= CheckSequence;
        if (sequenceTutorial == 1)
        {
            NowShowingTutorial += FirstTutorialShow;
        }
        else if (sequenceTutorial == 2)
        {
            NowShowingTutorial += SecondTutorialShow;
        }
        else if (sequenceTutorial == 3)
        {
            //////////////////////NowShowingTutorial += ThirdTutorialShow;
        }
    }
    void FirstTutorialShow(GameObject tutorial)
    {
        if (!ArenaScript.firstArenaUp && timer > 1.5f)
        {
            timer = 0f;
            NowShowingTutorial -= FirstTutorialShow;
            NowShowingTutorial += EnablingTutorial;
        }
        else if (timer > 1.5f)
        {
            timer = 0f;
            sequenceTutorial = 2;
            NowShowingTutorial -= FirstTutorialShow;
            NowShowingTutorial += CheckSequence;
        }
    }
    void SecondTutorialShow(GameObject tutorial)
    {
        if (timer > 3f && !Timer.TimerCanStart)
        {
            timer = 0f;
            NowShowingTutorial -= SecondTutorialShow;
            NowShowingTutorial += EnablingTutorial;
        }
    }
    void ThirdTutorialShow(GameObject tutorial)
    {
        NowShowingTutorial -= ThirdTutorialShow;
        NowShowingTutorial += EnablingTutorial;
    }
    void EnablingTutorial(GameObject tutorial)
    {
        tutorial.SetActive(true);
        Color nowColor = tutorial.GetComponent<Image>().color;
        float colorChange = nowColor.a + Time.deltaTime;
        if (colorChange < 0.6)
        {
            tutorial.GetComponent<Image>().color = new Color(nowColor.r, nowColor.g, nowColor.b, colorChange);
        }
        if (timer > 3f)
        {
            timer = 0f;
            NowShowingTutorial -= EnablingTutorial;
            NowShowingTutorial += DisablingTutorial;
        }
    }
    void DisablingTutorial(GameObject tutorial)
    {
        Color nowColor = tutorial.GetComponent<Image>().color;
        float colorChange = nowColor.a - Time.deltaTime;
        if (colorChange > 0f)
        {
            tutorial.GetComponent<Image>().color = new Color(nowColor.r, nowColor.g, nowColor.b, colorChange);
        }
        if (timer > 3f)
        {
            timer = 0f;
            NowShowingTutorial -= DisablingTutorial;
            NowShowingTutorial += TutorialHide;
        }
    }
    void TutorialHide(GameObject tutorial)
    {
        tutorial.SetActive(false);
        NowShowingTutorial -= TutorialHide;
        if (sequenceTutorial == 1)
        {
            sequenceTutorial = 2;
            NowShowingTutorial += CheckSequence;
        }
        else if (sequenceTutorial == 2)
        {
            sequenceTutorial = 3;
            NowShowingTutorial += CheckSequence;
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (sequenceTutorial == 1)
        {
            NowShowingTutorial(FirstTutorial);
        }
        else if (sequenceTutorial == 2)
        {
            NowShowingTutorial(SecondTutorial);
        }
        else if (sequenceTutorial == 3)
        {
            NowShowingTutorial(ThirdTutorial);
        }
    }
}