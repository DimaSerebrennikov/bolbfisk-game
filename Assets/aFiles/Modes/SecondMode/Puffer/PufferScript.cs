using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PufferScript : MonoBehaviour
{

    //=========================================================== Стены
    public Transform wallLeft;
    public Transform wallRight;
    public Transform wallUp;
    public Transform wallDown;
    //=========================================================== Стены
    //=========================================================== Основа
    public Transform feederLeft;
    public Transform pufferBodyTransform;
    Rigidbody2D rb;
    Vector2 newPosition;
    //=========================================================== Основа
    //=========================================================== Дополнение
    const float force = 400;
    const float speedRotation = 8f;
    bool fishWasLaunched;
    const float BRCoef = 4f;
    //=========================================================== Дополнение
    private void Awake()
    {
        fishWasLaunched = false;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(LaunchPuffer());
    }
    private void Update()
    {
        pufferBodyTransform.Rotate(new Vector3(0f, 0f, speedRotation) * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerHer"
            || collision.gameObject.tag == "EnemyHer"
            || collision.gameObject.tag == "PlayerPredator"
            || collision.gameObject.tag == "EnemyPredator"
            || collision.gameObject.tag == "FoodTag")
        {
            ObjectListScript.DestroyWithCounter(collision.gameObject);
        }
    }
    IEnumerator LaunchPuffer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);//Тут необходим баланс
            if (!fishWasLaunched)
            {
                int a = Random.Range(0, (int)(
                (BalanceInstrument.RandomEventDifficult * BRCoef) / 10
                ));
                if (a == 0)
                {
                    fishWasLaunched = true;
                    PufferFishSetAndBup();
                }
            }
            else
            {
                if (transform.position.x > wallRight.position.x
                    || transform.position.x < wallLeft.position.x
                    || transform.position.y > wallUp.position.y
                    || transform.position.y < wallDown.position.y)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    public void PufferFishSetAndBup()
    {
        //=========================================================== Установка на место
        newPosition = new Vector2(feederLeft.position.x - 2f,
            feederLeft.position.y +
            Random.Range(-feederLeft.localScale.y / 2, feederLeft.localScale.y / 2));
        Vector2 forceDirection = new Vector2(0, 0) - newPosition;
        //=========================================================== Установка на место
        //=========================================================== Реализация
        transform.position = newPosition;
        rb.AddForce(forceDirection * force);
        //=========================================================== Реализация

    }
}