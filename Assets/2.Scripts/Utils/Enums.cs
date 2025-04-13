using System;

public class Enums 
{
    public static string EnumToString<T>(T _enumValue) where T : Enum
    {
        return Enum.GetName(typeof(T), _enumValue);
    }

    public static int EnumToValue<T>(T _enumValue) where T : Enum
    {
        return Convert.ToInt32(_enumValue);
    }
}

namespace UtilEnums
{
    public enum SceneEnums
    {
        TitleScene = 0,
        MainScene = 1,
    }

    public enum TagEnums
    {
        Hero,
        Enemy,
    }

    public enum LayerEnums
    {
        Default = 0,
        TransparentFX=1,
        IgnoreRaycast=2,
        Hero = 3,
        Water=4,
        UI =5,
        HeroBox = 6,
        HeroAttack = 7,
        Map = 8,
        Monster_Line1 =11,
        Monster_Line2 =12,
        Monster_Line3 =13,
    }

    public enum PoolParentEnums
    {
        Monster=0,
        Bullet=1,
        DamageLog=2,
    }

    public enum PoolEnums
    {
        Zombie = 0,
        Bullet = 1,
    }

    public enum BulletEnums
    {
        Bullet = 0,
    }
}


namespace MonsterEnums
{
    public enum MonsterIDEnums
    {
        Zombie = 0,
    }
}
