using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    float duration;
    float power;
    public AnimationCurve curve;
    static public bool allowShaking;
    private void Start()
    {
        duration = 1.5f;
        allowShaking = false;
    }
    public void StartShaking()
    {
        StartCoroutine(Shaking());
    }
    IEnumerator Shaking() 
    {
        
        Vector3 startPosition = transform.position;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float strength = curve.Evaluate(timer / duration);
            if (strength != 0 )  strength *= 0.65f; ;
            transform.rotation = Quaternion.Euler(0, 0, strength * Random.Range(0,10));
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
