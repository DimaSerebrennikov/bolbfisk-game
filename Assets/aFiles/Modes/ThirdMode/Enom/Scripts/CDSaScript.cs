using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDSaScript : MonoBehaviour
{
    public static float ChooseDirectionAndStabilization(float a, float b)
    {
        float c = Mathf.Abs(a - b);
        float d = 360 - Mathf.Abs(a - b);

        if (c < d)
        {
            return ZeroSlowlyMax(c, a, b);
        }
        else
        {
            return ZeroSlowlyMax(d, b, a);
        }
        float ZeroSlowlyMax(float minimalAngle, float side1, float side2)
        {
            if (minimalAngle < 2)
            {
                return 0;
            }
            else if (side1 > side2)
            {
                if (minimalAngle < 15)
                {
                    return -0.4f;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (minimalAngle < 15)
                {
                    return 0.4f;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
