using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class AnchorEvent : MonoBehaviour
{
    [SerializeField] GameObject Anchor;
    [SerializeField] Transform TopFeeder;
    [SerializeField] Transform BotFeeder;
    [SerializeField] GameObject Warning;
    bool nowDownWarning;
    bool nowDownAnchor;
    float myTimer;
    bool beforeDownWarning;//using at start of warning down
    public CameraShaker cameraShaker;
    const float BRCoef = 2;
    private void Start()
    {
        myTimer = 0f;
        nowDownAnchor = false;
        nowDownWarning = false;
        beforeDownWarning = true;
        TopWarning();
        TopAnchorSetLocation();
    }
    private void FixedUpdate()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - chance for anchor event 200s = 100%
        myTimer += Time.fixedDeltaTime;
        if (Timer.TimerCanStart && myTimer >= 10 && !nowDownWarning)
        {
            myTimer = 0;
            int a = Random.Range(0, (int)(
                (BalanceInstrument.RandomEventDifficult * BRCoef) / 10
                ));
            //=========================================================== Если рыб слишком мало, то якори и проверка на улетевшие
            //=========================================================== Если рыб слишком мало, то якори и проверка на улетевшие
            if (a == 0)
            {
                nowDownWarning = true;
            }
        }
        if (myTimer >= 5
            && Timer.TimerCanStart
            && !nowDownWarning
            && ObjectListScript.enemyBrutalList.Count > (ObjectListScript.playerBrutalList.Count + 1) * 4f) //+1 для того, чтобы если у игрока 0 брутальных, то
            //он всё ещё имеет шанс
        {
            myTimer = 0;
            nowDownWarning = true; //бросить якорь 100% раз в 10 секунд
                                   ////----------------------------------------------------------- Удалить затерявшихся бруталов
                                   //for (int b = 0; b < ObjectListScript.playerBrutalList.Count; b++)
                                   //{
                                   //    GameObject currentGameObject = ObjectListScript.playerBrutalList[b];
                                   //    if (currentGameObject.transform.position.x > (Camera.main.orthographicSize * playerAspectRatio / 2) + 1
                                   //        || currentGameObject.transform.position.x < -((Camera.main.orthographicSize * playerAspectRatio / 2) + 1)
                                   //        || currentGameObject.transform.position.y > (Camera.main.orthographicSize / 2) + 1
                                   //        || currentGameObject.transform.position.y < -((Camera.main.orthographicSize / 2) + 1))
                                   //    {
                                   //        ObjectListScript.DestroyWithCounter(currentGameObject);
                                   //    }
                                   //}
                                   ////----------------------------------------------------------- Удалить затерявшихся бруталов
                                   ////----------------------------------------------------------- Удалить затерявшихся пассивных
                                   //for (int b = 0; b < ObjectListScript.playerPassiveList.Count; b++)
                                   //{
                                   //    GameObject currentGameObject = ObjectListScript.playerPassiveList[b];
                                   //    if (currentGameObject.transform.position.x > (Camera.main.orthographicSize * playerAspectRatio / 2) + 1
                                   //        || currentGameObject.transform.position.x < -((Camera.main.orthographicSize * playerAspectRatio / 2) + 1)
                                   //        || currentGameObject.transform.position.y > (Camera.main.orthographicSize / 2) + 1
                                   //        || currentGameObject.transform.position.y < -((Camera.main.orthographicSize / 2) + 1))
                                   //    {
                                   //        ObjectListScript.DestroyWithCounter(currentGameObject);
                                   //    }
                                   //}
                                   ////----------------------------------------------------------- Удалить затерявшихся пассивных
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - chance for anchor event 200s = 100%
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - start of warning down
        if (nowDownWarning)
        {
            if (beforeDownWarning)
            {
                ObjectScaling();
                beforeDownWarning = false;
            }
            DownWarning();
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - start of warning down
        if (nowDownAnchor)
        {
            DownAnchor();
        }
    }

    void DownWarning()//for fixedupdate, linery drop warning of anchor
    {
        Warning.transform.position = new Vector2(Warning.transform.position.x, Warning.transform.position.y - 14 * Time.fixedDeltaTime);
        if (Warning.transform.position.y <= BotFeeder.position.y + 0.5f)
        {
            TopWarning();
        }
    }
    void TopWarning()//set new location for next warning and let anchor down
    {
        float TempSize = TopFeeder.localScale.x / 2;
        Warning.transform.position = new Vector2(Random.Range(-TempSize * 0.8f, TempSize * 0.8f), 44f);//поставить на верх и зарандомить х.
        Anchor.transform.position = new Vector2(Anchor.transform.position.x, TopFeeder.transform.position.y + 2);
        nowDownAnchor = true;
        nowDownWarning = false;
        beforeDownWarning = true;
    }
    void ObjectScaling()//scales, warning and anchor, because it is must be only the one size for all map sizes
    {
        float tempScale = 0.02142857f * TopFeeder.localScale.x;
        Anchor.transform.localScale = new Vector2(tempScale, tempScale);
        Warning.transform.localScale = new Vector2(tempScale, Warning.transform.localScale.y);// "y" for phone is unchangeing
    }
    public void DownAnchor()//linearly drop down anchor in fixedupdate
    {
        Anchor.transform.position = new Vector2(Anchor.transform.position.x, Anchor.transform.position.y - 28 * Time.fixedDeltaTime);
        if (Anchor.transform.position.y < BotFeeder.position.y - 2)
        {
            TopAnchor();
        }
    }
    void TopAnchor()// after anchor event set location on the warning location and vibrate
    {
        TopAnchorSetLocation();
        TopAnchorVibration();
    }
    void TopAnchorSetLocation()
    {
        Anchor.transform.position = new Vector2(Warning.transform.position.x, 44);
        nowDownAnchor = false;
    }
    void TopAnchorVibration()
    {
        AnchorScript.savedFish = null;
        cameraShaker.StartShaking();
        Vibrator.Vibrate(350);
    }

}
