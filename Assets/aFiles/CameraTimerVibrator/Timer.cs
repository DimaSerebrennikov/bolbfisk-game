using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    //=========================================================== Редактор
    [SerializeField] GameObject Back;
    static SpriteRenderer BackSR;
    [SerializeField] GameObject UiBack;
    static Image UiBackImage;
    //=========================================================== Редактор
    //=========================================================== Поля таймера
    public static float MyTimer;
    static float DelayCheckRecord;
    public static bool TimerCanStart;
    //=========================================================== Поля таймера
    //=========================================================== Поля рекорда
    string record;
    string uiBackRed;
    string uiBackGreen;
    string uiBackBlue;
    //=========================================================== Поля рекорда
    //=========================================================== Безопасные строки
    const string RecordSwipe = "RecordSwipe";
    const string UiBackRedSwipe = "UiBackRedSwipe";
    const string UiBackGreenSwipe = "UiBackGreenSwipe";
    const string UiBackBlueSwipe = "UiBackBlueSwipe";

    const string RecordStone = "RecordStone";
    const string UiBackRedStone = "UiBackRedStone";
    const string UiBackGreenStone = "UiBackGreenStone";
    const string UiBackBlueStone = "UiBackBlueStone";

    const string RecordBigFish = "RecordBigFish";
    const string UiBackRedBigFish = "UiBackRedBigFish";
    const string UiBackGreenBigFish = "UiBackGreenBigFish";
    const string UiBackBlueBigFish = "UiBackBlueBigFish";
    //=========================================================== Безопасные строки
    //=========================================================== Награды
    public Restart restart;
    public AwardsScript awardsScript;
    //=========================================================== Награды
    void Start()
    {
        //=========================================================== Объявление
        TimerCanStart = false;
        UiBackImage = UiBack.GetComponent<Image>();
        BackSR = Back.GetComponent<SpriteRenderer>();
        DelayCheckRecord = 0f;
        MyTimer = 0f;
        //=========================================================== Объявление
        //=========================================================== Определение текущего мода
        if (GetActiveSceneScript.currentScene == SS.SwipeMode)
        {
            record = RecordSwipe;
            uiBackRed = UiBackRedSwipe;
            uiBackGreen = UiBackGreenSwipe;
            uiBackBlue = UiBackBlueSwipe;

        }
        else if (GetActiveSceneScript.currentScene == SS.StoneMode)
        {
            record = RecordStone;
            uiBackRed = UiBackRedStone;
            uiBackGreen = UiBackGreenStone;
            uiBackBlue = UiBackBlueStone;
        }
        else if (GetActiveSceneScript.currentScene == SS.BigFishMode)
        {
            TimerCanStart = true;
            record = RecordBigFish;
            uiBackRed = UiBackRedBigFish;
            uiBackGreen = UiBackGreenBigFish;
            uiBackBlue = UiBackBlueBigFish;
        }
        //=========================================================== Определение текущего мода
        //=========================================================== Проверка на новая ли игра
        if (!PlayerPrefs.HasKey(record)
            || !PlayerPrefs.HasKey(uiBackRed)
            || !PlayerPrefs.HasKey(uiBackGreen)
            || !PlayerPrefs.HasKey(uiBackBlue))
        {
            PlayerPrefs.SetFloat(record, 0f);
            PlayerPrefs.SetFloat(uiBackRed, 0.9019608f);
            PlayerPrefs.SetFloat(uiBackGreen, 0.9019608f);
            PlayerPrefs.SetFloat(uiBackBlue, 1f);
        }
        UiBackImage.color = new Color(PlayerPrefs.GetFloat(uiBackRed), PlayerPrefs.GetFloat(uiBackGreen), PlayerPrefs.GetFloat(uiBackBlue), 0.8f);
        //=========================================================== Проверка на новая ли игра
    }
    void Update()
    {
        //=========================================================== Таймер
        DelayCheckRecord += Time.deltaTime;
        if (TimerCanStart)
        {
            MyTimer += Time.deltaTime;
        }
        //=========================================================== Таймер
        //=========================================================== Задержка для обновления рекорда
        if (DelayCheckRecord >= 1)
        {
            DelayCheckRecord = 0;

            CheckMyRecord();
        }
        //=========================================================== Задержка для обновления рекорда
    }
    public void CheckMyRecord()//writes prepared colors in background, which is setted by animation
    {
        if (PlayerPrefs.GetFloat(record) < MyTimer)
        {
            PlayerPrefs.SetFloat(record, MyTimer);
            PlayerPrefs.SetFloat(uiBackRed, BackSR.color.r);
            PlayerPrefs.SetFloat(uiBackGreen, BackSR.color.g);
            PlayerPrefs.SetFloat(uiBackBlue, BackSR.color.b);
            UiBackImage.color = new Color(PlayerPrefs.GetFloat(uiBackRed), PlayerPrefs.GetFloat(uiBackGreen), PlayerPrefs.GetFloat(uiBackBlue), 0.8f);
            //=========================================================== Проверка на отображение награды перед самой смертью
            if (!PlayerPrefs.HasKey(SS.award1Reached) //get second
                && GetActiveSceneScript.currentScene == SS.SwipeMode
                && PlayerPrefs.GetFloat(record) > 240f
                && PremiumActivatorScript.ActivationStatus)
            {
                awardsScript.nextTypeOfAward = awardsScript.award1;
                restart.OnDeathD = awardsScript.ShowAward;
                PlayerPrefs.SetInt(SS.award1Reached, 1);
                PlayerPrefs.SetInt(SS.modeShine, 1);
            }
            if (!PlayerPrefs.HasKey(SS.award2Reached) //get third
                && GetActiveSceneScript.currentScene == SS.StoneMode
                && PlayerPrefs.GetFloat(record) > 300f)
            {
                awardsScript.nextTypeOfAward = awardsScript.award2;
                restart.OnDeathD = awardsScript.ShowAward;
                PlayerPrefs.SetInt(SS.award2Reached, 1);
                PlayerPrefs.SetInt(SS.modeShine, 1);
            }
            if (!PlayerPrefs.HasKey(SS.premiumReached) //get premium
                && GetActiveSceneScript.currentScene == SS.SwipeMode
                && PlayerPrefs.GetFloat(record) > 360f)
            {
                awardsScript.nextTypeOfAward = awardsScript.premium;
                restart.OnDeathD = awardsScript.ShowAward;
                PremiumActivatorScript.ActivationStatus = true;
                PlayerPrefs.SetInt(SS.modeShine, 1);
                PlayerPrefs.SetInt(SS.award1Reached, 1);
                PlayerPrefs.SetInt(SS.modeShine, 1);
            }
            if (!PlayerPrefs.HasKey(SS.winReached) //get surprise
                && PlayerPrefs.GetFloat(RecordSwipe) > 360f
                && PlayerPrefs.GetFloat(RecordStone) > 360f
                && PlayerPrefs.GetFloat(RecordBigFish) > 360f)
            {
                awardsScript.nextTypeOfAward = awardsScript.win;
                restart.OnDeathD = awardsScript.ShowAward;
                PlayerPrefs.SetInt(SS.winReached, 1);
                PlayerPrefs.SetInt(SS.modeShine, 1);
            }
            //=========================================================== Проверка на отображение награды перед самой смертью
        }
    }
}
