using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour//all fishes have this tag
{
    float TimerCollision;
    private void Awake()
    {
        TimerCollision = 0f;
    }
    private void Update()
    {
        TimerCollision += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - creates bubbles permanently
        if (TimerCollision > 2f)
        {
            TimerCollision = 0f;
            if (collision.gameObject.tag == "Wall" ||
                    collision.gameObject.tag == "PlayerHer" ||
                    collision.gameObject.tag == "EnemyHer" ||
                    collision.gameObject.tag == "PlayerPredator" ||
                    collision.gameObject.tag == "EnemyPredator")
            {
                Vector2 CollisionPoint = collision.GetContact(0).point;
                BubbleSpammer.CreateBubble(CollisionPoint, 3);
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - creates bubbles permanently
    }
}
