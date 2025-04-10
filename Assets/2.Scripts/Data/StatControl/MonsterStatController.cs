using MonsterEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatController 
{
    BaseMonster monster = null;
    
    public MonsterStatController(BaseMonster _monster, MonsterID _monsterID = MonsterID.Zombie)
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
