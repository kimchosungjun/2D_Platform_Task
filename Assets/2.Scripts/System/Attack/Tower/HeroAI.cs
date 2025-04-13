using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : BaseAI
{
    [SerializeField] protected SOProjectileData soProjectileData;
    [SerializeField] protected Transform shootTransform;
    [SerializeField] protected Transform gunTransform;
    [SerializeField] float angleOffset = 5f;
    [SerializeField] float sightDist = 25f;
    public float fireTermTime = 3f;
    private float fireTimer = 0f;
    float minDist = Mathf.Infinity;
    Camera mainCam;
    Quaternion initRotation = Quaternion.Euler(new Vector3(0, 0, 0));

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        Vector2 inputDir = GetInputDirection();

        if (inputDir == Vector2.zero)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
                RotateToTarget(target.position);
        }
        else
        {
            RotateToTarget((Vector2)transform.position + inputDir.normalized);
        }

        if (fireTermTime + 1f > fireTimer)
            fireTimer += Time.deltaTime;

        Transform fireTarget = FindClosestEnemy();
        if (fireTimer >= fireTermTime && fireTarget != null)
        {
            FireBullets();
            fireTimer = 0f;
        }
    }

    Vector2 GetInputDirection()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
            return mouseWorld - (Vector2)transform.position;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Vector2 touchWorld = mainCam.ScreenToWorldPoint(Input.GetTouch(0).position);
            return touchWorld - (Vector2)transform.position;
        }
#endif
        return Vector2.zero;
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        minDist = Mathf.Infinity;
        int cnt = enemyObjects.Length;
        for (int i = 0; i < cnt; i++)
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
        GlobalMgr.PoolMgr.GetPool(UtilEnums.PoolEnums.Bullet, UtilEnums.PoolParentEnums.Bullet, shootTransform.position, rotation).
            GetComponent<Projectile>().SetData(soProjectileData.GetProjectileData(), UtilEnums.TagEnums.Enemy, direction);
    }

    public override void ResetData()
    {
        gunTransform.rotation = initRotation;
    }
}
