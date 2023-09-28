using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityMethods
{
    public static float Map(float val, float rangeMin, float rangeMax, float newRangeMin, float newRangeMax)
    {
        float inter = Mathf.InverseLerp(rangeMin, rangeMax, val);
        float output = Mathf.Lerp(newRangeMin, newRangeMax, inter);
        return output;
    }
}
