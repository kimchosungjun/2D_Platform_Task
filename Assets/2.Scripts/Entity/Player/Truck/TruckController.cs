using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    //int currentBoxCnt = 5;
    const int MaxBoxCnt = 5;

    OnTruckBox[] onTruckBoxSet = new OnTruckBox[MaxBoxCnt];

    void Awake()
    {
        InitBoxSet();
        Init();
    }

    void InitBoxSet()
    {
        if (onTruckBoxSet.Length == 0)
            onTruckBoxSet = GetComponentsInChildren<OnTruckBox>();
    }

    void Init()
    {
        
    }
}
