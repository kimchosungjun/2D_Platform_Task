using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : BaseMonster
{
    Vector2 jumpDirection;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool isGround = false;
    [SerializeField] bool isFrontWall = false;
    [SerializeField] bool isHead = false;

    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask groundLayer;

    #region Unity Life Cycle

    [Header("Limit")]
    [SerializeField] float limitXSpeed = 3f;
    [SerializeField] float limitYSpeed = 3f;

    [Header("Transforms")]
    [SerializeField] Transform headTf;
    [SerializeField] Transform groundTf;
    [SerializeField] Transform backTf;
    [Header("Detect")]
    [SerializeField] protected Transform detectTransform;

    protected int headLayer = 8;
    protected int wall = 7; 
    protected bool coolDownMove = false;
    protected float coolDownTime = 0.5f;


    RaycastHit2D hit;
    RaycastHit2D wallHit;
    RaycastHit2D groundHit;
    RaycastHit2D headHit;
    RaycastHit2D backHit;

    bool isBack = false;

    Collider2D coll2D = null;

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        jumpDirection = new Vector2(-0.2f, 10f);
    }

    void Start()
    {
        Setup();
        monsterLayer = 1 << 8;
    }



    void Detect()
    {
        coll2D = Physics2D.OverlapBox(headTf.position, new Vector2(0.4f, 0.1f), 360f, monsterLayer);
        if (coll2D != null && coolDownMove == false)
        {
            StartCoroutine(CCoolDownMove());
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            isHead = true;
            rigid.AddForce(Vector2.right * 2.5f, ForceMode2D.Impulse);
        }
        else
            isHead = false;
    }

    void FixedUpdate()
    {
        Detect();

        //if (coolDownMove) return;
        if (isHead || coolDownMove) return;

        // Ground
        groundHit = Physics2D.Raycast(groundTf.position, Vector2.down, 1f, groundLayer);
        if (groundHit.collider != null)
            isGround = true;
        else
            isGround = false;

        // Wall
        wallHit = Physics2D.Raycast(detectTransform.position, Vector2.left, 0.2f, wallLayer);
        if (wallHit.collider != null)
            isFrontWall = true;
        else
            isFrontWall = false;

        // Back
        backHit = Physics2D.Raycast(backTf.position, Vector2.right, 0.2f, monsterLayer);
        if (backHit.collider != null)
            isBack = true;
        else
            isBack = false;

        // Jump
        hit = Physics2D.Raycast(detectTransform.position, Vector2.left, 0.1f, monsterLayer);
        if (hit.collider != null && !isFrontWall && isGround && !isBack)
        {
            rigid.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            //hit.collider.GetComponent<Rigidbody2D>()?.AddForce(Vector2.down, ForceMode2D.Impulse);
        }

        // Move
        if (!isFrontWall && hit.collider == null && !coolDownMove && isGround)
        {
            rigid.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
        }
        LimitSpeed();
    }

    void LimitSpeed()
    {
        float xSpeed = rigid.velocity.x;
        float ySpeed = rigid.velocity.y;
        if (rigid.velocity.y > limitYSpeed)
            ySpeed = limitYSpeed;
        if (rigid.velocity.x < -limitXSpeed)
            xSpeed = -limitXSpeed;
        rigid.velocity = new Vector2(xSpeed, ySpeed);
    }
    #endregion

    IEnumerator CCoolDownMove()
    {
        float time = 0f;
        coolDownMove = true;
        while (time < coolDownTime)
        {
            time += Time.deltaTime;
            //rb.velocity = new Vector2(0, rb.velocity.y);
            yield return null;
        }
        coolDownMove = false;
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }
}
