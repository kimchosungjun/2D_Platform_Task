using MonsterEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatController 
{
    BaseMonster monster = null;
    //MonsterData monsterData = null;
    
    public MonsterStatController(BaseMonster _monster)
    {
        if (monster == null) monster = _monster;
    }

    public virtual void Hit(int _damamge)
    {

    }

    public void ResetStat()
    {

    }
}
