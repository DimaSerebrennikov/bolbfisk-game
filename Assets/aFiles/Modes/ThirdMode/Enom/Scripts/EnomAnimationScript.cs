using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EnomAnimationScript : MonoBehaviour
{
    //=========================================================== Объекты анимации
    public GameObject Mouth;
    public GameObject Teeth;
    public GameObject Eye;
    public GameObject Fin;
    public GameObject Body;
    //=========================================================== Объекты анимации
    //=========================================================== Компоненты объектов анимации
    SpriteRenderer MouthSR;
    SpriteRenderer TeethSR;
    SpriteRenderer EyeSR;
    SpriteRenderer FinSR;
    SpriteRenderer BodySR;
    public SpriteRenderer DeathSR;
    public PolygonCollider2D myCollider;
    Vector2[] colliderPoints;
    //=========================================================== Компоненты объектов анимации
    //=========================================================== Кадры
    public Sprite[] MouthList;
    public Sprite[] TeethList;
    public Sprite[] EyeList;
    public Sprite[] FinList;
    public Sprite[] BodyList;
    public Sprite[] DeathList;
    //----------------------------------------------------------- Варианты рта
    public Sprite[] Mouth0;
    public Sprite[] Mouth1;
    public Sprite[] Mouth2;
    public Sprite[] Mouth3;
    public Sprite[] Mouth4;
    public Sprite[] Mouth5;
    public Sprite[] Mouth6;
    public Sprite[] Mouth7;
    public Sprite[] Mouth8;
    public Sprite[] Mouth9;
    public Sprite[] Mouth10;
    public Sprite[] Mouth11;
    public Sprite[] Mouth12;
    public Sprite[] Mouth13;
    public Sprite[] Mouth14;
    //----------------------------------------------------------- Варианты рта
    //----------------------------------------------------------- Варианты плавника
    public Sprite[] Fin0;
    public Sprite[] Fin1;
    public Sprite[] Fin2;
    public Sprite[] Fin3;
    public Sprite[] Fin4;
    public Sprite[] Fin5;
    public Sprite[] Fin6;
    public Sprite[] Fin7;
    public Sprite[] Fin8;
    public Sprite[] Fin9;
    public Sprite[] Fin10;
    public Sprite[] Fin11;
    public Sprite[] Fin12;
    public Sprite[] Fin13;
    public Sprite[] Fin14;
    //----------------------------------------------------------- Варианты плавника
    //=========================================================== Кадры
    static int size;
    public static int Size
    {
        get
        {
            return size;
        }
        set
        {
            if (value <= 14 &&
                value >= 0)
            {
                size = value;
            }
            else if (value >= 15)
            {
                iAmDiying = true;
            }
        }
    }
    //=========================================================== Для анимаций
    public static bool iAmDiying;
    int finCounter;
    int deathCounter;
    float finTPFrame;
    bool finReversing;
    int eatCounter;
    float eatTPFrame;
    bool eatReversing;
    bool oneTimeReverseEating;
    public static bool nowEating;
    //=========================================================== Для анимаций
    //=========================================================== Програмное изменение размерностей при увеличении рыбы
    float finLocalScale;
    float finLocalPositionX;
    float finLocalPositionY;
    float finLocalScaleStep;
    float finLocalPositionXStep;
    float finLocalPositionYStep;
    float eyeLocalPositionX;
    float eyeLocalPositionY;
    float eyeLocalPositionXStep;
    float eyeLocalPositionYStep;
    //=========================================================== Програмное изменение размерностей при увеличении рыбы
    public int sizeEditor;
    public GameObject meat;
    public CameraShaker cameraShaker;
    public AwardsScript awards;
    public Restart restart;
    private void Awake()
    {
        //=========================================================== Объявление
        MouthSR = Mouth.GetComponent<SpriteRenderer>();
        TeethSR = Teeth.GetComponent<SpriteRenderer>();
        EyeSR = Eye.GetComponent<SpriteRenderer>();
        FinSR = Fin.GetComponent<SpriteRenderer>();
        BodySR = Body.GetComponent<SpriteRenderer>();
        size = 0;
        oneTimeReverseEating = false;
        finCounter = 0;
        deathCounter = 0;
        finReversing = false;
        eatCounter = 0;
        eatReversing = false;
        nowEating = false;
        iAmDiying = false;
        colliderPoints = myCollider.points;

        //----------------------------------------------------------- Временные значения плавника
        float startFinLocalScale = 0.4330745f;
        float startFinLocalPositionX = -1.404f;
        float startFinLocalPositionY = 0.298f;
        float finishFinLocalScale = 0.9556882f;
        float finishFinLocalPositionX = -4.77f;
        float finishFinLocalPositionY = -0.21f;
        float countOfBind = 14f;
        //----------------------------------------------------------- Временные значения плавника
        finLocalScale = startFinLocalScale;
        finLocalScaleStep = (finishFinLocalScale - startFinLocalScale) / countOfBind;
        finLocalPositionX = startFinLocalPositionX;
        finLocalPositionXStep = (finishFinLocalPositionX - startFinLocalPositionX) / countOfBind;
        finLocalPositionY = startFinLocalPositionY;
        finLocalPositionYStep = (finishFinLocalPositionY - startFinLocalPositionY) / countOfBind;
        //----------------------------------------------------------- Временные значения для глаза
        float startEyeLocalPositionX = 0.801f;
        float startEyeLocalPositionY = 1.011f;
        float finishEyeLocalPositionX = -0.2f;
        float finishEyeLocalPositionY = 2.43f;
        //----------------------------------------------------------- Временные значения для глаза
        eyeLocalPositionX = startEyeLocalPositionX;
        eyeLocalPositionXStep = (finishEyeLocalPositionX - startEyeLocalPositionX) / countOfBind;
        eyeLocalPositionY = startEyeLocalPositionY;
        eyeLocalPositionYStep = (finishEyeLocalPositionY - startEyeLocalPositionY) / countOfBind;
        //=========================================================== Объявление
        EnomLipsScript.onEating += ChangeSize;
        Size = 1;
        ChangeSize();
    }
    private void Update()
    {
        //=========================================================== Анимация действующая в каждом кадре
        Finning();
        Eating();
        //=========================================================== Анимация действующая в каждом кадре
    }
    void Eating()
    {
        eatTPFrame += Time.deltaTime;
        while (eatTPFrame > 0.025f)
        {
            eatTPFrame -= 0.025f;
            if (nowEating)//включена анимацию поедания и 1 кадр раз в ограничение
            {
                //=========================================================== Перебирает кадры в допустимом диапазоне
                if (eatCounter < MouthList.Length &&
                    eatCounter >= 0)
                {
                    MouthSR.sprite = MouthList[eatCounter];
                    TeethSR.sprite = TeethList[eatCounter];
                }
                //=========================================================== Перебирает кадры в допустимом диапазоне
                CounterWithReverse(MouthList.Length, ref eatCounter, ref eatReversing, ref nowEating, ref oneTimeReverseEating);
                if (!nowEating)
                {
                    MouthSR.sprite = MouthList[0];
                    TeethSR.sprite = TeethList[0];
                }
            }
        }
    }
    void Finning()
    {
        finTPFrame += Time.deltaTime;
        while (finTPFrame > 0.02f)
        {
            finTPFrame -= 0.02f;
            //=========================================================== Перебирает кадры в допустимом диапазоне
            if (finCounter < FinList.Length &&
                finCounter >= 0)
            {
                FinSR.sprite = FinList[finCounter];
            }
            //=========================================================== Перебирает кадры в допустимом диапазоне
            CounterWithReverse(FinList.Length, ref finCounter, ref finReversing);
        }
    }
    //void Eyeing()
    //{
    //    if (target != null)
    //    {
    //        rb.transform.right = Vector3.Lerp(rb.transform.right, (target.transform.position - rb.transform.position), 1.2f * Time.fixedDeltaTime);
    //    }
    //    else
    //    {
    //        closestDist = Mathf.Infinity;
    //    }
    //    Eye.transform.right = 
    //}
    void CounterWithReverse(int length, ref int counter, ref bool reversing)
    {
        //=========================================================== Насчитывание кадров
        if (!reversing)
        {
            counter++;
        }
        else
        {
            counter--;
        }
        //=========================================================== Насчитывание кадров
        //=========================================================== Обозначает что счёт стоит на границе
        if (counter >= length - 1)
        {
            counter = length - 1;
            reversing = true;
        }
        else if (counter <= 0)
        {
            counter = 0;
            reversing = false;
        }
        //=========================================================== Обозначает что счёт стоит на границе
    }
    void CounterWithReverse(int length, ref int counter, ref bool reversing, ref bool acception, ref bool oneTimeReverse)
    {
        //=========================================================== Насчитывание кадров
        if (!reversing)
        {
            counter++;
        }
        else
        {
            counter--;
        }
        //=========================================================== Насчитывание кадров
        //=========================================================== Обозначает что счёт стоит на границе
        if (counter >= length - 1)
        {
            counter = length - 1;
            reversing = true;
            oneTimeReverse = true;
        }
        else if (counter <= 0)
        {
            counter = 0;
            reversing = false;
            if (oneTimeReverse)
            {
                acception = false;
                oneTimeReverse = false;
            }
        }
        //=========================================================== Обозначает что счёт стоит на границе
    }
    public void ChangeSize()
    {
        if (iAmDiying)
        {
            StartCorDeathAnimation();
        }
        else
        {
            //=========================================================== Подстроить рот под размер тела
            if (size == 0)
            {
                MouthList = Mouth0;
                FinList = Fin0;
                Mouth.transform.localPosition = new Vector2(0.343f, -0.16f);
                Mouth.transform.localScale = new Vector2(0.4751921f, 0.4751921f);
                ChangeColliderPoints(
                    new Vector2(-1.71f, 1.79f),
                    new Vector2(-1.16f, 1.14f),
                    new Vector2(-2.33f, 0.20f),
                    new Vector2(-1.52f, -0.62f),
                    new Vector2(-1.06f, -1.11f),
                    new Vector2(-0.55f, -1.26f),
                    new Vector2(-0.05f, -1.22f),
                    new Vector2(0.39f, -1.03f),
                    new Vector2(0.79f, -0.73f),
                    new Vector2(-0.14f, 0.31f),
                    new Vector2(1.34f, 0.96f),
                    new Vector2(0.84f, 1.36f),
                    new Vector2(0.25f, 1.49f),
                    new Vector2(-0.32f, 1.50f),
                    new Vector2(-0.39f, 2.31f),
                    new Vector2(-1.15f, 2.31f));
            }
            else if (size == 1)
            {
                MouthList = Mouth1;
                FinList = Fin1;
                Mouth.transform.localPosition = new Vector2(0.414f, -0.236f);
                Mouth.transform.localScale = new Vector2(0.5410624f, 0.5410624f);
                ChangeColliderPoints(
                    new Vector2(-1.82f, 1.92f),
                    new Vector2(-1.26f, 1.29f),
                    new Vector2(-2.60f, 0.22f),
                    new Vector2(-1.60f, -0.96f),
                    new Vector2(-1.23f, -1.29f),
                    new Vector2(-0.57f, -1.48f),
                    new Vector2(-0.01f, -1.46f),
                    new Vector2(0.54f, -1.25f),
                    new Vector2(0.92f, -0.97f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(1.53f, 1.07f),
                    new Vector2(1.15f, 1.41f),
                    new Vector2(0.56f, 1.64f),
                    new Vector2(-0.42f, 1.61f),
                    new Vector2(-0.47f, 2.45f),
                    new Vector2(-1.24f, 2.44f));
            }
            else if (size == 2)
            {
                MouthList = Mouth2;
                FinList = Fin2;
                Mouth.transform.localPosition = new Vector2(0.463f, -0.328f);
                Mouth.transform.localScale = new Vector2(0.599791f, 0.599791f);
                ChangeColliderPoints(
                    new Vector2(-1.89f, 2.06f),
                    new Vector2(-1.38f, 1.41f),
                    new Vector2(-2.97f, 0.16f),
                    new Vector2(-1.84f, -1.05f),
                    new Vector2(-1.35f, -1.48f),
                    new Vector2(-0.60f, -1.68f),
                    new Vector2(0.02f, -1.64f),
                    new Vector2(0.63f, -1.37f),
                    new Vector2(0.95f, -1.08f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(1.80f, 1.08f),
                    new Vector2(1.31f, 1.54f),
                    new Vector2(0.50f, 1.79f),
                    new Vector2(-0.49f, 1.75f),
                    new Vector2(-0.57f, 2.57f),
                    new Vector2(-1.32f, 2.55f));
            }
            else if (size == 3)
            {
                MouthList = Mouth3;
                FinList = Fin3;
                Mouth.transform.localPosition = new Vector2(0.518f, -0.404f);
                Mouth.transform.localScale = new Vector2(0.6641699f, 0.6641699f);
                ChangeColliderPoints(
                    new Vector2(-1.99f, 2.17f),
                    new Vector2(-1.46f, 1.55f),
                    new Vector2(-3.41f, 0.04f),
                    new Vector2(-1.93f, -1.34f),
                    new Vector2(-1.25f, -1.80f),
                    new Vector2(-0.44f, -1.92f),
                    new Vector2(0.19f, -1.84f),
                    new Vector2(0.79f, -1.57f),
                    new Vector2(1.18f, -1.23f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(2.02f, 1.15f),
                    new Vector2(1.54f, 1.63f),
                    new Vector2(0.39f, 1.97f),
                    new Vector2(-0.63f, 1.89f),
                    new Vector2(-0.67f, 2.70f),
                    new Vector2(-1.43f, 2.72f));
            }
            else if (size == 4)
            {
                MouthList = Mouth4;
                FinList = Fin4;
                Mouth.transform.localPosition = new Vector2(0.579f, -0.4721f);
                Mouth.transform.localScale = new Vector2(0.7297517f, 0.7297517f);
                ChangeColliderPoints(
                    new Vector2(-2.10f, 2.35f),
                    new Vector2(-1.54f, 1.69f),
                    new Vector2(-3.60f, 0.32f),
                    new Vector2(-2.04f, -1.58f),
                    new Vector2(-1.18f, -2.08f),
                    new Vector2(-0.29f, -2.14f),
                    new Vector2(0.43f, -1.98f),
                    new Vector2(1.02f, -1.66f),
                    new Vector2(1.35f, -1.32f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(2.22f, 1.21f),
                    new Vector2(1.67f, 1.81f),
                    new Vector2(0.42f, 2.10f),
                    new Vector2(-0.68f, 2.04f),
                    new Vector2(-0.75f, 2.86f),
                    new Vector2(-1.52f, 2.85f));
            }
            else if (size == 5)
            {
                MouthList = Mouth5;
                FinList = Fin5;
                Mouth.transform.localPosition = new Vector2(0.655f, -0.557f);
                Mouth.transform.localScale = new Vector2(0.7956796f, 0.7956796f);
                ChangeColliderPoints(
                    new Vector2(-2.21f, 2.48f),
                    new Vector2(-1.66f, 1.83f),
                    new Vector2(-3.90f, 0.39f),
                    new Vector2(-2.18f, -1.78f),
                    new Vector2(-1.22f, -2.33f),
                    new Vector2(-0.29f, -2.37f),
                    new Vector2(0.49f, -2.20f),
                    new Vector2(1.03f, -1.90f),
                    new Vector2(1.45f, -1.48f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(2.47f, 1.27f),
                    new Vector2(1.73f, 1.97f),
                    new Vector2(0.43f, 2.27f),
                    new Vector2(-0.82f, 2.15f),
                    new Vector2(-0.84f, 3.02f),
                    new Vector2(-1.62f, 3.01f));
            }
            else if (size == 6)
            {
                MouthList = Mouth6;
                FinList = Fin6;
                Mouth.transform.localPosition = new Vector2(0.728f, -0.6371f);
                Mouth.transform.localScale = new Vector2(0.8624635f, 0.8624635f);
                ChangeColliderPoints(
                    new Vector2(-2.29f, 2.62f),
                    new Vector2(-1.76f, 1.99f),
                    new Vector2(-4.42f, 0.55f),
                    new Vector2(-2.37f, -1.97f),
                    new Vector2(-1.44f, -2.54f),
                    new Vector2(-0.42f, -2.63f),
                    new Vector2(0.38f, -2.46f),
                    new Vector2(1.08f, -2.11f),
                    new Vector2(1.49f, -1.67f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(2.72f, 1.25f),
                    new Vector2(1.99f, 2.01f),
                    new Vector2(0.33f, 2.44f),
                    new Vector2(-0.90f, 2.30f),
                    new Vector2(-0.95f, 3.12f),
                    new Vector2(-1.72f, 3.12f));
            }
            else if (size == 7)
            {
                MouthList = Mouth7;
                FinList = Fin7;
                Mouth.transform.localPosition = new Vector2(0.764f, -0.7138f);
                Mouth.transform.localScale = new Vector2(0.9280209f, 0.9280209f);
                ChangeColliderPoints(
                    new Vector2(-2.38f, 2.76f),
                    new Vector2(-1.84f, 2.12f),
                    new Vector2(-4.62f, 0.09f),
                    new Vector2(-2.57f, -2.12f),
                    new Vector2(-1.60f, -2.73f),
                    new Vector2(-0.47f, -2.88f),
                    new Vector2(0.52f, -2.66f),
                    new Vector2(1.17f, -2.34f),
                    new Vector2(1.73f, -1.77f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(2.91f, 1.35f),
                    new Vector2(2.13f, 2.16f),
                    new Vector2(0.44f, 2.59f),
                    new Vector2(-1.01f, 2.42f),
                    new Vector2(-1.04f, 3.30f),
                    new Vector2(-1.84f, 3.30f));
            }
            else if (size == 8)
            {
                MouthList = Mouth8;
                FinList = Fin8;
                Mouth.transform.localPosition = new Vector2(0.834f, -0.797f);
                Mouth.transform.localScale = new Vector2(0.9952355f, 0.9952355f);
                ChangeColliderPoints(
                    new Vector2(-2.49f, 2.87f),
                    new Vector2(-1.93f, 2.26f),
                    new Vector2(-4.87f, 0.05f),
                    new Vector2(-2.81f, -2.19f),
                    new Vector2(-2.10f, -2.77f),
                    new Vector2(-0.63f, -3.09f),
                    new Vector2(0.36f, -2.97f),
                    new Vector2(1.10f, -2.61f),
                    new Vector2(1.93f, -1.89f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(3.13f, 1.47f),
                    new Vector2(2.28f, 2.27f),
                    new Vector2(0.43f, 2.74f),
                    new Vector2(-1.11f, 2.58f),
                    new Vector2(-1.14f, 3.42f),
                    new Vector2(-1.90f, 3.42f));
            }
            else if (size == 9)
            {
                MouthList = Mouth9;
                FinList = Fin9;
                Mouth.transform.localPosition = new Vector2(0.881f, -0.877f);
                Mouth.transform.localScale = new Vector2(1.05976f, 1.05976f);
                ChangeColliderPoints(
                    new Vector2(-2.57f, 3.00f),
                    new Vector2(-2.03f, 2.37f),
                    new Vector2(-5.39f, 0.36f),
                    new Vector2(-3.03f, -2.35f),
                    new Vector2(-2.37f, -2.87f),
                    new Vector2(-1.40f, -3.27f),
                    new Vector2(-0.38f, -3.32f),
                    new Vector2(0.93f, -2.95f),
                    new Vector2(1.98f, -2.11f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(3.39f, 1.48f),
                    new Vector2(2.19f, 2.52f),
                    new Vector2(0.62f, 2.89f),
                    new Vector2(-1.20f, 2.71f),
                    new Vector2(-1.24f, 3.56f),
                    new Vector2(-2.00f, 3.55f));
            }
            else if (size == 10)
            {
                MouthList = Mouth10;
                FinList = Fin10;
                Mouth.transform.localPosition = new Vector2(0.96f, -0.97f);
                Mouth.transform.localScale = new Vector2(1.12412f, 1.12412f);
                ChangeColliderPoints(
                    new Vector2(-2.62f, 3.12f),
                    new Vector2(-2.11f, 2.51f),
                    new Vector2(-5.58f, -0.06f),
                    new Vector2(-3.18f, -2.52f),
                    new Vector2(-2.23f, -3.26f),
                    new Vector2(-1.28f, -3.53f),
                    new Vector2(-0.28f, -3.52f),
                    new Vector2(1.01f, -3.17f),
                    new Vector2(2.18f, -2.19f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(3.58f, 1.53f),
                    new Vector2(2.46f, 2.58f),
                    new Vector2(0.34f, 3.05f),
                    new Vector2(-1.29f, 2.85f),
                    new Vector2(-1.33f, 3.65f),
                    new Vector2(-2.10f, 3.67f));
            }
            else if (size == 11)
            {
                MouthList = Mouth11;
                FinList = Fin11;
                Mouth.transform.localPosition = new Vector2(1.023f, -1.0343f);
                Mouth.transform.localScale = new Vector2(1.191417f, 1.191417f);
                ChangeColliderPoints(
                    new Vector2(-2.62f, 3.12f),
                    new Vector2(-2.11f, 2.51f),
                    new Vector2(-5.58f, -0.06f),
                    new Vector2(-3.18f, -2.52f),
                    new Vector2(-2.23f, -3.26f),
                    new Vector2(-1.28f, -3.53f),
                    new Vector2(-0.28f, -3.52f),
                    new Vector2(1.01f, -3.17f),
                    new Vector2(2.18f, -2.19f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(3.58f, 1.53f),
                    new Vector2(2.46f, 2.58f),
                    new Vector2(0.34f, 3.05f),
                    new Vector2(-1.29f, 2.85f),
                    new Vector2(-1.33f, 3.65f),
                    new Vector2(-2.10f, 3.67f));
            }
            else if (size == 12)
            {
                MouthList = Mouth12;
                FinList = Fin12;
                Mouth.transform.localPosition = new Vector2(1.073f, -1.1111f);
                Mouth.transform.localScale = new Vector2(1.259571f, 1.259571f);
                ChangeColliderPoints(
                    new Vector2(-2.83f, 3.42f),
                    new Vector2(-2.30f, 2.79f),
                    new Vector2(-6.21f, -0.11f),
                    new Vector2(-3.62f, -2.71f),
                    new Vector2(-2.84f, -3.44f),
                    new Vector2(-1.73f, -3.92f),
                    new Vector2(-0.14f, -3.96f),
                    new Vector2(1.37f, -3.45f),
                    new Vector2(2.46f, -2.46f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(3.97f, 1.74f),
                    new Vector2(2.70f, 2.92f),
                    new Vector2(0.28f, 3.35f),
                    new Vector2(-1.48f, 3.12f),
                    new Vector2(-1.53f, 3.97f),
                    new Vector2(-2.29f, 3.93f));
            }
            else if (size == 13)
            {
                MouthList = Mouth13;
                FinList = Fin13;
                Mouth.transform.localPosition = new Vector2(1.134f, -1.189f);
                Mouth.transform.localScale = new Vector2(1.324911f, 1.324911f);
                ChangeColliderPoints(
                    new Vector2(-2.83f, 3.42f),
                    new Vector2(-2.30f, 2.79f),
                    new Vector2(-6.21f, -0.11f),
                    new Vector2(-3.62f, -2.71f),
                    new Vector2(-2.84f, -3.44f),
                    new Vector2(-1.73f, -3.92f),
                    new Vector2(-0.14f, -3.96f),
                    new Vector2(1.37f, -3.45f),
                    new Vector2(2.46f, -2.46f),
                    new Vector2(-0.17f, 0.38f),
                    new Vector2(3.97f, 1.74f),
                    new Vector2(2.70f, 2.92f),
                    new Vector2(0.28f, 3.35f),
                    new Vector2(-1.48f, 3.12f),
                    new Vector2(-1.53f, 3.97f),
                    new Vector2(-2.29f, 3.93f));
            }
            else if (size == 14)
            {
                MouthList = Mouth14;
                FinList = Fin14;
                Mouth.transform.localPosition = new Vector2(1.207f, -1.2691f);
                Mouth.transform.localScale = new Vector2(1.395138f, 1.395138f);
                ChangeColliderPoints(
                    new Vector2(-3.07f, 3.71f),
                    new Vector2(-3.06f, 2.72f),
                    new Vector2(-5.73f, -0.48f),
                    new Vector2(-4.01f, -3.07f),
                    new Vector2(-2.80f, -4.10f),
                    new Vector2(-0.73f, -4.50f),
                    new Vector2(1.31f, -4.02f),
                    new Vector2(2.53f, -2.57f),
                    new Vector2(2.60f, -2.57f),
                    new Vector2(-0.14f, 0.31f),
                    new Vector2(4.01f, 1.72f),
                    new Vector2(3.69f, 2.66f),
                    new Vector2(2.13f, 3.45f),
                    new Vector2(0.40f, 3.66f),
                    new Vector2(-1.28f, 3.50f),
                    new Vector2(-1.67f, 4.30f));
            }
            //=========================================================== Подстроить рот под размер тела
            //=========================================================== Настройка разных частей тела
            MouthSR.sprite = MouthList[eatCounter];
            FinSR.sprite = FinList[finCounter];
            BodySR.sprite = BodyList[size];
            EyeSR.sprite = EyeList[size];
            Fin.transform.localPosition = new Vector2(finLocalPositionX + finLocalPositionXStep * (size + 1), finLocalPositionY + finLocalPositionYStep * (size + 1));
            Fin.transform.localScale = new Vector2(finLocalScale + finLocalScaleStep * (size + 1), finLocalScale + finLocalScaleStep * (size + 1));
            Eye.transform.localPosition = new Vector2(eyeLocalPositionX + eyeLocalPositionXStep * (size + 1), eyeLocalPositionY + eyeLocalPositionYStep * (size + 1));
            //=========================================================== Настройка разных частей тела
            //=========================================================== Смена скорости
            BigFishScript.playerVelocity = BigFishScript.zeroPlayerVelocity
                + ((BigFishScript.zeroPlayerVelocity * BigFishScript.velocityMult) - BigFishScript.zeroPlayerVelocity) / 14 * size;
            //=========================================================== Смена скорости
        }
    }
    //=========================================================== Смерть рыбы со всеми функциями
    void DestroyEnomFish //перезапуск игры сразу после смерти рыбы, смерть - сразу после анимции смерти
        ()
    {
        if (restart.OnDeathD == awards.ShowAward)
        {
            awards.nextScene = SS.BigFishMode;
            awards.ShowAward();
        }
        else
        {
            SceneManager.LoadScene(SS.BigFishMode);
        }
        
    }
    public void StartCorDeathAnimation //перед смертю проигрывается анимация
       ()
    {
        StartCoroutine(DeathAnimation());
    }
    IEnumerator DeathAnimation()
    {
        Fin.SetActive(false);
        Eye.SetActive(false);
        Body.SetActive(false);
        Mouth.SetActive(false);
        Teeth.SetActive(false);
        myCollider.enabled = false;
        ForceAllFishes();
        cameraShaker.StartShaking();
        Vibrator.Vibrate(350);
        BubbleSpammer.CreateBubble(gameObject.transform.position, 100, true);
        //----------------------------------------------------------- Соаздаёт куски мяса
        for (int a = 0; a < 4; a++)
        {
            GameObject createdMeat = Instantiate(meat, gameObject.transform.position, Quaternion.identity);
            createdMeat.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 12f);
        }
        //----------------------------------------------------------- Создаёт куски мяса

        while (deathCounter >= 0 &&
            deathCounter < DeathList.Length)
        {
            DeathSR.sprite = DeathList[deathCounter];
            deathCounter++;
            BubbleSpammer.CreateBubble(gameObject.transform.position, 2, true);
            yield return new WaitForSeconds(0.0166f);
        }
        DestroyEnomFish();
    }
    void ForceAllFishes()
    {
        Vector3 nowPosition = gameObject.transform.position;
        for (int a = 0; a < ObjectListScript.enemyPassiveList.Count; a++)
        {
            GameObject currentGameObject = ObjectListScript.enemyPassiveList[a];
            currentGameObject.GetComponent<Rigidbody2D>().AddForce((currentGameObject.transform.position - nowPosition) * 500f);
        }
        for (int a = 0; a < ObjectListScript.playerPassiveList.Count; a++)
        {
            GameObject currentGameObject = ObjectListScript.playerPassiveList[a];
            currentGameObject.GetComponent<Rigidbody2D>().AddForce((currentGameObject.transform.position - nowPosition) * 500f);
        }
    }
    //=========================================================== Смерть рыбы со всеми функциями
    void ChangeColliderPoints
        (Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Vector2 p5, Vector2 p6, Vector2 p7,
        Vector2 p8, Vector2 p9, Vector2 p10, Vector2 p11, Vector2 p12, Vector2 p13, Vector2 p14, Vector2 p15, Vector2 p16)
    {
        colliderPoints[0] = p1;
        colliderPoints[1] = p2;
        colliderPoints[2] = p3;
        colliderPoints[3] = p4;
        colliderPoints[4] = p5;
        colliderPoints[5] = p6;
        colliderPoints[6] = p7;
        colliderPoints[7] = p8;
        colliderPoints[8] = p9;
        colliderPoints[9] = p10;
        colliderPoints[10] = p11;
        colliderPoints[11] = p12;
        colliderPoints[12] = p13;
        colliderPoints[13] = p14;
        colliderPoints[14] = p15;
        colliderPoints[15] = p16;
        myCollider.points = colliderPoints;
    }
}
