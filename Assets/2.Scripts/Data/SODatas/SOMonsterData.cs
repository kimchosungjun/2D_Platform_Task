using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SOMonsterData", menuName ="SOMonsterData", order =int.MinValue)]
public class SOMonsterData : ScriptableObject
{
    public int monsterHP;
    public int monsterAtk;
    public int monsterDef;
    public float monsterSpeed;

    public MonsterData GetMonsterData()
    {
        MonsterData data = new MonsterData(monsterHP, monsterAtk, monsterDef, monsterSpeed);
        return data;
    }
}
