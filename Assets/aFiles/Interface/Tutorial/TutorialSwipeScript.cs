using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialSwipeScript : MonoBehaviour
{
    public GameObject FirstTutorial;
    public GameObject SecondTutorial;

    public Sprite FirstTutorialSprite;
    public Sprite SecondTutorialSprite;
    float timer;
    int sequenceTutorial;
    bool anywayShowSecond;

    public delegate void DelegateGameObject(GameObject obj);
    DelegateGameObject NowShowingTutorial;
    private void Start()
    {
        sequenceTutorial = 1;
        timer = 0f;
        NowShowingTutorial = delegate { };
        NowShowingTutorial += CheckSequence;
        anywayShowSecond = false;

        FirstTutorial.GetComponent<Image>().sprite = FirstTutorialSprite;
        SecondTutorial.GetComponent<Image>().sprite = SecondTutorialSprite;
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
    }
    void FirstTutorialShow(GameObject tutorial)
    {
        if (!ArenaScript.firstArenaUp && timer > 2f)
        {
            timer = 0f;
            NowShowingTutorial -= FirstTutorialShow;
            NowShowingTutorial += EnablingTutorial;
        }
        else if (timer > 2f)
        {
            timer = 0f;
            sequenceTutorial = 2;
            NowShowingTutorial -= FirstTutorialShow;
            NowShowingTutorial += CheckSequence;
        }
    }
    void SecondTutorialShow(GameObject tutorial)
    {
        if ((timer > 2f && !Timer.TimerCanStart)
            || (timer > 1f && anywayShowSecond))
        {
            timer = 0f;
            NowShowingTutorial -= SecondTutorialShow;
            NowShowingTutorial += EnablingTutorial;
        }
    }
    void EnablingTutorial(GameObject tutorial)
    {
        tutorial.SetActive(true);
        Color nowColor = tutorial.GetComponent<Image>().color;
        float colorChange = nowColor.a + Time.deltaTime;
        if (colorChange < 0.7)
        {
            tutorial.GetComponent<Image>().color = new Color(nowColor.r, nowColor.g, nowColor.b, colorChange);
        }
        else
        {
            if (timer > 1.5f)
            {
                timer = 0f;
                NowShowingTutorial -= EnablingTutorial;
                NowShowingTutorial += DisablingTutorial;
            }
        }
    }
    void DisablingTutorial(GameObject tutorial)
    {
        Color nowColor = tutorial.GetComponent<Image>().color;
        float colorChange = nowColor.a - Time.deltaTime;
        if (timer > 1.5f)
        {
            tutorial.GetComponent<Image>().color = new Color(nowColor.r, nowColor.g, nowColor.b, colorChange);
        }
        if (colorChange < 0f)
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
            anywayShowSecond = true;
            NowShowingTutorial += CheckSequence;
        }
        else if (sequenceTutorial == 2)
        {
            Destroy(gameObject);
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
    }
}