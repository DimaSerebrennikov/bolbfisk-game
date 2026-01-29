using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class SpoiledFoodScript : MonoBehaviour
{
    [SerializeField] Sprite Food1;
    [SerializeField] Sprite Food2;
    [SerializeField] Sprite Food3;
    [SerializeField] GameObject spoiledFood;
    [SerializeField] GameObject predator;
    FeederScript feederScript;
    Transform topFeeder;
    float myTimerCreatePredatorFish;//delay for spaming predator from bottle
    bool GoUp;//when food eated it's go up
    bool GoDown;//when food instantiated it's go down 
    GameObject fishSaved = null;//save fish to deleted it after moving in up
    private void Awake()
    {
        myTimerCreatePredatorFish = 0;
        feederScript = FindAnyObjectByType<FeederScript>();//need to find script method which is not static
        topFeeder = FindAnyObjectByType<FeederUpScript>().GetComponent<Transform>();//prefab cant have reference from a scence object
        if (topFeeder == null)
        {
            Debug.Log("SOMETHING WRONG");
        }
        GoUp = false;
        GoDown = true;
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - random sprite
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
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - random sprite
    }
    private void FixedUpdate()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - already caught and it's rise fish
        if (GoUp && fishSaved != null && gameObject != null)
        {
            Vector2 tempPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 15 * Time.fixedDeltaTime);
            fishSaved.transform.position = tempPosition;
            gameObject.transform.position = tempPosition;
            if (tempPosition.y > topFeeder.position.y && myTimerCreatePredatorFish == 0)
            {
                GoUp = false;
                ObjectListScript.DestroyWithCounter(fishSaved);
                Destroy(gameObject); //удаляет испорченную еду, не особое удаление
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - already caught and it's rise fish
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - lineary drops spoiled food
        if (GoDown && !GoUp)
        {
            Vector2 tempPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 7.5f * Time.fixedDeltaTime);
            gameObject.transform.position = tempPosition;
            if (tempPosition.y < Random.Range(0, Camera.main.orthographicSize - 1))
            {
                GoDown = false;
            }
        }
        if (myTimerCreatePredatorFish > 0)
        {
            myTimerCreatePredatorFish -= Time.fixedDeltaTime;
            if (myTimerCreatePredatorFish <= 0)
            {
                myTimerCreatePredatorFish = 0;
                feederScript.CreateEnemyInFeeder(predator, "up");
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - lineary drops spoiled food
    }
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - lineary drops spoiled food
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if collision is real fish, catching only a firt fish
        if (collision.tag == "EnemyHer" || collision.tag == "PlayerHer")
        {
            MoveUp(collision.gameObject, gameObject);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if collision is real fish, catching only a firt fish
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if collision is bottle, raises and gives a delay to spam a new fish
        else if (collision.gameObject.GetComponent<BottleScript>() != null)
        {
            MoveUp(collision.gameObject, gameObject);
            CreatePredatorFish();
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if collision is bottle, raises and gives a delay to spam a new fish
    }
    void MoveUp(GameObject fish, GameObject food)//need delete colliders to not find next fish when previous one is raising
    {
        if (fishSaved == null)
        {
            fishSaved = fish;
            fish.GetComponent<Collider2D>().enabled = false;
            food.GetComponent<Collider2D>().enabled = false;
            GoUp = true;
        }
    }
    void CreatePredatorFish()
    {
        myTimerCreatePredatorFish = 0.5f;
    }
}
