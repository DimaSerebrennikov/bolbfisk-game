using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BalanceInstrument;

public class BalanceInstrument : MonoBehaviour
{
    //=========================================================== Результаты работы баланс тула
    public static float EnemyDifficult;
    public static float FoodDifficult;
    public static float RandomEventDifficult;
    //=========================================================== Результаты работы баланс тула
    //=========================================================== Меню принадлежности

    //=========================================================== Меню принадлежности
    //=========================================================== Объекты классов баланса
    SavedBalance savedBalance = new SavedBalance();
    SavedFood savedFood = new SavedFood();
    SavedRandom savedRandom = new SavedRandom();
    //=========================================================== Объекты классов баланса
    //=========================================================== Для обсчёта количества таймеров в использовании формулы
    int b;
    int d;
    int e;
    //=========================================================== Для обсчёта количества таймеров в использовании формулы
    //=========================================================== Результаты баланса объекты
    SavedBalance swipeSavedBalance = new SavedBalance();
    SavedFood swipeFoodBalance = new SavedFood();
    SavedRandom swipeRandomBalance = new SavedRandom();

    SavedBalance stoneSavedBalance = new SavedBalance();
    SavedFood stoneFoodBalance = new SavedFood();
    SavedRandom stoneRandomBalance = new SavedRandom();

    SavedBalance bigfishSavedBalance = new SavedBalance();
    SavedFood bigfishFoodBalance = new SavedFood();
    SavedRandom bigfishRandomBalance = new SavedRandom();
    //=========================================================== Результаты баланса объекты
    void Awake()
    {
        //=========================================================== Результаты баланса
        //----------------------------------------------------------- Swipe
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Dif
        swipeSavedBalance.myTimerSaved = new List<float>() {
            0f,
            60f,
            90f,
            120f,
            165f,
            240f,
            360f,
            480f};
        swipeSavedBalance.myValueSaved = new List<float>() {
            4f,
            3f,
            1.36f,
            1.41f,
            1.4f, 
            0.853f,
            0.21f,
            0f};
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Dif
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Food
        swipeFoodBalance.myTimerSaved = new List<float>() { 
            0f, 
            120f,
            360f,
            480f};
        swipeFoodBalance.myValueSaved = new List<float>() {
            2.5f,
            2f,
            0.07f,
            10f};
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Food
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Event
        swipeRandomBalance.myTimerSaved = new List<float>() { 
            0f, 
            49f,
            50f,
            120f, 
            300f, 
            301f, 
            360f,
            480f};
        swipeRandomBalance.myValueSaved = new List<float>() { 
            10000f,
            10000f,
            150f,
            100f,
            150f, 
            10000f,
            10000f,
            100f};
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Event
        //----------------------------------------------------------- Swipe
        //----------------------------------------------------------- Stone
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Dif
        stoneSavedBalance.myTimerSaved = new List<float>() {
            0f,//1
            65f,//2
            66f,//3
            174f,//4
            175f,//5
            294f,//6
            295f,//7
            330f,//8
            331f,//9
            360f,//10
            480f};//11
        stoneSavedBalance.myValueSaved = new List<float>() { 
            1f, //1
            1f, //2
            2f, //3
            2f, //4
            3f, //5
            3f, //6
            4f, //7
            4f, //8
            5f,//9
            5f, //10
            7f };//11
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Dif
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Food
        stoneFoodBalance.myTimerSaved = new List<float>() { 
            0f, 
            300f,
            301f};
        stoneFoodBalance.myValueSaved = new List<float>() { 
            1.8f,
            1.2f,
            1f};
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Food
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Event
        stoneRandomBalance.myTimerSaved = new List<float>() {
            0f,
            60f,
            61f, 
            180f,
            300f,
            301f,
            360f,
            480f};
        stoneRandomBalance.myValueSaved = new List<float>() { 
            10000f,
            10000f, 
            150f,
            100f,
            150f, 
            10000f,
            10000f,
            100f};
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Event
        //----------------------------------------------------------- Stone
        //----------------------------------------------------------- BigFish
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Dif
        bigfishSavedBalance.myTimerSaved = new List<float>() { 
            0f,
            15f,
            360f};
        bigfishSavedBalance.myValueSaved = new List<float>() {
            4f,
            1.2f, 
            1.2f };
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Dif
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Food
        bigfishFoodBalance.myTimerSaved = new List<float>() { 
            0f, 
            15f, 
            16f,
            35f, 
            36f};
        bigfishFoodBalance.myValueSaved = new List<float>() { 
            4f,
            4f, 
            0.18f, 
            0.18f, 
            0.6f};
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Food
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Aspect
        bigfishRandomBalance.myTimerSaved = new List<float>() { 
            0f, 
            35f, 
            36f, 
            360f, 
            480f };
        bigfishRandomBalance.myValueSaved = new List<float>() {
            1.3f,
            1.3f,
            2.05f,
            2.15f,
            10f};
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Aspect
        //----------------------------------------------------------- BigFish
        //=========================================================== Результаты баланса
        //=========================================================== Начальная реализация баланса
        if (GetActiveSceneScript.currentScene == SS.SwipeMode)
        {
            savedBalance = swipeSavedBalance;
            savedFood = swipeFoodBalance;
            savedRandom = swipeRandomBalance;
        }
        else if (GetActiveSceneScript.currentScene == SS.StoneMode)
        {
            savedBalance = stoneSavedBalance;
            savedFood =  stoneFoodBalance;
            savedRandom = stoneRandomBalance;
        }
        else if (GetActiveSceneScript.currentScene == SS.BigFishMode)
        {
            savedBalance = bigfishSavedBalance;
            savedFood = bigfishFoodBalance;
            savedRandom = bigfishRandomBalance;
        }
        //=========================================================== Начальная реализация баланса
        //=========================================================== Счётчики для вершин массива, во время прохода в Update
        b = 1;
        d = 1;
        e = 1;
        //=========================================================== Счётчики для вершин массива, во время прохода в Update
        //=========================================================== Начальная сложность
        StartInit<SavedBalance>(ref savedBalance, ref EnemyDifficult);
        StartInit<SavedFood>(ref savedFood, ref FoodDifficult);
        StartInit<SavedRandom>(ref savedRandom, ref RandomEventDifficult);
        void StartInit 
           <T1>(ref T1 classObject, ref float difficult)
           where T1 : ISaved, new()
        {
            difficult = classObject.MyValueSaved[0];
        }
        //=========================================================== Начальная сложность
    }
    void Update()
    {
        //=========================================================== Секундомер

        //=========================================================== Секундомер
        void DifUpdater<T> //обновляет текущий баланс каждый кадр - результат этого инструмента
            (ref T classObject, ref int CMD, ref float resultDifficult) //CMD - counterMassiveDots
            where T : ISaved, new()
        {
            if (classObject.MyTimerSaved.Count > CMD)
            {
                if (classObject.MyTimerSaved[CMD] > Timer.MyTimer)
                {
                    resultDifficult = Formula(classObject.MyValueSaved, classObject.MyTimerSaved, CMD);
                }
                else
                {
                    CMD++;
                }
            }
        }
        //=========================================================== Исполнение
        DifUpdater<SavedBalance>(ref savedBalance, ref b, ref EnemyDifficult);
        DifUpdater<SavedFood>(ref savedFood, ref d, ref FoodDifficult);
        DifUpdater<SavedRandom>(ref savedRandom, ref e, ref RandomEventDifficult);
        //=========================================================== Исполнение
        //Debug.Log($"EnemyDifficult = {EnemyDifficult}");
        //Debug.Log($"FoodDifficult = {FoodDifficult}");
        //Debug.Log($"RandomDifficult = {RandomEventDifficult}");
    }
    float Formula //линейная интерполяция: за задаными двумя точками и текущим временем, находит значение в этой точке
     (List<float> value, List<float> time, int e)
    {
        float difCurTime = Timer.MyTimer - time[e - 1];
        float difValue = value[e] - value[e - 1];
        float difTime = time[e] - time[e - 1];
        float result = value[e - 1] + ((difCurTime) * (difValue)) / difTime;
        return result;
    }
    public class SavedBalance : ISaved
    {
        public List<float> MyTimer { get { return myTimer; } set { myTimer = value; } }
        static public List<float> myTimer = new List<float>();
        public List<float> MyValue { get { return myValue; } set { myValue = value; } }
        static public List<float> myValue = new List<float>();
        public List<float> MyTimerSaved { get { return myTimerSaved; } set { myTimerSaved = value; } }
        public List<float> myTimerSaved = new List<float>();
        public List<float> MyValueSaved { get { return myValueSaved; } set { myValueSaved = value; } }
        public List<float> myValueSaved = new List<float>();

    }
    public class SavedFood : ISaved
    {
        public List<float> MyTimer { get { return myTimer; } set { myTimer = value; } }
        static public List<float> myTimer = new List<float>();
        public List<float> MyValue { get { return myValue; } set { myValue = value; } }
        static public List<float> myValue = new List<float>();
        public List<float> MyTimerSaved { get { return myTimerSaved; } set { myTimerSaved = value; } }
        public List<float> myTimerSaved = new List<float>();
        public List<float> MyValueSaved { get { return myValueSaved; } set { myValueSaved = value; } }
        public List<float> myValueSaved = new List<float>();
    }
    public class SavedRandom : ISaved
    {
        public List<float> MyTimer { get { return myTimer; } set { myTimer = value; } }
        static public List<float> myTimer = new List<float>();
        public List<float> MyValue { get { return myValue; } set { myValue = value; } }
        static public List<float> myValue = new List<float>();
        public List<float> MyTimerSaved { get { return myTimerSaved; } set { myTimerSaved = value; } }
        public List<float> myTimerSaved = new List<float>();
        public List<float> MyValueSaved { get { return myValueSaved; } set { myValueSaved = value; } }
        public List<float> myValueSaved = new List<float>();
    }
    public interface ISaved
    {
        List<float> MyTimer { get; set; }
        List<float> MyValue { get; set; }
        List<float> MyTimerSaved { get; set; }
        List<float> MyValueSaved { get; set; }
    }
}

















//void Load(ref SavedFood savedBalance)
//{
//    if (File.Exists(Application.persistentDataPath + $"/save2{SceneManager.GetActiveScene().name}.txt"))
//    {
//        string saveString = File.ReadAllText(Application.persistentDataPath + $"/save2{SceneManager.GetActiveScene().name}.txt");
//        savedBalance = JsonUtility.FromJson<SavedFood>(saveString);
//    }
//}












//var tempFind = FindObjectsOfType<BalanceCard>();
//if (tempFind != null)
//{
//    float[] tempTimer = new float[400];
//    float[] tempValue = new float[400];
//    int i = 0;

//    savedBalance.MyTimer = new List<float>();//почистить сохранённое
//    savedBalance.MyValue = new List<float>();


//    while (tempFind.Length > i)
//    {
//        char[] myCharArray = tempFind[i].TimeField.text.ToCharArray();
//        if (myCharArray.Length == 3
//            && tempFind[i].PrecentField.text != "")
//        {
//            tempTimer[i] = (float)char.GetNumericValue(myCharArray[0]) * 60 + (float)char.GetNumericValue(myCharArray[1]) * 10 + (float)char.GetNumericValue(myCharArray[2]);//Записывает с карты в массив значния по порядку за правилом (5:00)

//            tempValue[i] = float.Parse(tempFind[i].PrecentField.text, CultureInfo.InvariantCulture);
//            //float q = Mathf.Pow(SavedBalance.MyValue[i + 1] / SavedBalance.MyValue[i-1], 1.0f / ((SavedBalance.MyTimer[i + 1] - SavedBalance.MyTimer[i - 1] / 2) - 1));
//            //tempValue[i] = tempValue[i - 1] * Mathf.Pow(q, i) * float.Parse(tempFind[0].PrecentField.text);

//            SavedBalance.myTimer.Add(tempTimer[i]);
//            SavedBalance.myValue.Add(tempValue[i]);
//            SortArray(ref SavedBalance.myTimer, ref SavedBalance.myValue);
//            Save<SavedBalance>(ref savedBalance);
//        }
//        i++;















//    }
//}
//var tempFindFood = FindObjectsOfType<EaterCard>();
//if (tempFindFood != null)
//{
//    float[] tempTimer = new float[400];
//    float[] tempValue = new float[400];
//    int i = 0;

//    SavedFood.myTimer = new List<float>();//почистить сохранённое
//    SavedFood.myValue = new List<float>();


//    while (tempFindFood.Length > i)
//    {
//        char[] myCharArray = tempFindFood[i].TimeField.text.ToCharArray();
//        if (myCharArray.Length == 3
//            && tempFindFood[i].PrecentField.text != "")
//        {
//            tempTimer[i] = (float)char.GetNumericValue(myCharArray[0]) * 60 + (float)char.GetNumericValue(myCharArray[1]) * 10 + (float)char.GetNumericValue(myCharArray[2]);
//            //Записывает с карты в массив значния по порядку за правилом (5:00)

//            tempValue[i] = float.Parse(tempFindFood[i].PrecentField.text, CultureInfo.InvariantCulture);
//            //float q = Mathf.Pow(SavedFood.MyValue[i + 1] / SavedFood.MyValue[i-1], 1.0f / ((SavedFood.MyTimer[i + 1] - SavedFood.MyTimer[i - 1] / 2) - 1));
//            //tempValue[i] = tempValue[i - 1] * Mathf.Pow(q, i) * float.Parse(tempFindFood[0].PrecentField.text);

//            SavedFood.myTimer.Add(tempTimer[i]);
//            SavedFood.myValue.Add(tempValue[i]);
//            SortArray(ref SavedFood.myTimer, ref SavedFood.myValue);
//            Save<SavedFood>(ref savedFood);
//        }


//        i++;
//    }
//}



















//void Save(ref SavedBalance savedBalance)
//{
//    savedBalance = new SavedBalance()
//    {
//        MyTimerSaved = SavedBalance.MyTimer,
//        MyValueSaved = SavedBalance.MyValue,
//    };
//    string json = JsonUtility.ToJson(savedBalance);
//    File.WriteAllText(Application.persistentDataPath + $"/save{SceneManager.GetActiveScene().name}.txt", json);
//}
//void Save(ref SavedFood savedBalance)
//{
//    savedBalance = new SavedFood()
//    {
//        MyTimerSaved = SavedFood.MyTimer,
//        MyValueSaved = SavedFood.MyValue,
//    };
//    string json = JsonUtility.ToJson(savedBalance);
//    File.WriteAllText(Application.persistentDataPath + $"/save2{SceneManager.GetActiveScene().name}.txt", json);
//}
//void Save(ref SavedRandom savedBalance)
//{
//    savedBalance = new SavedRandom()
//    {
//        MyTimerSaved = SavedRandom.MyTimer,
//        MyValueSaved = SavedRandom.MyValue,
//    };
//    string json = JsonUtility.ToJson(savedBalance);
//    File.WriteAllText(Application.persistentDataPath + $"/save3{SceneManager.GetActiveScene().name}.txt", json);
//}











//=========================================================== Обычный
//if (savedBalance.MyTimerSaved.Count > b)
//{
//    if (savedBalance.MyTimerSaved[b] > Timer.MyTimer)
//    {
//        EnemyDifficult = Formula(savedBalance.MyValueSaved, savedBalance.MyTimerSaved, b);
//    }
//    else
//    {
//        b++;
//    }
//}
//=========================================================== Обычный
//=========================================================== Еда
//if (savedFood.MyTimerSaved.Count > d)
//{
//    if (savedFood.MyTimerSaved[d] > Timer.MyTimer)
//    {
//        FoodDifficult = Formula(savedFood.MyValueSaved, savedFood.MyTimerSaved, d);
//    }
//    else
//    {
//        d++;
//    }
//}
//=========================================================== Еда
//=========================================================== Рандом ивент
//if (savedFood.MyTimerSaved.Count > d)
//{
//    if (savedFood.MyTimerSaved[d] > Timer.MyTimer)
//    {
//        FoodDifficult = Formula(savedFood.MyValueSaved, savedFood.MyTimerSaved, d);
//    }
//    else
//    {
//        d++;
//    }
//}
//=========================================================== Рандом ивент








//int c = 0;
//while (savedFood.MyTimerSaved.Count > c)
//{
//    GameObject newInstantiate
//        = Instantiate(EaterCard,
//        new Vector2(PositionOfTimeMark.position.x, PositionOfTimeMark.position.y - (350 * (c + savedBalance.MyTimerSaved.Count))),
//        Quaternion.identity);

//    newInstantiate.transform.SetParent(BalanceContent.transform);
//    EaterCard balanceCardOfIntantiate = newInstantiate.GetComponent<EaterCard>();
//    balanceCardOfIntantiate.TimeField.text = GiveMeMyNumberBitch((int)savedFood.MyTimerSaved[c]);
//    balanceCardOfIntantiate.PrecentField.text = savedFood.MyValueSaved[c].ToString().Replace(",", ".");
//    //if (c != 0)
//    //    balanceCardOfIntantiate.LastResult.text = Formula(savedFood.MyValueSaved, savedFood.MyTimerSaved, c).ToString();
//    c++;
//}