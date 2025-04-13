using UnityEngine;

[CreateAssetMenu(fileName = "SOHerorData", menuName = "SOHeroData", order = int.MinValue)]
public class SOHeroData : ScriptableObject
{
    public int heroHP;
    public int heroDef;

    public HeroData GetHeroData()
    {
        HeroData hero = new HeroData(heroHP, heroDef);
        return hero;
    }
}
