using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScript : MonoBehaviour
{
    [SerializeField] GameObject predator;
    static public GameObject savedFish;//not kill our just spammed fish
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if real fish kill it
        if (collision.gameObject.GetComponent<Fish>() != null && collision.gameObject != savedFish)
        {
            BubbleSpammer.CreateBubble(collision.gameObject.transform.position, 20);
            ObjectListScript.DestroyWithCounter(collision.gameObject);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if real fish kill it
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if bottle insts predator
        else if (collision.gameObject.GetComponent<BottleScript>() != null)
        {
            Vector2 tempLocation = collision.transform.position;
            savedFish = ObjectListScript.InstantiateWithCounter(predator, tempLocation);
            savedFish.transform.rotation = Quaternion.Euler(0, 0, -90);
            savedFish.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 75);
            BubbleSpammer.CreateBubble(tempLocation, 40);
            ObjectListScript.DestroyWithCounter(collision.gameObject);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if bottle insts predator
    }
}
