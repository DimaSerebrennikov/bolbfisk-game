using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHer : MonoBehaviour//all coments in EnemyHer
{
    public GameObject EnemyHer;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - models of fish
    public Sprite LitHer;
    public Sprite MidHer;
    public Sprite BigHer;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - models of fish
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - birth
    public GameObject newHer;
    public GameObject newPred;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - birth
    public void ZARAZA()
    {
        var a = gameObject.transform.rotation;
        GameObject b = ObjectListScript.InstantiateWithCounter(EnemyHer, gameObject.transform.position);
        b.transform.rotation = a;
        PassiveFishScript passiveFishScriptNewEnemy = b.GetComponent<PassiveFishScript>();
        passiveFishScriptNewEnemy.level = gameObject.GetComponent<PassiveFishScript>().level;
        passiveFishScriptNewEnemy.LevelCheck();
        ObjectListScript.DestroyWithCounter(gameObject);
    }
}
