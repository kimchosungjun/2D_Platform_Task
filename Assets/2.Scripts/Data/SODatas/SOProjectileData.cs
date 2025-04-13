using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SOProjectileData", menuName = "SOProjectileData", order = int.MinValue)]
public class SOProjectileData : ScriptableObject
{
    public float launchSpeed;
    public float maintainTime;
    public int damage;

    public ProjectileData GetProjectileData()
    {
        ProjectileData projectileData = new ProjectileData(launchSpeed, maintainTime, damage);
        return projectileData;
    }
}

