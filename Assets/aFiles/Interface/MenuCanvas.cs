using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject Retry;
    [SerializeField] GameObject Mode;
    [SerializeField] GameObject Sounds;
    [SerializeField] GameObject Premium;
    [SerializeField] GameObject PremiumCanvas;
    [SerializeField] GameObject Menu;
    [SerializeField] Sprite SoundsOn;
    [SerializeField] Sprite SoundsOff;
    [SerializeField] GameObject BackgroundCanvas;
    [SerializeField] GameObject ModesCanvas;
    public Restart restart;
    
    private void Start()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - sound check
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                AudioListener.volume = 1f;
            }
            else
            {
                AudioListener.volume = 0f;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - sound check
    }
    public void ClickOnMenuButton()//button to open main menu
    {
        if (Menu.activeSelf == true)
        {
            Menu.SetActive(false);
            BackgroundCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (PremiumCanvas.activeSelf == true)
        {
            PremiumCanvas.SetActive(false);
            Menu.SetActive(true);
        }
        else if (ModesCanvas.activeSelf == true)
        {
            Menu.SetActive(true);
            ModesCanvas.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                Sounds.GetComponent<Image>().sprite = SoundsOn;
            }
            else
            {
                Sounds.GetComponent<Image>().sprite = SoundsOff;
            }

            Menu.SetActive(true);
            BackgroundCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void ClickOnModes()
    {
        ModesCanvas.SetActive(true);
        Menu.SetActive(false);
    }
    public void ClickOnRetry()
    {
        Time.timeScale = 1f;
        Menu.SetActive(false);
        BackgroundCanvas.SetActive(false);
        restart.OnDeathWithSelfRemove();
    }
    public void ClickOnSounds()
    {
        if (AudioListener.volume != 0f)
        {
            Sounds.GetComponent<Image>().sprite = SoundsOff;
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            Sounds.GetComponent<Image>().sprite = SoundsOn;
            AudioListener.volume = 1f;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
    public void ClickOnPremium()
    {
        PremiumCanvas.SetActive(true);
        Menu.SetActive(false);
    }
}
