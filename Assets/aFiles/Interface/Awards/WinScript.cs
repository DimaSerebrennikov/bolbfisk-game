using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public Sprite[] sprites;
    Image image;
    int spriteCount;
    float TBF; //time between frames
    bool startAnimation;
    public AwardsScript awardScript;
    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        spriteCount = 0;
        TBF = 0;
        startAnimation = false;
    }
    private void OnEnable()
    {
        spriteCount = 0;
        startAnimation = true;
    }
    private void Update()
    {
        TBF += Time.unscaledDeltaTime;
        if (spriteCount >= 0
            && spriteCount < sprites.Length
            && sprites.Length > 0
            && TBF > 0.016
            && startAnimation
            )
        {
            image.sprite = sprites[spriteCount];
            spriteCount++;
            TBF = 0f;
        }
        if (spriteCount >= sprites.Length)
        {
            spriteCount = 290;
        }
    }
}