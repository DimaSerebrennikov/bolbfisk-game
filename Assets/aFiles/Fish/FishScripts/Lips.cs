using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lips : MonoBehaviour
{
    public GameObject targeter;
    Vector3 lastPosition;
    Vector3 lastPositionForCG;
    Vector2 TempBubblePosition;
    private void Awake()
    {
        lastPosition = transform.position;
    }
    private void FixedUpdate()
    {
        lastPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHer" || collision.gameObject.tag == "EnemyPredator")
        {
            TempBubblePosition = transform.position;
            BubbleSpammer.CreateBubble(TempBubblePosition, 20);
            lastPositionForCG = collision.gameObject.GetComponent<LastPositionScript>().lastPostion;

            targeter.transform.right = Bposition(collision.gameObject.transform.position, lastPositionForCG)
                - Bposition(transform.position, lastPosition);

            if (BetweenAngles(targeter.transform.eulerAngles.z, transform.eulerAngles.z))
            {
                if (collision.gameObject.transform.Find("EnemyLips") != null)
                {
                    Transform enemyPredatorLips = collision.gameObject.transform.Find("EnemyLips");//губы противника
                    Transform playerGameObject = gameObject.transform.parent;//тело рыбки игрока
                    targeter.transform.right = playerGameObject.position - enemyPredatorLips.transform.position;
                    if (BetweenAngles(targeter.transform.eulerAngles.z, enemyPredatorLips.eulerAngles.z))
                    {
                        ObjectListScript.DestroyWithCounter(collision.gameObject);
                        ObjectListScript.DestroyWithCounter(playerGameObject.gameObject);
                    }
                    else
                    {
                        ObjectListScript.DestroyWithCounter(collision.gameObject);
                    }
                }
                else
                {
                    ObjectListScript.DestroyWithCounter(collision.gameObject);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHer" || collision.gameObject.tag == "EnemyPredator")
        {
            TempBubblePosition = transform.position;
            lastPositionForCG = collision.gameObject.GetComponent<LastPositionScript>().lastPostion;

            targeter.transform.right = Bposition(collision.gameObject.transform.position, lastPositionForCG)
                - Bposition(transform.position, lastPosition);

            if (BetweenAngles(targeter.transform.eulerAngles.z, transform.eulerAngles.z))
            {
                if (collision.gameObject.transform.Find("EnemyLips") != null)
                {
                    Transform enemyPredatorLips = collision.gameObject.transform.Find("EnemyLips");//губы противника
                    Transform playerGameObject = gameObject.transform.parent;//тело рыбки игрока
                    targeter.transform.right = playerGameObject.position - enemyPredatorLips.transform.position;
                    if (BetweenAngles(targeter.transform.eulerAngles.z, enemyPredatorLips.eulerAngles.z))
                    {
                        ObjectListScript.DestroyWithCounter(collision.gameObject);
                        ObjectListScript.DestroyWithCounter(playerGameObject.gameObject);
                    }
                    else
                    {
                        ObjectListScript.DestroyWithCounter(collision.gameObject);
                    }
                }
                else
                {
                    ObjectListScript.DestroyWithCounter(collision.gameObject);
                }
            }
        }
    }
    Vector3 Bposition(Vector3 cp, Vector3 lp)
    {
        return (cp + lp) / 2f;
    }
    bool BetweenAngles(float a, float b)//angle between 2 angles
    {
        float c = Mathf.Abs(a - b);
        float d = 360 - Mathf.Abs(a - b);
        if (Mathf.Min(c, d) < 45)
        { 
            return true;
        } 
        else
        {
            return false;
        }
    }
}
