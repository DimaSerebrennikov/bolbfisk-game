using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class OrderCorrection : MonoBehaviour
{
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer [] parts;

    private void Awake()
    {
        int randomOrder = (Random.Range(7, 1000)/7) * 7;
        body.sortingOrder = randomOrder;
        int a = 0;
        while(parts.Length > a)
        {
            if (parts[a] != null)
            parts[a].sortingOrder = randomOrder + 1;
            a++;
        }
    }
}
