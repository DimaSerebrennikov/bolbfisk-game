using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpammer : MonoBehaviour
{
    [SerializeField] GameObject BubbleObject;
    [SerializeField] GameObject BubbleObjectLong;
    [SerializeField] GameObject BubbleObjbectLittle;
    static GameObject BubbleObjectStatic;
    static GameObject BubbleObjectLongStatic;
    static GameObject BubbleObjectLittleStatic;
        private void Start()
    {
        BubbleObjectLittleStatic = BubbleObjbectLittle;
        BubbleObjectStatic = BubbleObject;
        BubbleObjectLongStatic = BubbleObjectLong;
    }
    static public void CreateBubble(Vector2 PointOfBubbling, int countOfBubles)//spam in vector point selected number of bubbles
    {
        for (int a = 0; a < countOfBubles; a++)
        {
            if (ObjectListScript.bubbleList.Count <= 150)
            {
                int RandomOfAmount = Random.Range(0, 4);
                if (RandomOfAmount == 0)
                {
                    GameObject OneBubble = ObjectListScript.InstantiateWithCounter(BubbleObjectLongStatic, PointOfBubbling);
                    OneBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f)));
                }
                else if (RandomOfAmount == 1)
                {
                    GameObject OneBubble = ObjectListScript.InstantiateWithCounter(BubbleObjectStatic, PointOfBubbling);
                    OneBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f)));
                }
                else if (RandomOfAmount == 2)
                {
                    GameObject OneBubble = ObjectListScript.InstantiateWithCounter(BubbleObjectLittleStatic, PointOfBubbling);
                    OneBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f)));
                }
            }
        }
    }
    static public void CreateBubble(Vector2 PointOfBubbling, int countOfBubles, bool isItStrong)//spam in vector point selected number of bubbles
    {
        for (int a = 0; a < countOfBubles; a++)
        {
            if (ObjectListScript.bubbleList.Count <= 150)
            {
                int RandomOfAmount = Random.Range(0, 4);
                if (RandomOfAmount == 0)
                {
                    GameObject OneBubble = ObjectListScript.InstantiateWithCounter(BubbleObjectLongStatic, PointOfBubbling);
                    OneBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f)));
                }
                else if (RandomOfAmount == 1)
                {
                    GameObject OneBubble = ObjectListScript.InstantiateWithCounter(BubbleObjectStatic, PointOfBubbling);
                    OneBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f)));
                }
                else if (RandomOfAmount == 2)
                {
                    GameObject OneBubble = ObjectListScript.InstantiateWithCounter(BubbleObjectLittleStatic, PointOfBubbling);
                    OneBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.075f, 0.075f), Random.Range(-0.075f, 0.075f)));
                }
            }
        }
    }
}
