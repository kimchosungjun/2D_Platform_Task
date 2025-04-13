using UnityEngine;

public class MonsterStatController : StatController 
{
    [Header("Must Set")]
    [SerializeField] SOMonsterData soMonsterData;
    /*[SerializeField]*/ BaseMonster monster = null;
    /*[SerializeField]*/ HitEffect hitEffect = null;

    MonsterData monsterData = null; 

    void Awake()
    {
        if(monster==null) monster = GetComponent<BaseMonster>();
        if(hitEffect==null) hitEffect = GetComponent<HitEffect>();
        if(soMonsterData!=null) monsterData = soMonsterData.GetMonsterData();
    }

    public override void Hit(int _damage)
    {
        if (monsterData == null) return;

        int damage = _damage - monsterData.monsterDef;

        // Min Damage
        if (damage <= 0)
            damage = 1;

        monsterData.monsterHP -= damage;
        if(monsterData.monsterHP<=0)
            hitEffect?.DoHitEffect(monster.Death);
        else
            hitEffect?.DoHitEffect();
    }

    public override void ResetStat()
    {
        if (monsterData == null) return;
        monsterData = soMonsterData.GetMonsterData();
    }

    public MonsterData GetMonsterData()
    {
        if (monsterData != null)
            return monsterData;

        if (soMonsterData == null)
            return null;
        monsterData = soMonsterData.GetMonsterData();
        
        return monsterData;
    }
}
