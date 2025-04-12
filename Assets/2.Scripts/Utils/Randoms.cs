using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;

public class Randoms 
{
    /// <summary>
    /// return LayerEnum, intLayer : 1~4 
    /// </summary>
    /// <returns></returns>
    public static Tuple<LayerEnums,int> GetRandomLayer()
    {
        int result = UnityEngine.Random.Range(1, 5);
        LayerEnums layerEnums = (LayerEnums)(result + 10);
        return Tuple.Create(layerEnums, result);    
    }
}
