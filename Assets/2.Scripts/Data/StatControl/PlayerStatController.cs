using UnityEngine;

public class PlayerStatController : StatController
{
    [Header("Must Set")]
    [SerializeField] SOHeroData soHeroData;
    [SerializeField] HPIndicator hpIndicator;

    HitEffect hitEffect;
    HeroData heroData = null;
    Vector3 floatTextPosDelta = new Vector3(-0.2f, 1.3f, 0);

    Hero hero = null;
    public Hero Hero { set { hero = value; } }  

    void Awake()
    {
        if (hpIndicator == null) hpIndicator = GetComponentInChildren<HPIndicator>();
        if (hitEffect == null) hitEffect = GetComponent<HitEffect>();
        if (soHeroData != null) heroData = soHeroData.GetHeroData();
    }

    void Start()
    {
        ResetStat();
    }

    public override void Hit(int _damage)
    {
        if (heroData == null) return;

        int damage = _damage - heroData.heroDef;

        // Min Damage
        if (damage <= 0)
            damage = 1;

        GlobalMgr.PoolMgr.GetPool(UtilEnums.PoolEnums.DamageLog, UtilEnums.PoolParentEnums.DamageLog,
            this.transform.position + floatTextPosDelta, Quaternion.identity).GetComponent<DamageLogUI>().SetDamage(damage);
        heroData.heroHP -= damage;
        hpIndicator.DecreaseHP(damage);

        if (heroData.heroHP <= 0)
        {
            hero.gameObject.SetActive(false);
            hero.Death();
            //hitEffect?.DoHitEffect(hero.Death);
        }
        else
            hitEffect?.DoHitEffect();
    }

    public override void ResetStat()
    {
        if (heroData == null) return;
        heroData = soHeroData.GetHeroData();
        hpIndicator.SetHP(heroData.heroHP);
    }
}
