using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInterface : MonoBehaviour
{
    [SerializeField] AudioClip AudioClip0;
    [SerializeField] AudioClip AudioClip1;
    [SerializeField] AudioClip AudioClip2;
    public AudioSource [] MyAudioSource;
    int turn;
    void Start()
    {
        turn = 0;
    }
    public void PlaySound()
    {
        //=========================================================== Работа с массивом источников звука
        AudioSource tempAudioS = MyAudioSource[turn];
        turn++;
        if (turn >= MyAudioSource.Length)
        {
            turn = 0;
        }
        //=========================================================== Работа с массивом источников звука
        //=========================================================== Случайный из 3
        int a = Random.Range(0, 3);
        if (a == 0)
        {
            tempAudioS.clip = AudioClip0;
        }
        else if (a == 1)
        {
            tempAudioS.clip = AudioClip1;
        }
        else
        {
            tempAudioS.clip = AudioClip2;
        }
        //=========================================================== Случайный из 3
            tempAudioS.Play();
    }
}
