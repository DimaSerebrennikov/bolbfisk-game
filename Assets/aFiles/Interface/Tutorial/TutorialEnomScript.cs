using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialEnomScript : MonoBehaviour
{
    public GameObject FirstTutorial;
    public GameObject SecondTutorial;
    public GameObject ThirdTutorial;

    public Sprite FirstTutorialSprite;
    public Sprite SecondTutorialSprite;
    public Sprite ThirdTutorialSprite;
    float timer;
    int sequenceTutorial;

    public delegate void DelegateGameObject(GameObject obj);
    DelegateGameObject NowShowingTutorial;
    private void Start()
    {
        sequenceTutorial = 1;
        timer = 0f;
        NowShowingTutorial = delegate { };
        NowShowingTutorial += CheckSequence;

        FirstTutorial.GetComponent<Image>().sprite = FirstTutorialSprite;
        SecondTutorial.GetComponent<Image>().sprite = SecondTutorialSprite;
        ThirdTutorial.GetComponent<Image>().sprite = ThirdTutorialSprite;
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
            NowShowingTutorial += ThirdTutorialShow;
        }
    }
    void FirstTutorialShow(GameObject tutorial)
    {
        if (timer > 0.5f)
        {
            timer = 0f;
            NowShowingTutorial -= FirstTutorialShow;
            NowShowingTutorial += EnablingTutorial;
        }
    }
    void SecondTutorialShow(GameObject tutorial)
    {
        timer = 0f;
        NowShowingTutorial -= SecondTutorialShow;
        NowShowingTutorial += EnablingTutorial;
    }
    void ThirdTutorialShow(GameObject tutorial)
    {
        timer = 0f;
        NowShowingTutorial -= ThirdTutorialShow;
        NowShowingTutorial += EnablingTutorial;
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
        if (timer > 4f)
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
        tutorial.GetComponent<Image>().color = new Color(nowColor.r, nowColor.g, nowColor.b, colorChange);
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
            NowShowingTutorial += CheckSequence;
        }
        else if (sequenceTutorial == 2)
        {
            sequenceTutorial = 3;
            NowShowingTutorial += CheckSequence;
        }
        else if (sequenceTutorial == 3)
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
        else if (sequenceTutorial == 3)
        {
            NowShowingTutorial(ThirdTutorial);
        }
    }
}