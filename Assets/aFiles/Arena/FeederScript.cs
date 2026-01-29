using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FeederScript : MonoBehaviour
{
    [SerializeField] GameObject food;
    [SerializeField] GameObject feederUp;
    [SerializeField] GameObject feederDown;
    [SerializeField] GameObject feederRight;
    [SerializeField] GameObject feederLeft;
    [SerializeField] GameObject enemyHer;
    [SerializeField] GameObject enemyPredator;
    public GameObject playerHer;
    Vector2 temp;
    Vector2 tempForEnemy;
    Vector2 powerDirectForEnemy;
    Vector2 powerDirect;
    float countBeforePlayerInEnom;
    private void Start()
    {
        countBeforePlayerInEnom = 0f;
        ModeCheck();
    }
    void ModeCheck()
    {
        switch (GetActiveSceneScript.currentScene)
        {
            case "SwipeMode":
                StartCoroutine(WeEatedFirstFood());
                break;
            case "StoneMode":
                StartCoroutine(WeEatedFirstFood());
                break;
            case "BigFishMode":
                StartCoroutine(StartForThirdMode());
                StartCoroutine(Feeder());
                break;
            default:
                Debug.Log("ERROR"); 
                break;
        }
    }
    private IEnumerator Feeder()//creats food
    {
        while (true)
        {
            var a = Random.Range(0, 4);
            if (a == 0)
            {
                temp = new Vector2(feederUp.transform.position.x +
                    Random.Range(-feederUp.transform.localScale.x / 2, feederUp.transform.localScale.x / 2),
                    feederUp.transform.position.y);
            }
            else if (a == 1)
            {
                temp = new Vector2(feederDown.transform.position.x +
                    Random.Range(-feederDown.transform.localScale.x / 2, feederDown.transform.localScale.x / 2),
                    feederDown.transform.position.y);
            }
            else if (a == 2)
            {
                temp = new Vector2(feederRight.transform.position.x,
                    feederRight.transform.position.y +
                    Random.Range(-feederRight.transform.localScale.y / 2, feederRight.transform.localScale.y / 2));
            }
            else
            {
                temp = new Vector2(feederLeft.transform.position.x,
                    feederRight.transform.position.y +
                    Random.Range(-feederRight.transform.localScale.y / 2, feederRight.transform.localScale.y / 2));
            }
            Vector2 TempVector = new Vector2(0, 0) - temp;
            float RandomAngle = Random.Range(-0.785398f, 0.785398f);
            powerDirect = (

                new Vector2(TempVector.x * Mathf.Cos(RandomAngle) - TempVector.y * Mathf.Sin(RandomAngle),
                TempVector.x * Mathf.Sin(RandomAngle) + TempVector.y * Mathf.Cos(RandomAngle))
                ) * 10f;
            ObjectListScript.InstantiateWithCounter(food, temp).GetComponent<Rigidbody2D>().AddForce(powerDirect);
            yield return new WaitForSeconds(BalanceInstrument.FoodDifficult);
        }
    }
    private IEnumerator FeederEnemy()//creats enemy
    {
        while (true)
        {
            yield return new WaitForSeconds(BalanceInstrument.EnemyDifficult);
            CreateEnemy(1);
        }
    }
    private IEnumerator WeEatedFirstFood()//tutorial
    {
        yield return new WaitUntil(() => ArenaScript.firstArenaUp);
        StartCoroutine(WeEatedFish());
    }
    private IEnumerator WeEatedFish()//tutorial
    {
        CreateEnemy(0);//спам только пассивный
         yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => ObjectListScript.enemyPassiveList.Count <= 0);
        //Старт игры
        Timer.TimerCanStart = true;
        StartCoroutine(Feeder());
        if (GetActiveSceneScript.currentScene == SS.SwipeMode)
        {
            StartCoroutine(FeederEnemy());
        }
    }
    void CreateEnemy
        (int WhoIsSpaming)//spam fish, 0 = only passive enemy,
                          //1 = enemy predator + enemy passive,
                          //2 = enemy passive + player passive
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - random feeder
        var a = Random.Range(0, 4);
        if (a == 0)
        {
            tempForEnemy = new Vector2(feederUp.transform.position.x +
                Random.Range(-feederUp.transform.localScale.x / 2, feederUp.transform.localScale.x / 2),
                feederUp.transform.position.y);
        }
        else if (a == 1)
        {
            tempForEnemy = new Vector2(feederDown.transform.position.x +
                Random.Range(-feederDown.transform.localScale.x / 2, feederDown.transform.localScale.x / 2),
                feederDown.transform.position.y);
        }
        else if (a == 2)
        {
            tempForEnemy = new Vector2(feederRight.transform.position.x,
                feederRight.transform.position.y +
                Random.Range(-feederRight.transform.localScale.y / 2, feederRight.transform.localScale.y / 2));
        }
        else
        {
            tempForEnemy = new Vector2(feederLeft.transform.position.x,
                feederRight.transform.position.y +
                Random.Range(-feederRight.transform.localScale.y / 2, feederRight.transform.localScale.y / 2));
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - random feeder
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - set fish in feeder and set random direction for impulse spam
        Vector2 TempVector = new Vector2(0, 0) - tempForEnemy;
        float RandomAngle = Random.Range(-0.785398f, 0.785398f);
        powerDirectForEnemy = (

            new Vector2(TempVector.x * Mathf.Cos(RandomAngle) - TempVector.y * Mathf.Sin(RandomAngle),
            TempVector.x * Mathf.Sin(RandomAngle) + TempVector.y * Mathf.Cos(RandomAngle))
            );
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - set fish in feeder and set random direction for impulse spam
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - instantiate passive fish
        if (WhoIsSpaming == 0)
        {
            GameObject TempHer = ObjectListScript.InstantiateWithCounter(enemyHer, tempForEnemy);
            TempHer.transform.right = new Vector3(0, 0, 0) - TempHer.transform.position;
            TempHer.GetComponent<Rigidbody2D>().AddForce(powerDirectForEnemy * 100);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - instantiate passive fish
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - instantiate passive or predator fish
        else if (WhoIsSpaming == 1)
        {
            int RandomForEnemy = Random.Range(0, 7);//1 к 6
            if (RandomForEnemy == 0)
            {
                GameObject TempHer = ObjectListScript.InstantiateWithCounter(enemyPredator, tempForEnemy);
                TempHer.transform.right = TempHer.transform.position - new Vector3(0, 0, 0);
                TempHer.GetComponent<Rigidbody2D>().AddForce(powerDirectForEnemy * 5);
            }
            else
            {
                GameObject TempHer = ObjectListScript.InstantiateWithCounter(enemyHer, tempForEnemy);
                TempHer.transform.right = new Vector3(0, 0, 0) - TempHer.transform.position;
                TempHer.GetComponent<Rigidbody2D>().AddForce(powerDirectForEnemy * 200);
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - instantiate passive or predator fish
        else if (WhoIsSpaming == 2)
        {
            if (countBeforePlayerInEnom >= BalanceInstrument.RandomEventDifficult)
            {
                countBeforePlayerInEnom = countBeforePlayerInEnom - BalanceInstrument.RandomEventDifficult;
                //=========================================================== Создание рыбы игрока
                GameObject TempHer = ObjectListScript.InstantiateWithCounter(playerHer, tempForEnemy);
                TempHer.transform.right = TempHer.transform.position - new Vector3(0, 0, 0);
                TempHer.GetComponent<Rigidbody2D>().AddForce(powerDirectForEnemy * 5);
                //=========================================================== Создание рыбы игрока
            }
            else
            {
                countBeforePlayerInEnom = countBeforePlayerInEnom + 1f;
                //=========================================================== Создание рыбы противника
                GameObject TempHer = ObjectListScript.InstantiateWithCounter(enemyHer, tempForEnemy);
                TempHer.transform.right = TempHer.transform.position - new Vector3(0, 0, 0);
                TempHer.GetComponent<Rigidbody2D>().AddForce(powerDirectForEnemy * 5);
                //=========================================================== Создание рыбы противника
            }
        }
    }
    public void CreateEnemyInFeeder(GameObject objectToGo, string side)//creats setted enemy in up or down feeder
    {
        if (side == "up")
        {
            tempForEnemy = new Vector2(feederUp.transform.position.x +
                Random.Range(-feederUp.transform.localScale.x / 3, feederUp.transform.localScale.x / 3),
                feederUp.transform.position.y);
        }
        else
        {
            tempForEnemy = new Vector2(feederDown.transform.position.x +
                Random.Range(-feederDown.transform.localScale.x / 3, feederDown.transform.localScale.x / 3),
                feederDown.transform.position.y);
        }
        Vector2 TempVector = new Vector2(0, 0) - tempForEnemy;
        float RandomAngle = Random.Range(-0.5f, 0.5f);
        powerDirectForEnemy = (
            new Vector2(TempVector.x * Mathf.Cos(RandomAngle) - TempVector.y * Mathf.Sin(RandomAngle),
            TempVector.x * Mathf.Sin(RandomAngle) + TempVector.y * Mathf.Cos(RandomAngle))
            );
        GameObject TempHer = ObjectListScript.InstantiateWithCounter(objectToGo, tempForEnemy);
        TempHer.transform.right = new Vector3(0, 0, 0) - TempHer.transform.position;
        TempHer.GetComponent<Rigidbody2D>().AddForce(powerDirectForEnemy * 50);
    }
    IEnumerator StartForThirdMode()//creates enemy and player's fishes for third mode
    {
        while (true)
        {
            yield return new WaitForSeconds(BalanceInstrument.EnemyDifficult);//always waits for 2 seconds
            CreateEnemy(2);
        }
    }
}
