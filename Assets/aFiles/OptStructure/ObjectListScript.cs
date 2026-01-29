using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class ObjectListScript : MonoBehaviour
{
    public static List<GameObject> playerPassiveList;
    public static List<GameObject> enemyPassiveList;
    public static List<GameObject> playerBrutalList;
    public static List<GameObject> enemyBrutalList;
    public static List<GameObject> foodList;
    public static List<GameObject> bubbleList;
    public static List<GameObject> stoneList;
    private void Awake()
    {
        playerPassiveList = new List<GameObject>();
        enemyPassiveList = new List<GameObject>();
        playerBrutalList = new List<GameObject>();
        enemyBrutalList= new List<GameObject>();
        foodList = new List<GameObject>();
        bubbleList = new List<GameObject>();
        stoneList = new List<GameObject>();
        //=========================================================== Начальные рыбки тоже записываются в список
        CheckMode();
        //=========================================================== Начальные рыбки тоже записываются в список
    }
    static void CheckMode()
    {
        switch (GetActiveSceneScript.currentScene)
        {
            case "SwipeMode":
                AddPrevious<PlayerHer>();
                AddPrevious<EnemyHer>();
                AddPrevious<PlayerPredator>();
                AddPrevious<EnemyPredator>();
                AddPrevious<FoodScript>();
                AddPrevious<Bubble>();
                break;
            case "StoneMode":
                AddPrevious<PlayerHer>();
                AddPrevious<EnemyHer>();
                AddPrevious<PlayerPredator>();
                AddPrevious<EnemyPredator>();
                AddPrevious<FoodScript>();
                AddPrevious<StoneTag>();
                AddPrevious<Bubble>();
                break;
            case "BigFishMode":
                AddPrevious<PlayerHer>();
                AddPrevious<EnemyHer>();
                AddPrevious<FoodScript>();
                AddPrevious<Bubble>();
                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    }
    static void AddPrevious //заполняет начальными значениями
        <T>() where T : MonoBehaviour
    {
        T[] array = FindObjectsOfType<T>();
        for (int a = 0; a < array.Length; a++)
        {
            GameObject b = array[a].gameObject;
            ChangeListAmount(true, b);
        }
    }
    public static void ChangeListAmount//меняет значение списка с указаным объектом
        (bool addIsTrue, GameObject targetGameObject)
    {
        List<GameObject> mainList = new List<GameObject>();
        //=========================================================== Проверяет нужен ли данный тип для сбора
        if (targetGameObject.GetComponent<PlayerHer>() != null)
        {
            ref var a = ref playerPassiveList;
            mainList = a;
        }
        else if (targetGameObject.GetComponent<EnemyHer>() != null)
        {
            ref var a = ref enemyPassiveList;
            mainList = a;
        }
        else if (targetGameObject.GetComponent<EnemyPredator>() != null)
        {
            ref var a = ref enemyBrutalList;
            mainList = a;
        }
        else if (targetGameObject.GetComponent<PlayerPredator>() != null)
        {
            ref var a = ref playerBrutalList;
            mainList = a;
        }
        else if (targetGameObject.GetComponent<FoodScript>() != null)
        {
            ref var a = ref foodList;
            mainList = a;
        }
        else if (targetGameObject.GetComponent<Bubble>()!= null)
        {
            ref var a = ref bubbleList;
            mainList = a;
        }
        else if (targetGameObject.GetComponent<StoneTag>() != null)
        {
            ref var a = ref stoneList;
            mainList = a;
        }
        //=========================================================== Проверяет нужен ли данный тип для сбора
        //=========================================================== Добавляет или удаляет в существующий список
        if (addIsTrue)
        {
            mainList.Add(targetGameObject); //если тип не собирается, то список будет пустым
        }
        else
        {
            for (int a = 0; a < mainList.Count; a++)
            {
                if (mainList[a] == targetGameObject)
                {
                    mainList.RemoveAt(a);
                }
            }
        }
        //=========================================================== Добавляет или удаляет в существующий список
    }
    public static void DestroyWithCounter(GameObject deleteIt)
    {
        if (deleteIt.activeSelf != false)
        {
            ChangeListAmount(false, deleteIt);
            OptDestroyScript.OptDestroy(deleteIt);
        }
    }
    public static GameObject InstantiateWithCounter//подсчитывает существующие объекты сразу после их создания
        (GameObject instantiate, Vector2 objectPosition)
    {
        GameObject a = OptDestroyScript.OptInstantiate(instantiate, objectPosition);
        ChangeListAmount(true, a);
        return a;
    }
}
//public static void ChangeListAmount//меняет значение списка с указаным объектом
//        (bool addIsTrue, GameObject targetGameObject)
//{
//    if (addIsTrue)
//    {
//        playerPassiveList.Add(targetGameObject);
//    }
//    else
//    {
//        int d = targetGameObject.GetComponent<PlayerHer>().number;
//        for (int a = 0; a < playerPassiveList.Count; a++)
//        {
//            if (playerPassiveList[a].GetComponent<PlayerHer>().number == d)
//            {
//                playerPassiveList.RemoveAt(a);
//            }
//        }
//    }
//}
//public static GameObject InstantiateWithCounter//подсчитывает существующие объекты сразу после их создания
//    (GameObject instantiate, Vector2 objectPosition)
//{
//    GameObject c = OptDestroyScript.OptInstantiate(instantiate, objectPosition);
//    ChangeListAmount(true, c);
//    int e = c.GetComponent<PlayerHer>().number;
//    if (e == 0)
//    {
//        e = numbering;
//        numbering++;
//    }
//    return c;
//}