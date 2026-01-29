using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioBundleS : MonoBehaviour
{
    //=========================================================== eat
    List<VariationSoundS> eatList;
    public GameObject eatPref;
    int eatCount;
    //=========================================================== eat
    //=========================================================== birth
    List<VariationSoundS> birthList;
    public GameObject birthPref;
    int birthCount;
    //=========================================================== birth
    //=========================================================== hit
    List<VariationSoundS> hitList;
    public GameObject hitPref;
    int hitCount;
    //=========================================================== hit
    //=========================================================== kill
    List<VariationSoundS> killList;
    public GameObject killPref;
    int killCount;
    //=========================================================== kill
    //=========================================================== critKill
    List<VariationSoundS> critKillList;
    public GameObject critKillPref;
    int critKillCount;
    //=========================================================== critKill
    private void Awake()
    {
        eatList = new List<VariationSoundS>();
        hitList = new List<VariationSoundS>();
        birthList = new List<VariationSoundS>();
        killList = new List<VariationSoundS>();
        critKillList = new List<VariationSoundS>();
        for (int i = 0; i < 5; i++)
        {
            eatList.Add(Instantiate(eatPref).GetComponent<VariationSoundS>());
            hitList.Add(Instantiate(hitPref).GetComponent<VariationSoundS>());
            birthList.Add(Instantiate(birthPref).GetComponent<VariationSoundS>());
            killList.Add(Instantiate(killPref).GetComponent<VariationSoundS>());
            critKillList.Add(Instantiate(critKillPref).GetComponent<VariationSoundS>());
        }
    }
    public void PlayEatSound()
    {
        if (eatCount >= eatList.Count)
        {
            eatCount = 0;
        }
        if (!eatList[eatCount].aS.isPlaying)
        {
            eatList[eatCount].PlaySound();
        }
        eatCount++;
    }
    public void PlayHitSound()
    {
        if (hitCount >= hitList.Count)
        {
            hitCount = 0;
        }
        if (!hitList[hitCount].aS.isPlaying)
        {
            hitList[hitCount].PlaySound();
        }
        hitCount++;
    }
    public void PlayBirthSound()
    {
        if (birthCount >= birthList.Count)
        {
            birthCount = 0;
        }
        if (!birthList[birthCount].aS.isPlaying)
        {
            birthList[birthCount].PlaySound();
        }
        birthCount++;
    }
    public void PlayKillSound()
    {
        if (killCount >= killList.Count)
        {
            killCount = 0;
        }
        if (!killList[killCount].aS.isPlaying)
        {
            killList[killCount].PlaySound();
        }
        killCount++;
    }
    public void PlayCritKillSound()
    {
        if (critKillCount >= critKillList.Count)
        {
            critKillCount = 0;
        }
        if (!critKillList[critKillCount].aS.isPlaying)
        {
            critKillList[critKillCount].PlaySound();
        }
        critKillCount++;
    }
}