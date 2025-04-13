using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilEnums;

public class Zombie : BaseMonster
{
    #region Variable : Detect
    // RaycastHit
    RaycastHit2D frontHit; // Detect Same Line Monster
    RaycastHit2D backHit; // Detect Same Line Monster
    RaycastHit2D aboveHit; // Detect Above Same Line Monster
    RaycastHit2D groundHit; // Detect Ground & Same Line Monster
    RaycastHit2D heroHit; // Detect Player

    // Transform
    [Header("0 : Front, 1 : Above, 2 : Ground, 3 : Back")]
    [SerializeField] Transform[] detectTransfroms;

    enum DetectEnum
    {
        Front = 0,
        Above =1,
        Ground =2,
        Back =3,
    }

    bool isKnockBack = false;
    bool isGround = false;
    bool isFrontHero = false;
    bool isAbove = false;
    bool isBack = false;

    int heroLayer = 0;
    int groundLayer = 0;
    #endregion

    #region Variable : Movement
    [Header("Limit Speed")]
    [SerializeField] float limitXSpeed = 3f;
    [SerializeField] float limitYSpeed = 3f;

    [Header("Knock Back")]
    [SerializeField] float knockBackForce = 2.5f;
    [SerializeField] float knockBackTime = 1f;
    float moveSpeed = 0;
    float jumpForce = 0;
    Vector2 jumpDirection;
    #endregion

    #region Unity Life Cycle : Awake, Starat (Setting)

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
        moveSpeed = statController.GetMonsterData().monsterSpeed;
        jumpForce = statController.GetMonsterData().monsterJumpForce;
        heroLayer = 1 << (int)LayerEnums.Hero | 1 << (int)LayerEnums.HeroBox;
    }

    public override void Pooling(LayerEnums _layerEnums)
    {
        base.Pooling(_layerEnums);
        groundLayer = monsterLayer;
    }

    #endregion

    # region Unity Life Cycle : FixedUpdate (Operate AI)

    void FixedUpdate()
    {
        DetectAbove();

        if (isAbove || isKnockBack) return;

        DetectGround();
        DetectFront();
        DetectBack();
        Jump();
        HorizontalMove();
        LimitSpeed();
    }

    // Check Detect Layer
    #region Relate Detect
    void DetectAbove()
    {
        Collider2D coll2D = Physics2D.OverlapBox(detectTransfroms[1].position, new Vector2(0.4f, 0.1f), 360f, monsterLayer);
        if (coll2D != null && isKnockBack == false)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            StartCoroutine(CKnockBack());
            isAbove = true;
        }
        else
            isAbove = false;
    }

    IEnumerator CKnockBack()
    {
        isKnockBack = true;
        rigid.AddForce(Vector2.right * knockBackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackTime);
        isKnockBack = false;
    }

    void DetectGround()
    {
        groundHit = Physics2D.Raycast(detectTransfroms[2].position, Vector2.down, 1f, groundLayer);
        if (groundHit.collider != null)
            isGround = true;
        else
            isGround = false;
    }

    void DetectFront()
    {
        heroHit = Physics2D.Raycast(detectTransfroms[0].position, Vector2.left, 0.2f, heroLayer);
        if (heroHit.collider != null)
            isFrontHero = true;
        else
            isFrontHero = false;
    }

    void DetectBack()
    {
        backHit = Physics2D.Raycast(detectTransfroms[3].position, Vector2.right, 0.2f, monsterLayer);
        if (backHit.collider != null)
            isBack = true;
        else
            isBack = false;
    }
    #endregion

    // Control Movement
    #region Movement
    void Jump()
    {
        frontHit = Physics2D.Raycast(detectTransfroms[0].position, Vector2.left, 0.1f, monsterLayer);
        if (frontHit.collider != null && !isFrontHero && isGround && !isBack)
            rigid.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
    }

    void HorizontalMove()
    {
        if (!isFrontHero && frontHit.collider == null && !isKnockBack && isGround)
            rigid.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
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
    
    #endregion
}
