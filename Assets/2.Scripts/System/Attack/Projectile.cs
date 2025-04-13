using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;

public class Projectile : MonoBehaviour
{
    ProjectileData data = null;
    protected string enemyTag;

    public void SetData(ProjectileData _data, string _enemyTag, Vector3 _direction)
    {
        if (_data == null) return;
        if (this.gameObject.activeSelf == false)
            this.gameObject.SetActive(true);
        data = _data;
        enemyTag = _enemyTag;
        StartCoroutine(CMove(_direction));
    }

    public void SetData(ProjectileData _data, TagEnums _enemyTag, Vector3 _direction)
    {
        if (_data == null) return;
        if (this.gameObject.activeSelf == false)
            this.gameObject.SetActive(true);
        data = _data;
        enemyTag = _enemyTag.ToString();
        StartCoroutine(CMove(_direction));
    }

    IEnumerator CMove(Vector3 _direction)
    {
        float time = 0;
        while (time < data.maintainTime)
        {
            this.transform.position += _direction * Time.deltaTime * data.launchSpeed;
            yield return null;
        }

        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            this.gameObject.SetActive(false);
            collision.GetComponent<StatController>().Hit(data.damage);
        }
    }
}
