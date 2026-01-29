using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerPredator : MonoBehaviour
{
    public GameObject enemyPredator;
    public Sprite[] MyAnimationSprites;//0 - открытый рот, 120 - закрытый
    public Sprite[] AnimationForFinSprites;//0 ...14... 29
    public GameObject TargeterForFin;
    public SpriteRenderer Fin;
    public Sprite[] ChargeSprites;
    public void ZARAZA()
    {
        var a = gameObject.transform.rotation;
        GameObject b = ObjectListScript.InstantiateWithCounter(enemyPredator, gameObject.transform.position);
        b.transform.rotation = a;
        ObjectListScript.DestroyWithCounter(gameObject);
    }
}
