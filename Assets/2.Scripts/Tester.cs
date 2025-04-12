using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    bool isClimb = false;
    bool collide = false;
    int layer = 1 << 9;
    Rigidbody2D rb;
    [SerializeField] float speed = 3f;
    [SerializeField] float climbTime = 1f;
    [SerializeField] Transform tf;
    [SerializeField] HitEffect hitEffect;

    private void Awake()
    {
        hitEffect.Init(null);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            hitEffect.DoHitEffect();

        if (collide) return;

        if (isClimb == false)
        {
            Vector3 nextPos = transform.position + Time.deltaTime * speed * Vector3.left;
            rb.MovePosition(nextPos);
        }
        RaycastHit2D hit = Physics2D.Raycast(tf.position, Vector2.left, 0.1f, layer);
        if (hit.collider != null && isClimb == false)
        {
            StartCoroutine(moveTo(hit));
        }
    }

    IEnumerator moveTo(RaycastHit2D hit)
    {
        isClimb = true;
        float time = 0;
        Vector3 stPos = transform.position;
        Vector3 edPos = hit.collider.transform.position + Vector3.up + Vector3.left * 0.1f;
        while (time<climbTime)
        {
            transform.position = Vector3.Slerp(stPos, edPos, time / climbTime);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        Vector3 upPos = transform.position;
        Vector3 backPos = hit.collider.transform.position;
        float switchTime = climbTime / 2;
        while (time < switchTime)
        {
            transform.position = Vector3.Slerp(upPos, backPos, time / switchTime);
            hit.collider.transform.position = Vector3.Slerp(backPos, stPos, time / switchTime);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = backPos;
        hit.collider.transform.position = stPos + Vector3.left * 0.1f;
        isClimb = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(collide==false)
                collide = true;
        }
    }
}
