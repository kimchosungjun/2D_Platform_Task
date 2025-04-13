using UnityEngine;
using UtilEnums;
using MonsterEnums;
using System.Collections;

public abstract class BaseMonster : BaseEntity
{
    // Values : Common Component, This Layer 
    #region Variables
    //[Header("Common Value")]
    protected int monsterLayer;
    protected const string animState = "State";
    //[SerializeField] protected MonsterIDEnums monsterID = MonsterIDEnums.Zombie;

    //[Header("Common Component")]
    /*[SerializeField] */protected Animator anim;
    /*[SerializeField] */protected Rigidbody2D rigid; 
    /*[SerializeField] */protected CapsuleCollider2D coll;
    /*[SerializeField] */protected MonsterStatController statController;

    [Header("Attack")]
    [SerializeField] protected float attackCoolDownTime = 0.5f;
    protected bool isCoolDownAttack = false;
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
    #endregion

    #region Relate Pooling

    /// <summary>
    /// Pool : Reuse Monster
    /// </summary>
    /// <param name="_position"></param>
    public virtual void Pooling(LayerEnums _layerEnums)
    {
        if (this.gameObject.activeSelf == false)
            this.gameObject.SetActive(true);

        SetLayer( _layerEnums);
        SetMonsterData();
    }

    void SetLayer(LayerEnums _layerEnums)
    {
        monsterLayer = 0;
        int layerValue = (int)_layerEnums;
        this.gameObject.layer = (int)_layerEnums;
        monsterLayer = 1 << layerValue;
    }

    protected virtual void SetMonsterData()  { statController.ResetStat(); }
    #endregion

    #region Relate State
    public virtual void Death() 
    {
        this.gameObject.SetActive(false);
        MonsterMgr.Instance.DecreaseMonsterCnt();
    }

    public virtual void SetAnim(MonsterAnimState _animState)
    {
        anim.SetInteger(animState, (int)_animState);
    }

    protected void CoolDownAttack()
    {
        isCoolDownAttack = true;
        StartCoroutine(CCoolDownAttack());
    }

    protected IEnumerator CCoolDownAttack()
    {
        float time = 0;
        while(time < attackCoolDownTime)
        {
            time += Time.deltaTime;
            yield return null;  
        }
        SetAnim(MonsterAnimState.Run);
        isCoolDownAttack = false;
    }
    #endregion
}
