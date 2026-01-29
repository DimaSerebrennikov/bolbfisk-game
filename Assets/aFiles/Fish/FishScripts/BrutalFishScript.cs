using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BrutalFishScript : MonoBehaviour
{
    GameObject target;
    public Transform targetDirection;
    float targetDirectionDegree;
    Rigidbody2D rb;
    GameObject[] fishList;
    public float closestDist;
    public float timerForDiving;
    int NumberAnimationForFinSprites;
    int NowSprite;
    SpriteRenderer GameObjectSpriteRenderer;
    float TimerForFinAnimation;
    int DistCalibration;
    float ChargeSpritesTimer;
    int NumberChargeSprites;
    bool OnceAfterDiving;
    bool OnceDuringDiving;
    bool IWantBoop;
    public bool isJustSpammed;
    List<GameObject> whoPredatorWant;
    List<GameObject> whoPredatorWantNext;
    public AudioBundleS audioScript;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - write from player or enemy predator
    Sprite[] MyAnimationSprites;//0 - открытый рот, 119 - закрытый
    Sprite[] AnimationForFinSprites;//0 ...14... 29
    GameObject TargeterForFin;
    SpriteRenderer Fin;
    Sprite[] ChargeSprites;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - write from player or enemy predator
    void Awake()
    {
        audioScript = FindAnyObjectByType<AudioBundleS>();
        rb = GetComponent<Rigidbody2D>();
        GameObjectSpriteRenderer = GetComponent<SpriteRenderer>();
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - init player predator data
        if (gameObject.GetComponent<PlayerPredator>() != null)
        {
            PlayerPredator playerPredator = gameObject.GetComponent<PlayerPredator>();
            MyAnimationSprites = playerPredator.MyAnimationSprites;
            Array.Reverse(MyAnimationSprites);
            AnimationForFinSprites = playerPredator.AnimationForFinSprites;
            TargeterForFin = playerPredator.TargeterForFin;
            Fin = playerPredator.Fin;
            ChargeSprites = playerPredator.ChargeSprites;
            whoPredatorWant = ObjectListScript.enemyPassiveList;
            whoPredatorWantNext = ObjectListScript.enemyBrutalList;
            ObjectUpdate(false);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - init player predator data
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - init enemy predator data
        else
        {
            EnemyPredator enemyPredator = gameObject.GetComponent<EnemyPredator>();
            MyAnimationSprites = enemyPredator.MyAnimationSprites;
            Array.Reverse(MyAnimationSprites);
            AnimationForFinSprites = enemyPredator.AnimationForFinSprites;
            TargeterForFin = enemyPredator.TargeterForFin;
            Fin = enemyPredator.Fin;
            ChargeSprites = enemyPredator.ChargeSprites;
            whoPredatorWant = ObjectListScript.playerPassiveList;
            whoPredatorWantNext = ObjectListScript.playerBrutalList;
            ObjectUpdate(true);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - init enemy predator data
    }
    public void ObjectUpdate//обновл€ет объект без всего лишнего
    (bool IJS)//is just spammed
    {
        isJustSpammed = IJS;
        if (IJS)
        {
            StartCoroutine(JustSpammed());
        }
        IWantBoop = false;
        OnceDuringDiving = true;
        OnceAfterDiving = false;
        NumberChargeSprites = 0;
        ChargeSpritesTimer = 0f;
        TimerForFinAnimation = 0;
        NumberAnimationForFinSprites = 14;
        DistCalibration = 0;
        NowSprite = 119;
        timerForDiving = 0;
        closestDist = Mathf.Infinity;
        Fin.color = new Color(1, 1, 1, 1);
        GameObjectSpriteRenderer.sprite = MyAnimationSprites[NowSprite];
        Fin.sprite = AnimationForFinSprites[NumberAnimationForFinSprites];
    }
    private void FixedUpdate()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - looking for target and rotating
        //----------------------------------------------------------- find target
        closestDist = Mathf.Infinity;
        fishList = whoPredatorWant.ToArray();
        if (fishList.Length == 0)
        {
            fishList = whoPredatorWantNext.ToArray();
        }
        foreach (GameObject foodiy in fishList)
        {
            float a = Vector2.Distance(foodiy.transform.position, transform.position);
            if (a < closestDist)
            {
                target = foodiy;
                closestDist = a;
            }
        }

        fishList = null;
        //----------------------------------------------------------- find target
        //----------------------------------------------------------- rotates to target
        if (target != null && target.activeSelf && !isJustSpammed)
        {
            targetDirection.right = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            targetDirectionDegree = targetDirection.rotation.eulerAngles.z;
            rb.angularVelocity = 5000f * Time.fixedDeltaTime * CDSaScript.ChooseDirectionAndStabilization(transform.rotation.eulerAngles.z, targetDirectionDegree);
            //rb.transform.right = rb.transform.right + (target.transform.position - rb.transform.position) * 0.6f * Time.fixedDeltaTime;
        }
        //----------------------------------------------------------- rotates to target
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - looking for target and hunting
    }
    private void Update()
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - animation
        if (timerForDiving <= 0f)
        {
            TimerForFinAnimation += Time.deltaTime;
            if (target != null && target.activeSelf)
            {
                //----------------------------------------------------------- rotating system for fin, when fish needs rotate, fin has specific animation
                while (TimerForFinAnimation >= 0.05f)
                {
                    TimerForFinAnimation -= 0.05f;
                    //=========================================================== system which gives rotary direction
                    TargeterForFin.transform.right = target.transform.position - transform.position;
                    float AngleToTarget = TargeterForFin.transform.eulerAngles.z;
                    float FishAngle = transform.eulerAngles.z;
                    float OneWay = Mathf.Abs(AngleToTarget - FishAngle);
                    float AnotherWay = 360 - Mathf.Abs(AngleToTarget - FishAngle);
                    bool EFin = false;
                    if (AngleToTarget < FishAngle)
                    {
                        EFin = AnotherWay > OneWay;// true = clockwise
                    }
                    else
                    {
                        EFin = OneWay > AnotherWay;
                    }
                    //=========================================================== system which gives rotary direction
                    //=========================================================== if angle less then 9 in any way, its normolizing
                    if (Mathf.Min(OneWay, AnotherWay) < 9)
                    {
                        if (NumberAnimationForFinSprites > 14)
                        {
                            NumberAnimationForFinSprites--;
                        }
                        else if (NumberAnimationForFinSprites < 14)
                        {
                            NumberAnimationForFinSprites++;
                        }
                    }
                    //=========================================================== if angle less than 9 in any way, its normolizing
                    //=========================================================== rotates in the certain direction
                    else if (EFin)
                    {
                        if (NumberAnimationForFinSprites <= 28)//0 - fin in up
                        {
                            NumberAnimationForFinSprites++; //clockwise
                        }
                    }
                    else
                    {
                        if (NumberAnimationForFinSprites >= 1) //29 - fin in down
                        {
                            NumberAnimationForFinSprites--;//counter clockwise
                        }
                    }
                    //=========================================================== rotates in the certain direction
                    Fin.sprite = AnimationForFinSprites[NumberAnimationForFinSprites];//set sprite;
                }
                //----------------------------------------------------------- rotating system for fin, when fish needs rotate, fin has specific animation
                //----------------------------------------------------------- the greater the distance, the more closed the mouth
                DistCalibration = (int)(Vector2.Distance(target.transform.position, transform.position) * 10f) - 5;//distance -5 means increasing minimum distance, so
                //max opened mounth will be on distance 5, this number not in vector global scale, *10 means every 0.1 = 1 frame of animation, it's animation sprites value
                //=========================================================== the bigger distance the more closed mouth
                if (DistCalibration > NowSprite && NowSprite <= 118)
                {
                    NowSprite++;
                    GameObjectSpriteRenderer.sprite = MyAnimationSprites[NowSprite];
                }
                //=========================================================== the bigger distance the more closed mouth
                //=========================================================== and reverse
                else if (DistCalibration < NowSprite && NowSprite >= 1)
                {
                    NowSprite--;
                    GameObjectSpriteRenderer.sprite = MyAnimationSprites[NowSprite];
                }
                //=========================================================== and reverse
            }
            //=========================================================== no target: close mouth and normolize fin
            else
            {
                if (NowSprite <= 118)
                {

                    NowSprite++;
                    GameObjectSpriteRenderer.sprite = MyAnimationSprites[NowSprite];
                }

                while (TimerForFinAnimation >= 0.05f)
                {
                    TimerForFinAnimation -= 0.05f;
                    if (NumberAnimationForFinSprites > 14)
                    {
                        NumberAnimationForFinSprites--;
                    }
                    else if (NumberAnimationForFinSprites < 14)
                    {
                        NumberAnimationForFinSprites++;
                    }
                    Fin.sprite = AnimationForFinSprites[NumberAnimationForFinSprites];
                }
                //=========================================================== no target: close mouth and normolize fin
                //----------------------------------------------------------- the greater the distance, the more closed the mouth
            }
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - animation
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - predator doing diving things: change sprites, sound, looking at
        if (timerForDiving > 0)
        {
            ChargeSpritesTimer += Time.deltaTime;
            if (ChargeSpritesTimer >= 0.01666f && NumberChargeSprites <= 57)
            {
                ChargeSpritesTimer = 0f;
                GameObjectSpriteRenderer.sprite = ChargeSprites[NumberChargeSprites];
                NumberChargeSprites++;
                //----------------------------------------------------------- once in whole diving
                if (OnceDuringDiving)
                {
                    OnceDuringDiving = false;
                    Fin.color = new Color(1, 1, 1, 0);
                    IWantBoop = true;
                }
                //----------------------------------------------------------- once in whole diving
            }
            OnceAfterDiving = true;
            timerForDiving -= Time.deltaTime;
            transform.rotation = Swipe.TempForRotation;//в дайвинге смотреть в одну сторону
        }
        else
        {
            //----------------------------------------------------------- finish method in diving return everything in previous state
            if (OnceAfterDiving)
            {
                Fin.color = new Color(1, 1, 1, 1);
                OnceDuringDiving = true;
                OnceAfterDiving = false;
                NumberAnimationForFinSprites = 14;
                NowSprite = 119;
                NumberChargeSprites = 0;
                GameObjectSpriteRenderer.sprite = MyAnimationSprites[NowSprite];
                Fin.sprite = AnimationForFinSprites[NumberAnimationForFinSprites];
            }
            //----------------------------------------------------------- finish method in diving return everything in previous state
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - predator doing diving things: change sprites, sound, looking at
    }
    //float CustomFunction(int x)
    //{
    //    int x1 = x - 1;
    //    return 1 - (1 - Mathf.Pow(e,-0.1f * (float)x1)); // Ёкспоненциальна€ функци€
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - for bubbles
        //----------------------------------------------------------- with minimum speed doing only sound
        if (collision.gameObject.tag == "Wall")
        {
            float velocityForSound = Swipe.SwipePower / 500f;
            if (collision.gameObject.name == "WallRight"
                && rb.velocity.x < -velocityForSound)
            {
                audioScript.PlayHitSound();
            }
            else if (collision.gameObject.name == "WallLeft"
                && rb.velocity.x > velocityForSound)
            {
                audioScript.PlayHitSound();
            }
            else if (collision.gameObject.name == "WallUP"
                && rb.velocity.y < -velocityForSound)
            {
                audioScript.PlayHitSound();
            }
            else if (collision.gameObject.name == "WllDown"
                && rb.velocity.y > velocityForSound)
            {
                audioScript.PlayHitSound();
            }
        }
        //----------------------------------------------------------- with minimum speed doing only sound
        //----------------------------------------------------------- first collision after swipe doing bubbles
        if (IWantBoop)
        {
            if (collision.gameObject.tag == "Wall" ||
                    collision.gameObject.tag == "PlayerHer" ||
                    collision.gameObject.tag == "EnemyHer" ||
                    collision.gameObject.tag == "PlayerPredator" ||
                    collision.gameObject.tag == "EnemyPredator")
            {
                IWantBoop = false;
                Vector2 CollisionPoint = collision.GetContact(0).point;
                BubbleSpammer.CreateBubble(CollisionPoint, 20);
            }
        }
        //----------------------------------------------------------- first collision after swipe doing bubbles
        //----------------------------------------------------------- with minimum speed doing only sound
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - for bubbles
    }
    IEnumerator JustSpammed()//after spamming enemy predator looking at side where was born
    {
        yield return new WaitForSeconds(0.4f);
        isJustSpammed = false;
    }
}
