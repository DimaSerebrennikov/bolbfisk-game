using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    Vector2 StartPosition;
    Vector2 EndPosition;
    Vector2 Direction;
    public static float SwipePower;
    float AngleForMovingPredator;
    static public Quaternion TempForRotation;
    //bool WeCanRetrySwipe;
    float MinimumDistance;
    bool ICanSwipe;
    GameObject[] AllMyFishesWithSwipe;
    float TimerForSwipe;
    bool ImReadyStart;
    bool ImReadyFinish;
    public bool firstSwipe;
    private void Awake()//before ArenaScript
    {
        ImReadyStart = true;
        ImReadyFinish = true;
        TimerForSwipe = 0f;
        ICanSwipe = true;
        //WeCanRetrySwipe = true;
        MinimumDistance = Mathf.Min(Screen.width,Screen.height) / 10;
        AngleForMovingPredator = 0f;
        SwipePower = 700f;
        firstSwipe = false;
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - swipe input with limits
            if (ICanSwipe)
            {
                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && ImReadyStart)
                {
                    ImReadyStart = false;
                    ImReadyFinish = true;
                    StartPosition = Input.GetTouch(0).position;
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && ImReadyFinish)
                {
                    ImReadyStart = true;
                    ImReadyFinish = false;
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && ImReadyFinish)
                {
                    EndPosition = Input.GetTouch(0).position;
                    if (Vector2.Distance(StartPosition, EndPosition) >= MinimumDistance)
                    {
                        ImReadyFinish = false;
                        SmartFind(ObjectListScript.playerPassiveList);
                        SmartFind(ObjectListScript.playerBrutalList);
                        SmartFind(ObjectListScript.enemyPassiveList);
                        SmartFind(ObjectListScript.enemyBrutalList);
                        
                    }
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ImReadyFinish && !ImReadyStart)
                {
                    ICanSwipe = false;
                    ImReadyStart = true;
                    firstSwipe = true;
                }
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - swipe input with limits
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - delay between limits
        if (TimerForSwipe <= 1f && ICanSwipe == false)
        {
            TimerForSwipe += Time.deltaTime;
        }
        else
        {
            ICanSwipe = true;
            TimerForSwipe = 0f;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - delay between limits
    }
    void SmartFind(List<GameObject> listOfObjects)//find all fishes and use based on tag
    {
        AllMyFishesWithSwipe = listOfObjects.ToArray();
        foreach (var OneGameObject in AllMyFishesWithSwipe)
        {
            Rigidbody2D OneGameObjectRB = OneGameObject.GetComponent<Rigidbody2D>();
            //WeCanRetrySwipe = false;
            Direction = EndPosition - StartPosition;
            Direction = Direction.normalized;
            
            if (OneGameObject.GetComponent<BrutalFishScript>() != null)
            {
                BrutalFishScript enemyPredatorComponent = OneGameObject.GetComponent<BrutalFishScript>();
                if (!enemyPredatorComponent.isJustSpammed)
                {
                    enemyPredatorComponent.timerForDiving = 1f;
                    OneGameObjectRB.AddForce(Direction * SwipePower);
                    AngleForMovingPredator = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                    TempForRotation = Quaternion.AngleAxis(AngleForMovingPredator, Vector3.forward);
                    OneGameObject.transform.rotation = TempForRotation;
                }
            }
            else
            {
                OneGameObjectRB.AddForce(Direction * SwipePower);
            }
        }
    }
}
