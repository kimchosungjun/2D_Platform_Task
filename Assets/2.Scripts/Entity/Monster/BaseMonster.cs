using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;
using MonsterEnums;

public abstract class BaseMonster : BaseEntity
{
    #region Variables
    [Header("Common Value")]
    [SerializeField] protected MonsterID monsterID = MonsterID.Zombie;
    protected int climbLayer = 0;
    protected int spriteLayer = 0;

    [Header("Common Component")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected CapsuleCollider2D coll;
    protected SpriteRenderer[] sprites;
    protected MonsterStatController monsterStatController = null;
    #endregion

    #region Relate Setting
    public override void Init()
    {
        if(anim==null) anim = GetComponentInChildren<Animator>();   
        if(coll == null) coll = GetComponentInChildren<CapsuleCollider2D>();
        if (sprites == null) sprites = GetComponentsInChildren<SpriteRenderer>();
        if (monsterStatController == null) monsterStatController = new MonsterStatController(this , monsterID);
    }

    public override void Setup()
    {
        climbLayer = 1 << Enums.EnumToValue(LayerEnums.Climable) 
            | 1 << Enums.EnumToValue(LayerEnums.Monster);
    }
    
    // Pool : Reuse Monster
    public virtual void Pooling(int _layer, Vector2 _position)
    {
        SetLayerPosition(_layer, _position);
        SetMonsterData();
    }

    void SetLayerPosition(int _layer, Vector2 _position)
    {
        // Set Layer
        spriteLayer = _layer;
        int sprCnt = sprites.Length;
        for(int i=0; i<sprCnt; i++)
            sprites[i].sortingOrder = _layer;

        // Set Position
        transform.position = _position;
        transform.position += Vector3.down * 0.5f * _layer; 
    }

    // To Do ~~ Data
    void SetMonsterData()
    {

    }

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
