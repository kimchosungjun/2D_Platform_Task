using UnityEngine;

public abstract class StatController : MonoBehaviour
{
    //public abstract void Heal(int _heal);
    public abstract void Hit(int _damage);
    public abstract void ResetStat();
}
