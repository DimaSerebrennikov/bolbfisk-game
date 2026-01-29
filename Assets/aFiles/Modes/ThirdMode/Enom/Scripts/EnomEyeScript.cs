using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnomEyeScript : MonoBehaviour
{
    //=========================================================== Объявление
    GameObject target;
    float fixedUpdateCounter;
    //=========================================================== Объявление
    private void Awake()
    {
        fixedUpdateCounter = 0;
    }
    private void FixedUpdate()
    {
        fixedUpdateCounter++;
        if (fixedUpdateCounter >= 4)
        {
            Eyeing();
            fixedUpdateCounter = 0f;
        }
    }
    void Eyeing()
    {
        //=========================================================== Поиск ближайшей рыбки

        float closestDistance = Mathf.Infinity;
        //----------------------------------------------------------- Преобразовать Fish[] в GameObject[]
        foreach (GameObject oneOfAllTargets in ObjectListScript.playerPassiveList)
        {
            float a = Vector2.Distance(oneOfAllTargets.transform.position, transform.position);
            if (a < closestDistance)
            {
                target = oneOfAllTargets;
                closestDistance = a;
            }
        }
        foreach (GameObject oneOfAllTargets in ObjectListScript.enemyPassiveList)
        {
            float a = Vector2.Distance(oneOfAllTargets.transform.position, transform.position);
            if (a < closestDistance)
            {
                target = oneOfAllTargets;
                closestDistance = a;
            }
        }
        //=========================================================== Поиск ближайшей рыбки
        if (target != null && target.activeSelf)
        {
            transform.transform.right = Vector2.Lerp(transform.right, target.transform.position - transform.position, 0.1f);
        }
    }
}
