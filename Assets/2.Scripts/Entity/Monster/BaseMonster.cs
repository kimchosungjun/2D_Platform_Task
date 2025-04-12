using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;
using MonsterEnums;
using System;

public abstract class BaseMonster : BaseEntity
{
    #region Variables
    [Header("Common Value")]
    [SerializeField] protected MonsterIDEnums monsterID = MonsterIDEnums.Zombie;
    protected int climbLayer = 0;
    protected int spriteLayer = 0;
    protected int detectLayer = 0;

    [Header("Common Component")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb; 
    [SerializeField] protected CapsuleCollider2D coll;
    [SerializeField] protected Transform detectTransform;
    protected MonsterStatController monsterStatController = null;
    #endregion

    #region Relate Setting
    public override void Init()
    {
        base.Init();
        if(anim==null) anim = GetComponentInChildren<Animator>();   
        if(rb==null) rb=  GetComponent<Rigidbody2D>();
        if(coll == null) coll = GetComponentInChildren<CapsuleCollider2D>();
        if (sprites == null) sprites = GetComponentsInChildren<SpriteRenderer>();
        if (monsterStatController == null) monsterStatController = new MonsterStatController(this , monsterID);
    }

    public override void Setup()
    {
        //climbLayer = 1 << Enums.EnumToValue(LayerEnums.Climable) 
        //    | 1 << Enums.EnumToValue(LayerEnums.Monster);
    }
    
    /// <summary>
    /// Pool : Reuse Monster
    /// </summary>
    /// <param name="_position"></param>
    public virtual void Pooling(Vector2 _position)
    {
        SetLayerPosition( _position);
        SetMonsterData();
    }

    void SetLayerPosition(Vector2 _position)
    {

        // Set Layer
        detectLayer = 0;
        Tuple<LayerEnums,int> getTuple= Randoms.GetRandomLayer();

        this.gameObject.layer = (int)getTuple.Item1;
        spriteLayer = getTuple.Item2;
        detectLayer = 1 << (int)getTuple.Item2;

        int sprCnt = sprites.Length;
        for(int i=0; i<sprCnt; i++)
            sprites[i].sortingOrder = getTuple.Item2;

        // Set Position
        transform.position = _position;
        transform.position += Vector3.down * 0.5f * getTuple.Item2; 
    }

    // To Do ~~ Data
    protected virtual void SetMonsterData() { }

    #endregion

    #region Relate State
    public virtual void Hit(int _damage)
    {
        monsterStatController?.Hit(_damage);
    }

    public virtual void Death()
    {
        // To Do Death 
    }
    #endregion
}
