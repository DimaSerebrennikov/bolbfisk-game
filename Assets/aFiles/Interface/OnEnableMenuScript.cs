using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEnableMenuScript : MonoBehaviour
{
    public Sprite modeSprite;
    public Sprite modeSpriteShow;
    public Image modeImage;
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(SS.modeShine))
        {
            modeImage.sprite = modeSpriteShow;
        }
        else
        {
            modeImage.sprite = modeSprite;
        }
    }
}