using UnityEngine;
using UtilEnums;

public abstract class BaseMonster : BaseEntity
{
    // Values : Common Component, This Layer 
    #region Variables
    //[Header("Common Value")]
    protected int monsterLayer;
    //[SerializeField] protected MonsterIDEnums monsterID = MonsterIDEnums.Zombie;

    //[Header("Common Component")]
    /*[SerializeField] */protected Animator anim;
    /*[SerializeField] */protected Rigidbody2D rigid; 
    /*[SerializeField] */protected CapsuleCollider2D coll;
    /*[SerializeField] */protected MonsterStatController statController; 

    #endregion

    // Functions : Common Function(Setting, Pooling, Death)
    #region Relate Setting
    public override void Init()
    {
        base.Init();
        if(anim==null) anim = GetComponentInChildren<Animator>();   
        if(rigid==null) rigid=  GetComponent<Rigidbody2D>();
        if(coll == null) coll = GetComponentInChildren<CapsuleCollider2D>();
        if(statController == null) statController = GetComponentInChildren<MonsterStatController>();
    }

    public override void Setup()
    {
        monsterLayer = 0;
    }
    #endregion

    #region Relate Pooling

    /// <summary>
    /// Pool : Reuse Monster
    /// </summary>
    /// <param name="_position"></param>
    public virtual void Pooling(LayerEnums _layerEnums)
    {
        SetLayer( _layerEnums);
        SetMonsterData();
    }

    void SetLayer(LayerEnums _layerEnums)
    {
        monsterLayer = 0;
        int layerValue = (int)_layerEnums;
        this.gameObject.layer = (int)_layerEnums;
        monsterLayer = 1 << layerValue;
        int sprCnt = sprites.Length;
        for(int i=0; i<sprCnt; i++)
            sprites[i].sortingOrder = layerValue-10;
    }

    protected virtual void SetMonsterData()  { statController.ResetStat(); }
    #endregion

    #region Relate State
    public virtual void Death() { this.gameObject.SetActive(false); }
    #endregion
}
