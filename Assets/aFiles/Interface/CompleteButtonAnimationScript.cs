using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompleteButtonAnimationScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool pressNowWork;
    bool pressAfterDown;
    public bool buttonPressed;
    Vector3 scale;
    Vector3 scaleStep;
    Vector3 defaultScale;
    public UnityEvent methodEvent;
    bool complete;
    Sprite native;
    public Sprite completeSprite;
    public Image imageGameObject;



    private void Awake() //половина Awake написана в MyReset
    {
        native = imageGameObject.sprite;
        defaultScale = gameObject.transform.localScale;
        scale = gameObject.transform.localScale * 0.3f;
        scaleStep = scale / 10;
    }
    public void OnEnable()
    {
        MyReset();
    }
    void MyReset()
    {
        gameObject.transform.transform.localScale = defaultScale;
        pressAfterDown = false;
        pressNowWork = false;
        buttonPressed = false;
        complete = false;
        imageGameObject.sprite = native;
    }
    void MakeSpriteComplete()
    {
        complete = true;
        imageGameObject.sprite = completeSprite;
    }
    IEnumerator PressDown()
    {
            for (int a = 0; a < 10; a++)
            {
                gameObject.transform.localScale = gameObject.transform.localScale - scaleStep;
                yield return null;
            }
            pressAfterDown = true;
            methodEvent?.Invoke();
    }
    IEnumerator PressUp()
    {
        for (int a = 0; a < 10; a++)
        {
            gameObject.transform.localScale = gameObject.transform.localScale + scaleStep;
            yield return null;
        }
        pressNowWork = false;
    }
    void StartPressDown()
    {
        if (!pressNowWork)
        {
            pressNowWork = true;
            StartCoroutine(PressDown());
        }
    }
    void StartPressUp()
    {
        if (pressAfterDown)
        {
            pressAfterDown = false;
            StartCoroutine(PressUp());
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!complete)
        {
            MakeSpriteComplete();
        }
        else
        {
            StartPressDown();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StartPressUp();
    }
}