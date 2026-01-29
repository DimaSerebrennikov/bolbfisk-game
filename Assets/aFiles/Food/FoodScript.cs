using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainTools;

public class FoodScript : MonoBehaviour
{
    [SerializeField] Sprite Food1;
    [SerializeField] Sprite Food2;
    [SerializeField] Sprite Food3;
    bool collisionFlag;
    private void Awake()
    {
        collisionFlag = true;
        StartCoroutine(LeaveWall());
        int RandomFood = Random.Range(0, 3);
        if (RandomFood == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Food1;
        }
        else if (RandomFood == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Food2;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Food3;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)//find passive fish
    {
        if (collisionFlag)
        {
            if (collision.tag == "EnemyHer" || collision.tag == "PlayerHer")
            {
                collisionFlag = false;
                Vector2 TempBubblePosition = transform.position;
                BubbleSpammer.CreateBubble(TempBubblePosition, 10);

                collision.gameObject.GetComponent<PassiveFishScript>().FishEated();
                ObjectListScript.DestroyWithCounter(gameObject);
            }
        }
    }
    private void OnEnable()
    {
        collisionFlag = true;
    }
    IEnumerator LeaveWall()//enable collider after delay
    {
        Transform wallBlocker = gameObject.transform.Find("WallBlocker");
        wallBlocker.GetComponent<CircleCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(1);
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.TerrainTools;

//public class FoodScript : MonoBehaviour
//{
//    bool IamDestroying;
//    GameObject saveFish;
//    [SerializeField] Sprite Food1;
//    [SerializeField] Sprite Food2;
//    [SerializeField] Sprite Food3;
//    private void Awake()
//    {
//        StartCoroutine(LeaveWall());
//        IamDestroying = false;
//        int RandomFood = Random.Range(0, 3);
//        if (RandomFood == 0)
//        {
//            gameObject.GetComponent<SpriteRenderer>().sprite = Food1;
//        }
//        else if (RandomFood == 1)
//        {
//            gameObject.GetComponent<SpriteRenderer>().sprite = Food2;
//        }
//        else
//        {
//            gameObject.GetComponent<SpriteRenderer>().sprite = Food3;
//        }
//    }
//    void Update()
//    {
//        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - coz collider calls twice, so it's in update
//        if (IamDestroying && saveFish != null)
//        {
//            saveFish.gameObject.GetComponent<PassiveFishScript>().FishEated();
//            Destroy(gameObject);
//        }
//        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - coz collider calls twice, so it's in update
//    }
//    private void OnTriggerEnter2D(Collider2D collision)//find passive fish
//    {
//        if (collision.tag == "EnemyHer" || collision.tag == "PlayerHer")
//        {
//            Vector2 TempBubblePosition = transform.position;
//            BubbleSpammer.CreateBubble(TempBubblePosition, 10);
//            EatingSound.IWantEatingSound = true;

//            saveFish = collision.gameObject;
//            IamDestroying = true;
//        }
//    }
//    IEnumerator LeaveWall()//enable collider after delay
//    {
//        Transform wallBlocker = gameObject.transform.Find("WallBlocker");
//        wallBlocker.GetComponent<CircleCollider2D>().isTrigger = false;
//        yield return new WaitForSeconds(1);
//    }