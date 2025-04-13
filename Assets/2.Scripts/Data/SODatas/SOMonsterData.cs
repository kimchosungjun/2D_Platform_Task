using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SOMonsterData", menuName ="SOMonsterData", order =int.MinValue)]
public class SOMonsterData : ScriptableObject
{
    [Header("Stat")]
    public int monsterHP;
    public int monsterAtk;
    public int monsterDef;
    [Header("Movement")]
    public float monsterSpeed;
    public float monsterJumpForce;

    public MonsterData GetMonsterData()
    {
        MonsterData data = new MonsterData(monsterHP, monsterAtk, monsterDef, monsterSpeed, monsterJumpForce);
        return data;
    }
}
