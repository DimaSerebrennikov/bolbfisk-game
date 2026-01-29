using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeScript : MonoBehaviour
{
    //=========================================================== ЗамкИ
    public GameObject Lock1;
    public GameObject Lock2;
    //=========================================================== ЗамкИ
    //=========================================================== Полоски
    public Image strip1;
    public Image strip2;
    public Image strip3;
    //=========================================================== Полоски
    //=========================================================== Спрайты модов
    public Image mode1;
    public Image mode2;
    public Image mode3;

    public Sprite[] animationMode1;
    public Sprite[] animationMode2;
    public Sprite[] animationMode3;
    //=========================================================== Спрайты модов
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
    public Transform toggle;
    bool itsMoving;
    Action MovingAct;
    int movingTimes;
    float step;
    const int interpolation = 80;
    public AnimationCurve curve;
    int willBeSelected;
    List<Coroutine> allPlayAnimation;
    bool firstClick;
    int nowSelecting;
    //=========================================================== Locky
    public GameObject locky1;
    public GameObject locky2;
    public GameObject locky3;
    //=========================================================== Locky
    //=========================================================== Адаптированная анимация
    float time;
    float targetTime;
    bool firstUpdate;
    //=========================================================== Адаптированная анимация
    public AwardsScript awards;
    public Restart restart;
    Color startColor;
    private void Awake()
    {
        startColor = new Color(0.9019608f, 0.9019608f, 1f);
        time = 0f;
        targetTime = 0.016f;
        firstUpdate = false;
    }
    private void OnEnable()
    {
        MyReset();
        //=========================================================== Работа с замочками и замками
        if (!PremiumActivatorScript.ActivationStatus)
        {
            Lock1.GetComponent<Image>().color = new Color(1,0.7960784f, 0.1529412f, 1);
        }
        else if (PlayerPrefs.HasKey(SS.award1Reached))
        {
            Lock1.SetActive(false);
        }
        if (PlayerPrefs.HasKey(SS.award2Reached))
        {
            Lock2.SetActive(false);
        }
        if (PlayerPrefs.HasKey(SS.modeShine))
        {
            PlayerPrefs.DeleteKey(SS.modeShine);
        }
        if (PlayerPrefs.HasKey(SS.RecordSwipe)
            && PlayerPrefs.GetFloat(SS.RecordSwipe) >= 360f)
        {
            locky1.SetActive(false);
        }
        if (PlayerPrefs.HasKey(SS.RecordStone)
            && PlayerPrefs.GetFloat(SS.RecordStone) >= 360f)
        {
            locky2.SetActive(false);
        }
        if (PlayerPrefs.HasKey(SS.RecordBigFish)
            && PlayerPrefs.GetFloat(SS.RecordBigFish) >= 360f)
        {
            locky3.SetActive(false);
        }
        //=========================================================== Работа с замочками и замками
        //=========================================================== Свайп
        if (PlayerPrefs.HasKey(RecordSwipe))
        {
            strip1.color = new Color
                (PlayerPrefs.GetFloat(UiBackRedSwipe), PlayerPrefs.GetFloat(UiBackGreenSwipe), PlayerPrefs.GetFloat(UiBackBlueSwipe));
        }
        else
        {
            strip1.color = startColor;
        }
        //=========================================================== Свайп
        //=========================================================== Камень
        if (PlayerPrefs.HasKey(RecordStone))
        {
            strip2.color = new Color
                (PlayerPrefs.GetFloat(UiBackRedStone), PlayerPrefs.GetFloat(UiBackGreenStone), PlayerPrefs.GetFloat(UiBackBlueStone));
        }
        else
        {
            strip2.color = startColor;
        }
        //=========================================================== Камень
        //=========================================================== Большая рыба
        if (PlayerPrefs.HasKey(RecordBigFish))
        {
            strip3.color = new Color
                (PlayerPrefs.GetFloat(UiBackRedBigFish), PlayerPrefs.GetFloat(UiBackGreenBigFish), PlayerPrefs.GetFloat(UiBackBlueBigFish));
        }
        else
        {
            strip3.color = startColor;
        }
        //=========================================================== Большая рыба
    }
    void MyReset()
    {
        allPlayAnimation = new List<Coroutine>();
        step = 0f;
        movingTimes = 0;
        MovingAct = () => { };
        itsMoving = false;
        willBeSelected = 0;
        firstClick = true;
        nowSelecting = 0;
        if (GetActiveSceneScript.currentScene == SS.SwipeMode)
        {
            toggle.position = strip1.transform.position;
        }
        else if (GetActiveSceneScript.currentScene == SS.StoneMode)
        {
            toggle.position = strip2.transform.position;
        }
        else if (GetActiveSceneScript.currentScene == SS.BigFishMode)
        {
            toggle.position = strip3.transform.position;
        }
        mode1.sprite = animationMode1[0];
        mode2.sprite = animationMode2[0];
        mode3.sprite = animationMode3[0];
    }
    private void Update()
    {
        if (firstUpdate)
        {
            time += Time.unscaledDeltaTime;
            while (time > targetTime)
            {
                time -= targetTime;
                MovingAct();
            }
        }
        else
        {
            firstUpdate = true;
        }
    }
    //=========================================================== Методы для анимации объекта в Update
    //----------------------------------------------------------- Свайп
    void MovingSwipe()
    {
        if (itsMoving)
        {
            float evalute = curve.Evaluate((float)movingTimes / (float)interpolation);
            toggle.position = new Vector2(strip1.transform.position.x, toggle.position.y + step * evalute);
            movingTimes++;
            if (movingTimes > interpolation)
            {
                itsMoving = false;
                nowSelecting = 0;
                CloseAllPlayAnimation();
                allPlayAnimation.Add(StartCoroutine(PlayAnimation(1)));
            }
        }
    }
    //----------------------------------------------------------- Свайп
    //----------------------------------------------------------- Стон
    void MovingStone()
    {
        if (itsMoving)
        {
            float evalute = curve.Evaluate((float)movingTimes / (float)interpolation);
            toggle.position = new Vector2(strip2.transform.position.x, toggle.position.y + step * evalute);
            movingTimes++;
            if (movingTimes > interpolation)
            {
                itsMoving = false;
                nowSelecting = 0;
                CloseAllPlayAnimation();
                allPlayAnimation.Add(StartCoroutine(PlayAnimation(2)));
            }
        }
    }
    //----------------------------------------------------------- Стон
    //----------------------------------------------------------- Эном
    void MovingEnom()
    {
        if (itsMoving)
        {
            float evalute = curve.Evaluate((float)movingTimes / (float)interpolation);
            toggle.position = new Vector2(strip3.transform.position.x, toggle.position.y + step * evalute);
            movingTimes++;
            if (movingTimes > interpolation)
            {
                itsMoving = false;
                nowSelecting = 0;
                CloseAllPlayAnimation();
                allPlayAnimation.Add(StartCoroutine(PlayAnimation(3)));
            }
        }
    }
    //----------------------------------------------------------- Эном
    //=========================================================== Методы для анимации объекта в Update
    //=========================================================== События кнопок
    //----------------------------------------------------------- Свайп
    public void ClickOnSwipe()
    {
        if (GetActiveSceneScript.currentScene == SS.SwipeMode
            && firstClick)
        {
            itsMoving = false;
            allPlayAnimation.Add(StartCoroutine(PlayAnimation(1)));
        }
        else if (willBeSelected == 1)
        {
            OpenNewMode(SS.SwipeMode);
        }
        else if (nowSelecting != 1
            && mode1.sprite == animationMode1[0])
        {
            CloseAllPlayAnimation();
            mode2.sprite = animationMode2[0];
            mode3.sprite = animationMode3[0];

            willBeSelected = 0;
            nowSelecting = 1;
            movingTimes = 0;
            step = (strip1.transform.position.y - toggle.transform.position.y) / interpolation;
            itsMoving = true;
            MovingAct = MovingSwipe;
        }
        firstClick = false;
    }
    //----------------------------------------------------------- Свайп
    //----------------------------------------------------------- Стон
    public void ClickOnStone()
    {
        if (!Lock1.activeSelf)
        {
            if (GetActiveSceneScript.currentScene == SS.StoneMode
                && firstClick)
            {
                itsMoving = false;
                allPlayAnimation.Add(StartCoroutine(PlayAnimation(2)));
            }
            else if (willBeSelected == 2)
            {
                OpenNewMode(SS.StoneMode);
            }
            else if (nowSelecting != 2
                && mode2.sprite == animationMode2[0])
            {
                CloseAllPlayAnimation();

                mode3.sprite = animationMode3[0];
                mode1.sprite = animationMode1[0];

                willBeSelected = 0;
                nowSelecting = 2;
                movingTimes = 0;
                step = (strip2.transform.position.y - toggle.transform.position.y) / interpolation;
                itsMoving = true;
                MovingAct = MovingStone;
            }
            firstClick = false;
        }
    }
    //----------------------------------------------------------- Стон
    //----------------------------------------------------------- Эном
    public void ClickOnBigFish()
    {
        if (!Lock2.activeSelf)
        {
            if (GetActiveSceneScript.currentScene == SS.BigFishMode
                && firstClick)
            {
                itsMoving = false;
                allPlayAnimation.Add(StartCoroutine(PlayAnimation(3)));
            }
            else if (willBeSelected == 3)
            {
                OpenNewMode(SS.BigFishMode);
            }
            else if (nowSelecting != 3
                && mode3.sprite == animationMode3[0])
            {
                CloseAllPlayAnimation();
                mode2.sprite = animationMode2[0];
                mode1.sprite = animationMode1[0];

                willBeSelected = 0;
                nowSelecting = 3;
                movingTimes = 0;
                step = (strip3.transform.position.y - toggle.transform.position.y) / interpolation;
                itsMoving = true;
                MovingAct = MovingEnom;
            }
            firstClick = false;
        }
    }
    //----------------------------------------------------------- Эном
    //=========================================================== События кнопок мода
    public void OpenNewMode(string mode) 
    {
        Time.timeScale = 1f;
        if (restart.OnDeathD == awards.ShowAward)
        {
            awards.nextScene = mode;
            awards.ShowAward();
        }
        else
        {
            SceneManager.LoadScene(mode);
        }
    }
    //=========================================================== Анимация для подтверждения
    IEnumerator PlayAnimation(int modeNumber)
    {
        if (modeNumber == 1)
        {
            for (int a = 0; a < animationMode1.Length; a++)
            {
                mode1.sprite = animationMode1[a];
                yield return null;
            }
            willBeSelected = 1;
        }
        else if (modeNumber == 2)
        {
            for (int a = 0; a < animationMode2.Length; a++)
            {
                mode2.sprite = animationMode2[a];
                yield return null;
            }
            willBeSelected = 2;
        }
        else if (modeNumber == 3)
        {
            for (int a = 0; a < animationMode3.Length; a++)
            {
                mode3.sprite = animationMode3[a];
                yield return null;
            }
            willBeSelected = 3;
        }
    }
    void CloseAllPlayAnimation()
    {
        for (int a = 0; a < allPlayAnimation.Count; a++)
        {
            StopCoroutine(allPlayAnimation[a]);
        }
        allPlayAnimation.Clear();
    }
    //=========================================================== Анимация для подтверждения
}