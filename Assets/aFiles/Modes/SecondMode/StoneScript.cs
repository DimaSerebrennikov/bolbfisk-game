
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    [SerializeField] GameObject stone;
    [SerializeField] Transform feederUp;
    [SerializeField] Transform feederDown;
    [SerializeField] Transform feederLeft;
    [SerializeField] Transform feederRight;
    bool ImReadyStart;
    bool ImReadyFinish;
    Vector2 StartPosition;
    Vector2 EndPosition;
    public static float MinimumDistance;
    Vector2 Direction;
    Vector2 spamPosition;
    float TimerForMinDist;
    public static float power;


    //before ArenaScript
    private void Awake()
    {
        power = 1500;
        ImReadyStart = true;
        ImReadyFinish = true;
        MinimumDistance = Mathf.Min(Screen.width, Screen.height) / 8;
        StartCoroutine(RemoveLostStones());
    }
    private void Update()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - SWIPE NO LIMITS
        if (Time.timeScale != 0)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && ImReadyStart)
            {
                ImReadyStart = false;
                ImReadyFinish = true;
                StartPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(-1f);
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && ImReadyFinish)
            {
                ImReadyStart = true;
                ImReadyFinish = false;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && ImReadyFinish)
            {
                EndPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
                if (Vector2.Distance(StartPosition, EndPosition) >= MinimumDistance)
                {
                    ImReadyFinish = false;
                    //----------------------------------------------------------- IN REVERSED DIRECTION FIND POSITION  TO ASPIRANT
                    Direction = EndPosition - StartPosition;
                    Direction = Direction.normalized;
                    spamPosition = Direction;
                    float a = 0f;
                    while (spamPosition.x + StartPosition.x < feederRight.position.x &&
                        spamPosition.x + StartPosition.x > feederLeft.position.x &&
                        spamPosition.y + StartPosition.y < feederUp.position.y &&
                        spamPosition.y + StartPosition.y > feederDown.position.y)

                    {
                        a -= 0.1f;
                        spamPosition = new Vector2(Direction.x * a, Direction.y * a);
                        if (a < -100)
                        {
                            Debug.Log("Error");
                            break;
                        }
                    }
                    spamPosition = new Vector2(spamPosition.x + StartPosition.x, spamPosition.y + StartPosition.y);
                    //random rotation for a stone inst
                    GameObject tempStone = ObjectListScript.InstantiateWithCounter(stone, spamPosition);
                    int b = Random.Range(0, 360);
                    tempStone.transform.Rotate(new Vector3(0,0,b));
                    tempStone.GetComponent<Rigidbody2D>().AddForce(Direction * power);
                    //----------------------------------------------------------- IN REVERSED DIRECTION FIND POSITION  TO ASPIRANT
                }
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ImReadyFinish && !ImReadyStart)
            {
                ImReadyStart = true;
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - SWIPE NO LIMITS
    }

    IEnumerator RemoveLostStones()//every some seconds removes all stones farrer than feeders
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(10f);
            GameObject[] allStones = ObjectListScript.stoneList.ToArray();

            for (int a = 0; allStones.Length > a; a++)
            {
                if (allStones[a] != null)
                {
                    if (allStones[a].transform.position.x > feederRight.position.x + 5 ||
                        allStones[a].transform.position.x < feederLeft.position.x - 5 ||
                        allStones[a].transform.position.y > feederUp.position.y + 5 ||
                        allStones[a].transform.position.y < feederDown.position.y - 5)
                    {
                        ObjectListScript.DestroyWithCounter(allStones[a].gameObject); ;
                    }
                }
            }
        }
        
    }
}
