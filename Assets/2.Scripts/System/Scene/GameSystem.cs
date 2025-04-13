using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : OperateByScene
{
    [SerializeField] FadeUI fadeUI;
    void Awake()
    {
        Init();
    }
    public override void Init()
    {
        GlobalMgr.ResourceMgr = new ResourceMgr();

        //if (fadeUI == null)
        //    fadeUI = FindObjectOfType<FadeUI>();
    }

    void Start()
    {
        Setup();
    }

    public override void Setup()
    {
        //fadeUI.Fade(true, () => { fadeUI.gameObject.SetActive(false); });
        MonsterMgr.Instance.StartSpawn();
    }
}
