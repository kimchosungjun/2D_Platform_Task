using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    protected SpriteRenderer[] sprites;
    /*[SerializeField]*/ protected HitEffect hitEffect;

    public virtual void Init()
    {
        if (sprites == null) sprites = GetComponentsInChildren<SpriteRenderer>();
        if (hitEffect==null) hitEffect = GetComponent<HitEffect>();
        hitEffect.Init(sprites);
    }
    public abstract void Setup();
}
