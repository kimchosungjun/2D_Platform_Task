using UnityEngine;

public class MonsterStatController : StatController 
{
    [Header("Must Set")]
    [SerializeField] SOMonsterData soMonsterData;
    [SerializeField] HPIndicator hpIndicator;
    /*[SerializeField]*/ BaseMonster monster;
    /*[SerializeField]*/ HitEffect hitEffect;
    MonsterData monsterData = null;
    Vector3 floatTextPosDelta = new Vector3(-0.2f, 1.3f, 0);

    void Awake()
    {
        if (hpIndicator == null) hpIndicator = GetComponentInChildren<HPIndicator>();
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

        GlobalMgr.PoolMgr.GetPool(UtilEnums.PoolEnums.DamageLog, UtilEnums.PoolParentEnums.DamageLog,
            this.transform.position + floatTextPosDelta, Quaternion.identity).GetComponent<DamageLogUI>().SetDamage(damage);
        monsterData.monsterHP -= damage;
        hpIndicator.DecreaseHP(damage);
        if (monsterData.monsterHP <= 0)
            hitEffect?.DoHitEffect(monster.Death);
        else
            hitEffect?.DoHitEffect();
    }

    public override void ResetStat()
    {
        if (monsterData == null) return;
        monsterData = soMonsterData.GetMonsterData();
        hpIndicator.SetHP(monsterData.monsterHP);
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
