using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleEventScript : MonoBehaviour
{
    [SerializeField] GameObject bottleFish;
    [SerializeField] FeederScript feederScript;
    float myTimer;
    const float BRCoef = 1;
    void Start()
    {
        myTimer = 0f;
    }
    private void Update()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - create bottle with random. 120 seconds = 100%, if spoiled food exists then 60s
        myTimer += Time.deltaTime;
        if (Timer.TimerCanStart && myTimer >= 10f)
        {
            myTimer = 0;
            int a = Random.Range(0, (int)(
                (BalanceInstrument.RandomEventDifficult * BRCoef) / (10)
                )); //+1 для того, чтобы второе число в функции было равно знаменателю случайности (1 к ЧИСЛО)
            if (a == 0)
            {
                feederScript.CreateEnemyInFeeder(bottleFish, "down");
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - create bottle with random. 120 seconds = 100%, if spoiled food exists then 60s
    }
}
