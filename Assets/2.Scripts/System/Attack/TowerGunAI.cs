using UnityEngine;

public class TowerGunAI : MonoBehaviour
{
    [SerializeField] protected SOProjectileData soProjectileData;
    [SerializeField] protected Transform shootTransform;
    [SerializeField] protected Transform gunTransform; 
    [SerializeField] float angleOffset = 5f;
    [SerializeField] float sightDist = 20f;
    public float fireTermTime = 3f;
    private float fireTimer = 0f;
    float minDist = Mathf.Infinity;

    void FixedUpdate()
    {
        Transform target = FindClosestEnemy();
        if (target != null)
            RotateToTarget(target.position);

        if(fireTermTime + 1f > fireTimer)
            fireTimer += Time.fixedDeltaTime;
        
        if (fireTimer >= fireTermTime && target!=null)
        {
            FireBullets();
            fireTimer = 0f;
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        minDist = Mathf.Infinity;
        int cnt = enemyObjects.Length;
        for(int i=0; i<cnt; i++)
        {
            float dist = Vector2.Distance(transform.position, enemyObjects[i].transform.position);
            if (dist < minDist && dist < sightDist)
            {
                minDist = dist;
                closest = enemyObjects[i].transform;
            }
        }
        return closest;
    }

    void RotateToTarget(Vector2 _targetPos)
    {
        Vector2 direction = _targetPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FireBullets()
    {
        float baseAngle = gunTransform.eulerAngles.z;
        FireBullet(baseAngle);                     
        FireBullet(baseAngle + angleOffset);
        FireBullet(baseAngle - angleOffset); 
    }

    void FireBullet(float _angle)
    {
        float rad = _angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        float bulletAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, bulletAngle);
        /*Transform bulletTransform = */GlobalMgr.PoolMgr.GetPool(UtilEnums.PoolEnums.Bullet, UtilEnums.PoolParentEnums.Bullet, shootTransform.position, rotation).
            GetComponent<Projectile>().SetData(soProjectileData.GetProjectileData(), UtilEnums.TagEnums.Enemy, direction);
    }
}
