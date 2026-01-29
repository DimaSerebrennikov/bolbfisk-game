using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ArenaScript : MonoBehaviour
{
    int FishAmount;
    int FishAmountSaved;
    [SerializeField] GameObject WallUp;
    [SerializeField] GameObject WallDown;
    [SerializeField] GameObject WallRight;
    [SerializeField] GameObject WallLeft;
    [SerializeField] GameObject FeederUp;
    [SerializeField] GameObject FeederDown;
    [SerializeField] GameObject FeederRight;
    [SerializeField] GameObject FeederLeft;
    [SerializeField] GameObject Backgrounds;
    float FloatScreenWidth;
    float FloatScrennHeight;
    public static int countBirthPredator;
    const float COEFMD = 14;//coefficient of minimum distance, the less value the bigger distance needs for thowing a stone;
    float PerStepSwipePower;
    float PerStepStonePower;
    Action onRisingFishAmount;
    float defaultOrtoSize;
    float stepChangeOrtoSize;
    const float tarOrt = 5f;
    float directSquare;
    public static bool firstArenaUp;

    void Awake()//After SwipeScript and StoneScript
    {
        //=========================================================== Ёкран
        FloatScreenWidth = Screen.width;
        FloatScrennHeight = Screen.height;
        //=========================================================== Ёкран
        //=========================================================== ‘ормула адаптации размера арены под устройство
        directSquare = tarOrt * FloatScrennHeight / FloatScreenWidth;
        Camera.main.orthographicSize = (tarOrt + directSquare) / 2;
        //=========================================================== ‘ормула адаптации размера арены под устройство
        //=========================================================== ќбъ€влени€
        defaultOrtoSize = Camera.main.orthographicSize;
        stepChangeOrtoSize = defaultOrtoSize / 35f;
        if (GetActiveSceneScript.currentScene != SS.BigFishMode)
        {
            onRisingFishAmount = SecondAndFirstMode;
        }
        else
        {
            onRisingFishAmount = () => { };
            Camera.main.orthographicSize = Camera.main.orthographicSize * 1.25f;
        }
        PerStepSwipePower = Swipe.SwipePower / 35f;
        PerStepStonePower = StoneScript.power / 35f;
        countBirthPredator = 1;
        FishAmount = 0;
        FishAmountSaved = 1;
        firstArenaUp = false;
        //=========================================================== ќбъ€влени€
        var TempBackgrounds = Backgrounds.transform.position;
        Backgrounds.transform.position = new Vector2(TempBackgrounds.x + Random.Range(-5f, 5f), TempBackgrounds.y + Random.Range(-5f, 5f));
        SetFeedersAndWalls();
    }
    void Update()
    {
        onRisingFishAmount();
        //------------------------------------------------------------------------------------------------------------------------------------------PC
        //WallUp.transform.position = new Vector2(0, FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 5);
        //WallDown.transform.position = new Vector2(0, -1 * (FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 5));
        //WallRight.transform.position = new Vector2(Camera.main.orthographicSize + 5, 0);
        //WallLeft.transform.position = new Vector2(-1 * (Camera.main.orthographicSize + 5), 0); 

        //FeederUp.transform.localScale = new Vector2(Camera.main.orthographicSize * 2, 0.1f);
        //FeederDown.transform.localScale = new Vector2(Camera.main.orthographicSize * 2, 0.1f);
        //FeederRight.transform.localScale = new Vector2(0.1f, FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize * 2);
        //FeederLeft.transform.localScale = new Vector2(0.1f, FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize * 2);

        //FeederUp.transform.position = new Vector2(0, FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 0.5f);
        //FeederDown.transform.position = new Vector2(0, -1 * (FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 0.5f));
        //FeederRight.transform.position = new Vector2(Camera.main.orthographicSize + 0.5f, 0);
        //FeederLeft.transform.position = new Vector2(-1 * (Camera.main.orthographicSize + 0.5f), 0);
        //------------------------------------------------------------------------------------------------------------------------------------------PC
    }
    void SetFeedersAndWalls()//set walls and feeder to screen size
    {
        WallUp.transform.position = new Vector2(0, Camera.main.orthographicSize + 10);
        WallDown.transform.position = new Vector2(0, -1 * (Camera.main.orthographicSize + 10));
        WallRight.transform.position = new Vector2(FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 10, 0);
        WallLeft.transform.position = new Vector2(-1 * (FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 10), 0);

        FeederUp.transform.localScale = new Vector2(FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize * 2, 0.1f);
        FeederDown.transform.localScale = new Vector2(FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize * 2, 0.1f);
        FeederRight.transform.localScale = new Vector2(0.1f, Camera.main.orthographicSize * 2);
        FeederLeft.transform.localScale = new Vector2(0.1f, Camera.main.orthographicSize * 2);

        FeederUp.transform.position = new Vector2(0, Camera.main.orthographicSize + 0.5f);
        FeederDown.transform.position = new Vector2(0, -1 * (Camera.main.orthographicSize + 0.5f));
        FeederRight.transform.position = new Vector2(FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 0.5f, 0);
        FeederLeft.transform.position = new Vector2(-1 * (FloatScreenWidth / FloatScrennHeight * Camera.main.orthographicSize + 0.5f), 0);
    }
    void SecondAndFirstMode()
    {
        FishAmount = ObjectListScript.playerBrutalList.Count + ObjectListScript.playerPassiveList.Count;
        if (FishAmount > FishAmountSaved && Camera.main.orthographicSize < defaultOrtoSize * 2)
        {
            firstArenaUp = true;
            int difInAmount = FishAmount - FishAmountSaved;
            FishAmountSaved = FishAmount;
            Camera.main.orthographicSize = Camera.main.orthographicSize + stepChangeOrtoSize * difInAmount;
            //500/(7/0.2) => начальна€ сила делитьс€ на количество всЄ количество шагов
            Swipe.SwipePower = Swipe.SwipePower + PerStepSwipePower * difInAmount;
            StoneScript.power = StoneScript.power + PerStepStonePower * difInAmount;
            //change MinimumDistance coefficient. Width divides on coefficient
        }
        else if (FishAmount < FishAmountSaved && Camera.main.orthographicSize > defaultOrtoSize)
        {
            int difInAmount = FishAmountSaved - FishAmount;
            FishAmountSaved = FishAmount;
            Camera.main.orthographicSize = Camera.main.orthographicSize - stepChangeOrtoSize * difInAmount;
            //500/(7/0.2) => начальна€ сила делитьс€ на количество всЄ количество шагов
            Swipe.SwipePower = Swipe.SwipePower - PerStepSwipePower * difInAmount;
            StoneScript.power = StoneScript.power - PerStepStonePower * difInAmount;
            //change MinimumDistance coefficient. Width divides on coefficient
        }
        StoneScript.MinimumDistance = FeederUp.transform.localScale.x / COEFMD;
        SetFeedersAndWalls();
    }
}
