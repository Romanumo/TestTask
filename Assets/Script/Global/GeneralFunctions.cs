using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralFunctions
{
    public static int CircleIndex(int value, int maxValue)
    {
        int result;
        result = value;
        if (result < 0)
            result = maxValue;

        if (result > maxValue)
            result = 0;
        return result;
    }
}
