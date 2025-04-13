using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSystem : OperateByScene
{
    [SerializeField] TitleFadeTextUI titleFadeTextUI;
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        GlobalMgr.ResourceMgr = new ResourceMgr();
        GlobalMgr.SceneMgr= new SceneMgr();
        GlobalMgr.UIMgr = new UIMgr();
    }

    void Start()
    {
        Setup();
    }

    public override void Setup()
    {
        titleFadeTextUI.ShowFade();
    }
}
