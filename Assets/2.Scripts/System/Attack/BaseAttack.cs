using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;

public abstract class BaseAttack : MonoBehaviour
{
    [SerializeField] protected DoAttack doAttack;
    protected AttackData attackData = null;
    protected string enemyTag = string.Empty;
    protected void SetEnemyTag(TagEnums _tagEnum)
    {
        enemyTag = Enums.EnumToString<TagEnums>(_tagEnum);
    }

    public virtual void SetData(TagEnums _tagEnum = TagEnums.Enemy, int _attackValue = 1)
    {
        if(attackData==null)
            attackData = new AttackData();  

        attackData.AttackValue = _attackValue;
        SetEnemyTag(_tagEnum);
    }

    public virtual void Attack() { if(doAttack!=null) doAttack.Attack(); }
}

