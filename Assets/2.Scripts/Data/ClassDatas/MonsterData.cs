using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData 
{
    public int monsterHP;
    public int monsterAtk;
    public int monsterDef;
    public float monsterSpeed;

    public MonsterData(int _monsterHP, int _monsterAtk, int _monsterDef, float _monsterSpeed)
    {
        this.monsterHP = _monsterHP;
        this.monsterAtk = _monsterAtk;
        this.monsterDef = _monsterDef;
        this.monsterSpeed = _monsterSpeed;
    }
}
