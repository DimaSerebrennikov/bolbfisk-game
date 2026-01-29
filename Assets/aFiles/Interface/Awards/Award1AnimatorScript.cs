using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Award1AnimatorScript : MonoBehaviour
{
    public Sprite[] sprites;
    Image image;
    int spriteCount;
    float TBF; //time between frames
    bool startAnimation;
    public AwardsScript awardScript;
    public AudioSource hitOnLock;
    public AudioSource openLock;
    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        spriteCount = 0;
        TBF = 0;
        startAnimation = false;
    }
    private void OnEnable()
    {
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
            if (spriteCount == 86 && (gameObject.name == "Award1"
                || gameObject.name == "Premium"))
            {
                hitOnLock.Play();
            }
            else if (spriteCount == 142 && gameObject.name == "Award2")
            {
                hitOnLock.Play();
            }
            else if (spriteCount == 142 && (gameObject.name == "Award1"
                || gameObject.name == "Premium"))
            {
                openLock.Play();
            }
            else if (spriteCount == 166 && gameObject.name == "Award2")
            {
                openLock.Play();
            }
            spriteCount++;
            TBF = 0f;
        }
        if (spriteCount >= sprites.Length)
        {
            awardScript.AnimationFinished();
        }
    }
}