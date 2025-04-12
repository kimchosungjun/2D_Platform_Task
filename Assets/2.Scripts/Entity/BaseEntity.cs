using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitEffect))]
public abstract class BaseEntity : MonoBehaviour
{
    protected SpriteRenderer[] sprites;
    [SerializeField] HitEffect hitEffect;

    public virtual void Init()
    {
        if(hitEffect==null) hitEffect = GetComponent<HitEffect>();
        hitEffect.Init(sprites);
    }
    public abstract void Setup();
}
