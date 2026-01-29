using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieScript : MonoBehaviour//висит на таймере
{
    public TextMeshProUGUI timerText;
    float timer;
    private void Awake()
    {
        timer = 12f;
    }
    private void Update()
    {
        if (Timer.TimerCanStart)
        {
            int a = (int)Math.Ceiling(timer);
            timerText.SetText(a.ToString());
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                int roundedDiff = (int)Math.Round(BalanceInstrument.EnemyDifficult);
                for (int b = 0; b < roundedDiff; b++)
                {
                    if (ObjectListScript.playerBrutalList.Count > 0
                        && ObjectListScript.playerBrutalList.Count > (float)ObjectListScript.playerPassiveList.Count * 1.17f)
                    {
                        ObjectListScript.playerBrutalList[0].GetComponent<PlayerPredator>().ZARAZA();
                    }
                    else if (ObjectListScript.playerPassiveList.Count > 0)
                    {
                        int lowestLevel = 9999;
                        int indexLowestLevel = 0;
                        for (int c = 0; c < ObjectListScript.playerPassiveList.Count; c++)
                        {
                            int nowLevel = ObjectListScript.playerPassiveList[c].GetComponent<PassiveFishScript>().level;
                            if (nowLevel < lowestLevel)
                            {
                                lowestLevel = nowLevel;
                                indexLowestLevel = c;
                            }
                        }
                        ObjectListScript.playerPassiveList[indexLowestLevel].GetComponent<PlayerHer>().ZARAZA();
                    }
                }
                timer = 12f;
            }
        }
    }
}
