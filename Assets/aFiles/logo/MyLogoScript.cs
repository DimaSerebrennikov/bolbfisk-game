using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyLogoScript : MonoBehaviour
{

    public Sprite[] sprites;
    float timeBetweenFrames;
    float targetTimeBetweenFrames;
    int currentFrame;
    public Image imageLogo;
    static bool showLogo = true;
    public Image darker;
    bool darkerOn;
    bool darkerOff;
    bool logoOn;
    public Action onDestroy;
    bool nextFrame;

    private void Awake()
    {
        nextFrame = false;
        darkerOn = false;
        logoOn = false;
        darkerOff = true;
        currentFrame = 0;
        targetTimeBetweenFrames = 0.016f;
        timeBetweenFrames = 0f;

        //=========================================================== ???????? ???? ?????? ??? ?????? ???????
        if (!showLogo)
        {
            Destroy(gameObject);
        }
        else
        {
            Time.timeScale = 0f;
            showLogo = false;
        }
        //=========================================================== ???????? ???? ?????? ??? ?????? ???????
    }
    private void Update()
    {

        timeBetweenFrames += Time.unscaledDeltaTime;
        while (timeBetweenFrames >= targetTimeBetweenFrames)
        {
            nextFrame = true;
            timeBetweenFrames -= targetTimeBetweenFrames;
        }
        //=========================================================== ????????? ?????????
        if (darkerOff)
        {
            if (nextFrame)
            {
                nextFrame = false;
                float nextColor = darker.color.a - 0.016f;
                darker.color = new Color(0, 0, 0, nextColor);
                if (nextColor <= 0)
                {
                    darkerOff = false;
                    logoOn = true;
                }
            }
        }
        //=========================================================== ????????? ?????????
        //=========================================================== ????? ????
        if (logoOn)
        {
            if (nextFrame
            && currentFrame < sprites.Length)
            {
                nextFrame = false;
                imageLogo.sprite = sprites[currentFrame];
                currentFrame++;
            }
            if (currentFrame >= sprites.Length)
            {
                logoOn = false;
                darkerOn = true;
            }
        }
        //=========================================================== ????? ????
        //=========================================================== ??????????
        if (darkerOn)
        {
            if (nextFrame)
            {
                nextFrame = false;
                float nextColor = darker.color.a + 0.016f;
                darker.color = new Color(0, 0, 0, nextColor);
                if (nextColor >= 1)
                {
                    FinalClosing();
                }
            }
        }
        //=========================================================== ??????????
    }
    void FinalClosing()
    {
        Time.timeScale = 1f;
        onDestroy?.Invoke();
        Destroy(gameObject);
    }
}