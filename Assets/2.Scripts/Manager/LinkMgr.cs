using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkMgr : MonoBehaviour
{
    void Awake()
    {
        GlobalMgr.Resource = new ResourceMgr();    
    }

    void Start()
    {
        
    }
}
