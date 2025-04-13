using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;

public class PoolMgr : MonoBehaviour
{
    [Header("0 : Monster, 1 : Bullet")]
    [SerializeField] Transform[] poolParents;
    Dictionary<PoolEnums, List<Transform>> poolGroup = new Dictionary<PoolEnums, List<Transform>>();
    
    void Awake()
    {
        GlobalMgr.Pool = this;
        
    }

    public Transform GetPool(PoolEnums _poolEnums, PoolParentEnums _poolParentEnums = PoolParentEnums.Monster)
    {
        if (poolGroup.ContainsKey(_poolEnums))
        {
            List<Transform> list = poolGroup[_poolEnums];
            int listCnt = list.Count;
            for(int i=0; i<listCnt; i++)
            {
                if (list[i].gameObject.activeSelf == false)
                    return list[i];
            }

            Transform loadTransform = GlobalMgr.Resource.LoadResource<Transform>("Pool/"+Enums.EnumToString(_poolEnums));
            if (loadTransform == null) { Debug.LogError("Error!! Pool Error"); return null; }
            Transform instTransform = Instantiate(loadTransform, poolParents[(int)_poolParentEnums]);
            poolGroup[_poolEnums].Add(instTransform);
            return instTransform;
        }
        else
        {
            List<Transform> newList = new List<Transform>();
            Transform loadTransform = GlobalMgr.Resource.LoadResource<Transform>("Pool/" + Enums.EnumToString(_poolEnums));
            if (loadTransform == null) { Debug.LogError("Error!! Pool Error"); return null; }
            Transform instTransform = Instantiate(loadTransform, poolParents[(int)_poolParentEnums]);
            newList.Add(instTransform);
            poolGroup.Add(_poolEnums, newList);
            return instTransform;
        }
    }

    public void ClearPool()
    {
        //List<string> poolParentNames = new List<string>();
        int poolParentCnt = poolParents.Length;
        for(int i=0; i<poolParentCnt; i++)
        {
            //string poolName = poolParents[i].name;
            //poolParentNames.Add(poolName);
            Destroy(poolParents[i]);
        }

        poolGroup.Clear();  
    }
}
