using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FishingRod : MonoBehaviour
{
    [SerializeField] GameObject spoiledFood;
    [SerializeField] Transform topFeeder;
    float myTimer;
    const int BRCoef = 1; //Between rendoms coeficient
    private void Start()
    {
        myTimer = 0;
    }
    void InstantiateRod()//create spoiled food
    {
        Vector2 startPosition = new Vector2(Random.Range((-topFeeder.localScale.x / 2) + 1,
    (topFeeder.localScale.x / 2) - 1), topFeeder.position.y);
        ObjectListScript.InstantiateWithCounter(spoiledFood, startPosition);
    }
    private void FixedUpdate()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - chance create spoiled fish, 120s = 100%, if bottle exists then 60s
        myTimer += Time.fixedDeltaTime;
        if (Timer.TimerCanStart && myTimer >= 10)
        {
            myTimer = 0;
            int a = Random.Range(0, (int)(
                (BalanceInstrument.RandomEventDifficult * BRCoef) / (10)
                ));
            if (a == 0)
            {
                InstantiateRod();
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - chance create spoiled fish, 120s = 100%, if bottle exists then 60s
    }
}