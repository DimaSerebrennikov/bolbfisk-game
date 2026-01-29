using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRotateScript : MonoBehaviour
{
    SpriteRenderer sr;
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        float randomColor = Random.Range(0.7f, 1f);
        sr.color = new Color (randomColor, randomColor, randomColor);
    }
}
