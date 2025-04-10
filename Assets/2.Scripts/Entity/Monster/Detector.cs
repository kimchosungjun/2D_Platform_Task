using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;

public class Detector : MonoBehaviour
{
    [SerializeField] protected Transform detectTransform;
    int targetLayer = 0;

    public void Init(LayerEnums[] _targetLayers)
    {
        int cnt = _targetLayers.Length;
        for(int i=0; i<cnt; i++)
        {
            if (_targetLayers[i] == LayerEnums.Default)
                targetLayer = targetLayer | 1; 
        }
    }

    public bool IsDetectTarget()
    {
        if (targetLayer == -1) return false;
        return false;
    }
}
