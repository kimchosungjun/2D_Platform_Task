using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSystem : OperateByScene
{
    LinkMgr linkMgr = null;
    void Awake()
    {
     
    }

    public override void Init()
    {
        if (linkMgr == null)
            linkMgr = new LinkMgr();
        linkMgr?.Init();
    }

    void Start()
    {
        
    }

    public override void Setup()
    {
        linkMgr?.Setup();
    }
}
