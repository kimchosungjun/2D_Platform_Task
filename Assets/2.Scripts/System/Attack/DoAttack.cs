using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoAttack : MonoBehaviour
{
    public abstract void Attack();
}

public class DoAttackProjectile : DoAttack
{
    protected float maintainTime = 0;
    protected DoAttackData doAttackData = null;
    public DoAttackProjectile(DoAttackData _doAttackData ,float _maintainTime = 10)
    {
        doAttackData = _doAttackData;
        maintainTime = _maintainTime;
    }

    public override void Attack()
    {
        StartCoroutine(CShootProjectile());
    }

    IEnumerator CShootProjectile()
    {
        float time = 0;
        while (time < maintainTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}

public class DoAttackNear : DoAttack
{
    public override void Attack()
    {

    }
}