using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InternetCheckScript : MonoBehaviour
{
    public MenuCanvas menuCanvas;
    public GameObject banner;
    float targetTime;
    float time;
    bool once;
    public bool internetCheckOn;
    public bool afterLogo;
    public MyLogoScript myLogoScript;
    private void Awake()
    {
        internetCheckOn = false;
        once = false;
        targetTime = 0.016f;
        time = -1f;
        if (Application.internetReachability != NetworkReachability.NotReachable
            || PremiumActivatorScript.ActivationStatus)
        {
            Destroy(gameObject);
        }
        else
        {
            myLogoScript.onDestroy = () => { Time.timeScale = 0f; };
            menuCanvas.ClickOnMenuButton();
            internetCheckOn = true;
            Restart.StartScreen = false;
        }
    }
    private void Update()
    {
        if (once && myLogoScript == null)
        {
            time += Time.unscaledDeltaTime;
            for (int a = 0; time > targetTime; a++)
            {
                time = time - targetTime;
                banner.transform.Rotate(0f, 0.5f, 0f);
            }
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                Destroy(gameObject);
                menuCanvas.ClickOnMenuButton();
            }
        }
        once = true;
    }
}