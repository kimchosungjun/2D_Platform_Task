using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : OperateByScene
{
    public static GameSystem Instance = null;

    [SerializeField] TruckController truckController;
    [SerializeField] FadeUI fadeUI;
    void Awake()
    {
        Init();
    }
    public override void Init()
    {
        if(GlobalMgr.ResourceMgr == null)
            GlobalMgr.ResourceMgr = new ResourceMgr();
        if (GlobalMgr.UIMgr == null)
            GlobalMgr.UIMgr = new UIMgr();

        if(truckController==null)
            truckController = FindObjectOfType<TruckController>();
        if (fadeUI == null)
            fadeUI = FindObjectOfType<FadeUI>();

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        Setup();
    }

    public override void Setup()
    {
        fadeUI.Fade(true, () => { fadeUI.gameObject.SetActive(false); });   
    }

    public void StartGame()
    {
        GlobalMgr.UIMgr.MainSceneUIControl.StartGame();
        MonsterMgr.Instance.StartSpawn();
    }

    public void DefeatGame()
    {
        MonsterMgr.Instance.StopSpawn();
        GlobalMgr.MonsterMgr.ClearKillLog();
        GlobalMgr.UIMgr.MainSceneUIControl.EndGame();
    }

    public void ReadyGame()
    {
        GlobalMgr.UIMgr.MainSceneUIControl.ReadyGame();
        truckController.ResetHeros();
    }
}
