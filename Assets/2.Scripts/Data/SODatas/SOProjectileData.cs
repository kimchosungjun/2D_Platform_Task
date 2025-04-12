using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SOProjectileData", menuName = "SOProjectileData", order = int.MinValue)]
public class SOProjectileData : ScriptableObject
{
    public float launchSpeed;
    public int damage;
}

