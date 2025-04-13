using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;

public class Randoms 
{
    /// <summary>
    /// return LayerEnum, intLayer : 1~3 
    /// </summary>
    /// <returns></returns>
    public static Tuple<LayerEnums,int> GetRandomLayer()
    {
        int result = UnityEngine.Random.Range(1, 4);
        LayerEnums layerEnums = (LayerEnums)(result + 10);
        return Tuple.Create(layerEnums, result);    
    }

    public static int GetRandomIntValue(int _includeStart, int _excludeEnd)
    {
        return UnityEngine.Random.Range(_includeStart, _excludeEnd);
    }

    public static float GetRandomFloatValue(float _includeStart, float _includeEnd)
    {
         return UnityEngine.Random.Range(_includeStart, _includeEnd);
    }
}
