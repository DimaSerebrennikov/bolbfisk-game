using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ButtonAnimationScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    Vector3 scale;
    Vector3 scaleStep;
    Vector3 defaultScale;
    public UnityEvent methodEvent;
    bool pressDown;
    bool pressUp;
    int resultSteps;
    int frequency;
    float time;
    float targetTime;
    bool firstUpdate;

    private void Awake() //половина Awake написана в MyReset
    {
        firstUpdate = false;
        time = 0f;
        targetTime = 0.016f;
        frequency = 10;
        defaultScale = gameObject.transform.localScale;
        scale = gameObject.transform.localScale * 0.3f;
        scaleStep = scale / frequency;

    }
    public void OnEnable()
    {
        MyReset();
    }
    void MyReset()
    {
        pressDown = false;
        pressUp = false;
        gameObject.transform.transform.localScale = defaultScale;
        buttonPressed = false;
        resultSteps = 0;
    }
    public void Update()
    {
        if (firstUpdate)
        {
            //=========================================================== Адаптированное время между кадрами
            time += Time.unscaledDeltaTime;
            while(time > targetTime)
            {
                time -= targetTime;
                //=========================================================== Адаптированное время между кадрами
                PressDown();
                PressUp();
            }
        }
        else
        {
            firstUpdate = true;
        }
    }
    void PressDown()
    {
        if (pressDown)
        {
            if (resultSteps > -frequency)
            {

                gameObject.transform.localScale = gameObject.transform.localScale - scaleStep;
                resultSteps--;
            }
            else
            {
                methodEvent?.Invoke();
                pressDown = false;
            }

        }
    }
    void PressUp()
    {
        if (pressUp && !pressDown)
        {
            if (resultSteps < 0)
            {
                gameObject.transform.localScale = gameObject.transform.localScale + scaleStep;
                resultSteps++;
            }
            else
            {
                pressUp = false;
            }
        }
    }
    void StartPressDown()
    {
        pressDown = true;
    }
    void StartPressUp()
    {
        pressUp = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        StartPressDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StartPressUp();
    }
}