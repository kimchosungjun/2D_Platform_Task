using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGunAI : MonoBehaviour
{
    [SerializeField] protected Transform shootTransform;
    [SerializeField] protected Transform gunTransform; // �ѱ� ȸ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public float fireInterval = 3f;
    public float bulletSpeed = 10f;
    public float angleOffset = 5f; // ��/�Ʒ� ����
    private float fireTimer = 0f;

    void Update()
    {
        Transform target = FindClosestEnemy();
        if (target != null)
        {
            RotateToTarget(target.position);
        }

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            FireBullets();
            fireTimer = 0f;
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }

    void RotateToTarget(Vector2 targetPos)
    {
        Vector2 direction = targetPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FireBullets()
    {
        float baseAngle = gunTransform.eulerAngles.z;

        FireBulletAtAngle(baseAngle);                     // ����
        FireBulletAtAngle(baseAngle + angleOffset);       // ���� 30��
        FireBulletAtAngle(baseAngle - angleOffset);       // �Ʒ��� 30��
    }

    void FireBulletAtAngle(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        // ȸ�� ���� ��� (�Ѿ��� ���� �ٶ󺸰� ���� ��)
        float bulletAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, bulletAngle);

        GameObject bullet = Instantiate(bulletPrefab, shootTransform.position, rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
