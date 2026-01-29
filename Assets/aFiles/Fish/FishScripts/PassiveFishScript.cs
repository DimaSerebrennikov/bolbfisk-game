using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassiveFishScript : MonoBehaviour
{
    public Transform targetDirection; //направление к цели
    float targetDirectionDegree;
    GameObject target;
    Rigidbody2D rb;
    float saveDistance;
    public float closestDist;
    public int level;
    bool itsPlayer;
    public AudioBundleS audioBundle;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - models of fish
    Sprite LitHer;
    Sprite MidHer;
    Sprite BigHer;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - models of fish
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - birth
    GameObject newHer;
    GameObject newPred;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - birth
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - fins
    Transform LittleFin;
    Transform MiddleFin;
    Transform BigFin;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - fins
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - lips
    Transform LittleLips;
    Transform MiddleLips;
    Transform BigLips;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - lips
    Action OnLevelUp;//on third level player and enemy fish has different methods
    void Awake()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - in childs finding already created objects
        if (GetComponent<PlayerHer>() != null)
        {
            PlayerHer playerHerScript = gameObject.GetComponent<PlayerHer>();
            LitHer = playerHerScript.LitHer;
            MidHer = playerHerScript.MidHer;
            BigHer = playerHerScript.BigHer;
            newHer = playerHerScript.newHer;
            newPred = playerHerScript.newPred;
            itsPlayer = true;
        }
        else
        {
            EnemyHer enemyHerScript = GetComponent<EnemyHer>();
            LitHer = enemyHerScript.LitHer;
            MidHer = enemyHerScript.MidHer;
            BigHer = enemyHerScript.BigHer;
            newHer = enemyHerScript.newHer;
            newPred = enemyHerScript.newPred;
            itsPlayer = false;
        }
        if (GetActiveSceneScript.currentScene == SS.BigFishMode)
        {
            OnLevelUp = LeveThreeUpThirdMode;
        }
        else if (itsPlayer)
        {
            OnLevelUp = LevelThreeUpPlayer;
        }
        else
        {
            OnLevelUp = LevelThreeUpEnemy;
        }
        LittleFin = transform.Find("PrefabLocationFinLittle");
        MiddleFin = transform.Find("PrefabLocationFinMiddle"); ;
        BigFin = transform.Find("PrefabLocationFinBig"); ;
        LittleLips = transform.Find("LittleLipsPref");
        MiddleLips = transform.Find("MiddleLipsPref");
        BigLips = transform.Find("BigLipsPref");
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - in childs finding already created objects
        audioBundle = FindAnyObjectByType<AudioBundleS>();
        closestDist = Mathf.Infinity;
        rb = GetComponent<Rigidbody2D>();
        ChangeSprite("Little");
    }
    private void FixedUpdate()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - finds nearest food and writes it into "target"
        foreach (GameObject foodiy in ObjectListScript.foodList)
        {
            float a = Vector2.Distance(foodiy.transform.position, transform.position);
            if (a < closestDist)
            {
                target = foodiy;
                closestDist = a;
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - finds nearest food and writes it into "target"
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if food is real go to that and rotate to that with lerp
        if (target != null && target.activeSelf)
        {
            if (Vector2.Distance(target.transform.position, transform.position) > saveDistance)
            {
                rb.AddForce((target.transform.position - transform.position) * 2f * Time.fixedDeltaTime * 60f);
            }
            else
            {
                rb.AddForce((target.transform.position - transform.position) * 1f * Time.fixedDeltaTime * 60f);
            }
            saveDistance = Vector2.Distance(target.transform.position, transform.position);
            //rotate with lerp
            targetDirection.right = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            targetDirectionDegree = targetDirection.rotation.eulerAngles.z;
            if (rb.angularVelocity < 180f
                && rb.angularVelocity > -180f)
            {
                rb.angularVelocity = 10000f * Time.fixedDeltaTime *
                    CDSaScript.ChooseDirectionAndStabilization(transform.rotation.eulerAngles.z, targetDirectionDegree);
            }
            //rb.transform.right = Vector3.Lerp(rb.transform.right, (target.transform.position - rb.transform.position), 1.2f * Time.fixedDeltaTime);
        }
        else
        {
            closestDist = Mathf.Infinity;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - if food is real go to that and rotate to that with lerp
    }
    //void Update()
    //{
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - levelling
    //if (level >= 3)
    //{
    //    OnLevelUp();
    //}
    //else if (level > lastLevel)
    //{
    //    if (level == 1)
    //    {
    //        ChangeSprite("Middle");
    //    }
    //    if (level == 2)
    //    {
    //        ChangeSprite("Big");
    //    }
    //    closestDist = Mathf.Infinity;
    //    lastLevel = level;
    //}
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - levelling
    //}
    public void ChangeSprite//меняет рыбе внешний вид всех частей тела
        (string fish)
    {
        if (fish == "Little")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = LitHer;

            MiddleFin.gameObject.SetActive(false);
            BigFin.gameObject.SetActive(false);
            LittleFin.gameObject.SetActive(true);

            LittleLips.gameObject.SetActive(true);
            MiddleLips.gameObject.SetActive(false);
            BigLips.gameObject.SetActive(false);

            gameObject.transform.localScale = new Vector2(0.3f, 0.3f);
            rb.mass = 2f;
        }
        else if (fish == "Middle")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = MidHer;

            MiddleFin.gameObject.SetActive(true);
            BigFin.gameObject.SetActive(false);
            LittleFin.gameObject.SetActive(false);

            LittleLips.gameObject.SetActive(false);
            MiddleLips.gameObject.SetActive(true); ;
            BigLips.gameObject.SetActive(false);

            gameObject.transform.localScale = new Vector2(0.39f, 0.39f);
            rb.mass = 3f;
        }
        else if (fish == "Big")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = BigHer;

            MiddleFin.gameObject.SetActive(false);
            BigFin.gameObject.SetActive(true);
            LittleFin.gameObject.SetActive(false);

            LittleLips.gameObject.SetActive(false);
            MiddleLips.gameObject.SetActive(false); ;
            BigLips.gameObject.SetActive(true);

            gameObject.transform.localScale = new Vector2(0.507f, 0.507f);
            rb.mass = 4.5f;
        }
    }
    void LevelThreeUpPlayer//каждый второй раз создаёт брутальную рыбу
        ()
    {
        LevelThreeUpCommon();
        GameObject SecondObject = ObjectListScript.InstantiateWithCounter(newHer, transform.position);
        SecondObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 1f) * 100f);
        SecondObject.transform.right = new Vector2(SecondObject.transform.position.x - 10, SecondObject.transform.position.y + 10);
        ArenaScript.countBirthPredator++;
        if (ArenaScript.countBirthPredator >= 2)
        {
            ArenaScript.countBirthPredator = 0;
            GameObject FirstObject = ObjectListScript.InstantiateWithCounter(newPred, transform.position);
            FirstObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 1f) * 50f);
            FirstObject.transform.right = new Vector2(FirstObject.transform.position.x + 10, FirstObject.transform.position.y + 10);
        }
    }
    void LevelThreeUpEnemy//создаёт рыбки противника, 250 на толчок брутальной, потому что она легче
        ()
    {
        LevelThreeUpCommon();
        GameObject FirstObject = ObjectListScript.InstantiateWithCounter(newPred, transform.position);
        GameObject SecondObject = ObjectListScript.InstantiateWithCounter(newHer, transform.position);
        FirstObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 1f) * 50f);
        SecondObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 1f) * 100f);
    }
    void LeveThreeUpThirdMode//для 3-го режима не создаёт брутальную рыбу
        ()
    {
        LevelThreeUpCommon();
        GameObject FirstObject;
        GameObject SecondObject;
        if (newHer.GetComponent<PlayerHer>() != null)
        {
            FirstObject = ObjectListScript.InstantiateWithCounter(newHer, transform.position);
            SecondObject = ObjectListScript.InstantiateWithCounter(newHer, transform.position);
        }
        else
        {
            FirstObject = ObjectListScript.InstantiateWithCounter(newHer, transform.position);
            SecondObject = ObjectListScript.InstantiateWithCounter(newHer, transform.position);
        }
        FirstObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 1f) * 50f);
        SecondObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 1f) * 100f);
    }
    void LevelThreeUpCommon//общее поведение для всех рождений
        ()
    {
        
        Vector2 TempBubblePosition = transform.position;
        BubbleSpammer.CreateBubble(TempBubblePosition, 20);
        rb.AddForce(new Vector2(0f, -1f) * 100f);
        FishUpdate();
    }
    public void FishUpdate()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        level = 0;
        ChangeSprite("Little");
    }
    public void FishEated
        ()
    {
        level++;
        LevelCheck();
    }
    public void LevelCheck()
    {
        if (level == 1)
        {
            ChangeSprite("Middle");
            audioBundle.PlayEatSound();
        }
        else if (level == 2)
        {
            ChangeSprite("Big");
            audioBundle.PlayEatSound();
        }
        else if (level >= 3)
        {
            OnLevelUp();
            audioBundle.PlayBirthSound();
        }
    }
}
