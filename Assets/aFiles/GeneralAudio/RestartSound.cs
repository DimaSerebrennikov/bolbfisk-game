using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartSound : MonoBehaviour
{
    AudioSource MyAudioSource;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
    }
    public void PlayRestartSound()
    {
        MyAudioSource.Play();
    }
}
