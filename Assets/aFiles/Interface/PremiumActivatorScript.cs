using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PremiumActivatorScript : MonoBehaviour
{
    //=========================================================== Все объекты подлежащие изменению
    public static PremiumActivatorScript inst;
    public GameObject goal1;
    public GameObject goal2;
    public Image buy;
    public Sprite thanks; //красное сердечко, ссылка на страницу игры
    public MyAd ad;
    //=========================================================== Все объекты подлежащие изменению
    private static bool activationStatus; //статус активации

    public static bool ActivationStatus { get => activationStatus;
        set
        {
            activationStatus = value;
            if (value == true)
            {
                Debug.Log("activation status was changed");
                PlayerPrefs.SetInt(SS.premiumReached, 1);
                inst.ReflectDataChanges();
            }
        }
    }

    private void Awake()
    {
        inst = this;
        if (PlayerPrefs.HasKey(SS.premiumReached))
        {
            ActivationStatus = true;
            Destroy(goal1);
            Destroy(goal2);
            buy.sprite = thanks;
        }
        else
        {
            ActivationStatus = false;
        }
    }
    public void ReflectDataChanges()
    {
        if (ActivationStatus)
        {
            PlayerPrefs.SetInt(SS.premiumReached, 1);
            Destroy(goal1);
            Destroy(goal2);
            buy.sprite = thanks;
            if (ad != null)
            {
                ad.DestroyAd();
            }
        }
    }
}