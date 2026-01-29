using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeatScript : MonoBehaviour
{
    public Sprite[] dieAnimationSprites;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHer"
            || collision.gameObject.tag == "PlayerHer"
            || collision.gameObject.tag == "FoodTag")
        {
            StartCoroutine(DieAnimation());
            ObjectListScript.DestroyWithCounter(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            StartCoroutine(DieAnimation());
        }
    }
    IEnumerator DieAnimation()
    {
        rb.velocity = Vector3.zero;
        for (int a = 0; a < dieAnimationSprites.Length; a++)
        {
            spriteRenderer.sprite = dieAnimationSprites[a];
            yield return new WaitForSeconds(0.01667f);
        }
        gameObject.SetActive(false);
    }
}