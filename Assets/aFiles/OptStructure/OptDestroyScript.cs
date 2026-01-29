using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptDestroyScript : MonoBehaviour
{
    public static List<GameObject> playerHerList;
    public static List<GameObject> enemyHerList;
    public static List<GameObject> enemyPredatorList;
    public static List<GameObject> playerPredatorList;
    public static List<GameObject> stoneList;
    public static List<GameObject> foodList;
    public static List<GameObject> bubbleList;
    public AudioBundleS audioBundle;
    static AudioBundleS audioBundle_s;
    private void Awake()
    {
        audioBundle_s = audioBundle;
        playerHerList = new List<GameObject>();
        enemyHerList = new List<GameObject>();
        enemyPredatorList = new List<GameObject>();
        playerPredatorList = new List<GameObject>();
        stoneList = new List<GameObject>();
        foodList = new List<GameObject>();
        bubbleList = new List<GameObject>();
    }
    public static void OptDestroy//вместо удаления выключает объект и если он предназначен для сохранения, то сохраняет
        (GameObject currentGameObject)
    {
        currentGameObject.SetActive(false);
        if (currentGameObject.GetComponent<PlayerHer>() != null)
        {
            playerHerList.Add(currentGameObject);
            audioBundle_s.PlayKillSound();
        }
        else if (currentGameObject.GetComponent<EnemyHer>() != null)
        {
            enemyHerList.Add(currentGameObject);
            audioBundle_s.PlayKillSound();
        }
        else if (currentGameObject.GetComponent<EnemyPredator>() != null)
        {
            enemyPredatorList.Add(currentGameObject);
            audioBundle_s.PlayCritKillSound();
        }
        else if (currentGameObject.GetComponent<PlayerPredator>() != null)
        {
            playerPredatorList.Add(currentGameObject);
            audioBundle_s.PlayKillSound();
        }
        else if (currentGameObject.GetComponent<StoneTag>() != null)
        {
            stoneList.Add(currentGameObject);
        }
        else if (currentGameObject.GetComponent<FoodScript>() != null)
        {
            foodList.Add(currentGameObject);
        }
        else if (currentGameObject.GetComponent<Bubble>() != null)
        {
            bubbleList.Add(currentGameObject);
        }
    }
    public static GameObject OptInstantiate//вместо обычного создания экземлпяра находит сохранённый выключенный и подготавливает его
        (GameObject instantiate, Vector2 objectPosition)
    {
        //=========================================================== Пассивная игрока
        if (instantiate.GetComponent<PlayerHer>() != null && playerHerList.Count > 0)
        {
            bool done = false;
            GameObject result = null;
            while (playerHerList.Count > 0 && !done)
            {
                if (playerHerList[0].activeSelf == true)
                {
                    playerHerList.RemoveAt(0);
                    Debug.Log("ПОВТОРЯШКА");
                }
                else
                {
                    done = true;
                    GameObject tempObject = playerHerList[0];
                    playerHerList.RemoveAt(0);
                    PassiveFishScript passiveFishScript = tempObject.GetComponent<PassiveFishScript>();
                    tempObject.SetActive(true);
                    tempObject.transform.position = objectPosition;
                    passiveFishScript.FishUpdate();
                    result = tempObject;
                }
            }
            if (result == null)
            {
                result = Instantiate(instantiate, objectPosition, Quaternion.identity);
            }
            return result;
        }
        //=========================================================== Пассивная игрока
        //=========================================================== Пассивная противника
        else if (instantiate.GetComponent<EnemyHer>() != null && enemyHerList.Count > 0)
        {
            bool done = false;
            GameObject result = null;
            while (enemyHerList.Count > 0 && !done)
            {
                if (enemyHerList[0].activeSelf == true)
                {
                    enemyHerList.RemoveAt(0);
                    Debug.Log("ПОВТОРЯШКА");
                }
                else
                {
                    done = true;
                    GameObject tempObject = enemyHerList[0];
                    enemyHerList.RemoveAt(0);
                    PassiveFishScript passiveFishScript = tempObject.GetComponent<PassiveFishScript>();
                    tempObject.SetActive(true);
                    tempObject.transform.position = objectPosition;
                    passiveFishScript.FishUpdate();
                    result = tempObject;
                }
            }
            if (result == null)
            {
                result = Instantiate(instantiate, objectPosition, Quaternion.identity);
            }
            return result;
        }
        //=========================================================== Пассивная противника
        //=========================================================== Брутальная противника
        else if (instantiate.GetComponent<EnemyPredator>() != null && enemyPredatorList.Count > 0)
        {
            bool done = false;
            GameObject result = null;
            while (enemyPredatorList.Count > 0 && !done)
            {
                if (enemyPredatorList[0].activeSelf == true)
                {
                    enemyPredatorList.RemoveAt(0);
                    Debug.Log("ПОВТОРЯШКА");
                }
                else
                {
                    done = true;
                    GameObject tempObject = enemyPredatorList[0];
                    enemyPredatorList.RemoveAt(0);
                    BrutalFishScript brutalFishScript = tempObject.GetComponent<BrutalFishScript>();
                    tempObject.SetActive(true);
                    tempObject.transform.position = objectPosition;
                    brutalFishScript.ObjectUpdate(true);
                    result = tempObject;
                }
            }
            if (result == null)
            {
                result = Instantiate(instantiate, objectPosition, Quaternion.identity);
            }
            return result;
        }
        //=========================================================== Брутальная противника
        //=========================================================== Брутальная игрока
        else if (instantiate.GetComponent<PlayerPredator>() != null && playerPredatorList.Count > 0)
        {
            bool done = false;
            GameObject result = null;
            while (playerPredatorList.Count > 0 && !done)
            {
                if (playerPredatorList[0].activeSelf == true)
                {
                    playerPredatorList.RemoveAt(0);
                    Debug.Log("ПОВТОРЯШКА");
                }
                else
                {
                    done = true;
                    GameObject tempObject = playerPredatorList[0];
                    playerPredatorList.RemoveAt(0);
                    BrutalFishScript brutalFishScript = tempObject.GetComponent<BrutalFishScript>();
                    tempObject.SetActive(true);
                    tempObject.transform.position = objectPosition;
                    brutalFishScript.ObjectUpdate(false);
                    result = tempObject;
                }
            }
            if (result == null)
            {
                result = Instantiate(instantiate, objectPosition, Quaternion.identity);
            }
            return result;
        }
        //=========================================================== Брутальная игрока
        //=========================================================== Камень
        else if (instantiate.GetComponent<StoneTag>() != null && stoneList.Count > 0)
        {
            GameObject tempObject = stoneList[0];
            stoneList.RemoveAt(0);
            tempObject.SetActive(true);
            tempObject.transform.position = objectPosition;
            return tempObject;
        }
        //=========================================================== Камень
        //=========================================================== Еда
        else if (instantiate.GetComponent<FoodScript>() != null && foodList.Count > 0)
        {
            bool done = false;
            GameObject result = null;
            while (foodList.Count > 0 && !done)
            {
                if (foodList[0].activeSelf == true)
                {
                    foodList.RemoveAt(0);
                    Debug.Log("ПОВТОРЯШКА");
                }
                else
                {
                    done = true;
                    GameObject tempObject = foodList[0];
                    foodList.RemoveAt(0);
                    tempObject.SetActive(true);
                    tempObject.transform.position = objectPosition;
                    result = tempObject;
                }
            }
            if (result == null)
            {
                result = Instantiate(instantiate, objectPosition, Quaternion.identity);
            }
            return result;
        }
        //=========================================================== Еда
        //=========================================================== Бульбошка
        else if (instantiate.GetComponent<Bubble>() != null && bubbleList.Count > 0)
        {
            GameObject tempObject = bubbleList[0];
            bubbleList.RemoveAt(0);
            tempObject.SetActive(true);
            tempObject.transform.position = objectPosition;
            return tempObject;
        }
        //=========================================================== Бульбошка
        else
        {
            return Instantiate(instantiate, objectPosition, Quaternion.identity);
        }
    }
}