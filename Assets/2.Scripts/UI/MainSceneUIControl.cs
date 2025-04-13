using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUIControl : MonoBehaviour
{
    #region Link UI
    [SerializeField] StartUI startUI;
    [SerializeField] DeadUI deadUI ;
    [SerializeField] EnemyKillUI killUI;
    [SerializeField] FadeUI fadeUI;
    #endregion

    #region Get UI : Property
    public StartUI StartUI { get { return startUI; } }
    public DeadUI DeadUI { get { return deadUI; } }
    public EnemyKillUI EnemyKillUI { get { return killUI; } }
    public FadeUI FadeUI { get { return fadeUI; } }
    #endregion

    void Awake()
    {
        //GlobalMgr.UIMgr.MainSceneUIControl = this;    
    
        if(startUI==null) startUI = GetComponentInChildren<StartUI>();
        if(deadUI == null) deadUI = GetComponentInChildren<DeadUI>();
        if(killUI == null) killUI = GetComponentInChildren<EnemyKillUI>();
        if(fadeUI == null) fadeUI = GetComponentInChildren<FadeUI>();
    }
}
