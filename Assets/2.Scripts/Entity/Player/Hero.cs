using UnityEngine;
using HeroEnums;

public class Hero : BaseEntity
{
    [SerializeField] HeroTypeEnums type = HeroTypeEnums.HeroBox;
    PlayerStatController playerStatController = null;
    ControlByTruckData controlByTruckData = new ControlByTruckData();
    BaseAI baseAI = null;

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        if(baseAI == null) baseAI = GetComponent<BaseAI>();
        if (playerStatController == null) playerStatController = GetComponent<PlayerStatController>();
        playerStatController.Hero = this;
        if (sprites == null) sprites = GetComponentsInChildren<SpriteRenderer>();
        if (hitEffect == null) hitEffect = GetComponent<HitEffect>();
        if (type == HeroTypeEnums.Hero)
            hitEffect.Init(sprites);
        else
            hitEffect.Init(sprites, false);
    }

    public void Death()
    {
        switch (type)
        {
            case HeroTypeEnums.HeroBox:
                controlByTruckData.Truck.AnnounceBreakBox(controlByTruckData.index);
                break; 
            case HeroTypeEnums.Hero:
                controlByTruckData.Truck.AnnounceBreakTruck(); ;
                GameSystem.Instance.DefeatGame();
                break;
            default:
                break;
        }
    }

    public void InitByTruck(TruckController _truck, int _index)
    {
        controlByTruckData.Truck = _truck;  
        controlByTruckData.index= _index;  
    }

    public void ResetHero()
    {
        baseAI.ResetData();
        playerStatController?.ResetStat();  
    }
}

public class ControlByTruckData
{
    public int index = -1;
    public TruckController Truck { get; set; } = null;
}
