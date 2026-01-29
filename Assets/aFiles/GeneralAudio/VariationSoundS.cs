using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariationSoundS : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioSource aS;
    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            aS.clip = clip1;
        }
        else if (rand == 1)
        {
            aS.clip = clip2;
        }
        else
        {
            aS.clip = clip3;
        }
        aS.Play();
    }
}
