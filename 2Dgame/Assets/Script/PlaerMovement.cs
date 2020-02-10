using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("移动参数")]
    public float speed = 8f;
    public float crouchSpeedDivisor = 3f; //下蹲移动时除以的倍数

    [Header("跳跃参数")]
    public float jumpForce = 6.3f; //跳跃的力
    public float jumpHoldForce = 1.9f; //长按跳跃额外力的加成
    public float jumpHoldDuration = 0.1f; //跳跃时间
    public float crouchJumpBoost = 2.5f; //下蹲状态下额外力的加成

    float jumpTime; //跳跃开始时间

    [Header("状态参数")]//准备修改为状态机
    public bool isCrouch;
    public bool isOnGround;
    public bool isJump;

    [Header("环境检测")]
    public LayerMask groundLayer; //地面layer
    public float footOffset = 0.4f;//脚下与人物位置的偏移
    public float headClearance = 0.5f;//头部与人物位置的偏移
    public float groundDistance = 0.2f;//检测距离

    //动作按键是否按下
    bool jumpPressed; //单次跳跃
    bool jumpHeld;    //长按跳跃
    bool crouchHeld;  //长按下蹲

    //碰撞体参数
    Vector2 collStandoff;
    Vector2 collStandsize;
    Vector2 collCrouchoff;
    Vector2 collCrouchsize;

    float xVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        collStandoff = coll.offset;
        collStandsize = coll.size;
        collCrouchsize = new Vector2(coll.size.x, coll.size.y / 2);
        collCrouchoff = new Vector2(coll.offset.x, coll.offset.y / 2);
    }

    void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovement();
        //跳跃
        MidMovement();
    }
    //射线检测重载
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;

        RaycastHit2D ray = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Debug.DrawRay(pos + offset, rayDiraction * length);

        return ray;
    }
    //环境判断
    void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset,0.05f),Vector2.down,groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0.05f), Vector2.down, groundDistance, groundLayer);
        if (leftCheck)
            isOnGround = true;
        else
            isOnGround = false;
    }
    //角色移动
    void GroundMovement()
    {
        //下蹲
        if (crouchHeld && !isCrouch && isOnGround)
            Crouch();
        else if (!crouchHeld && isCrouch)
            StandUp();
        else if (!isOnGround && !isCrouch)  //在空中  不是下蹲的状态 也要起立
            StandUp();
        //朝向
        FlipDirction();
        //移动
        xVelocity = Input.GetAxis("Horizontal");
        if (isCrouch)
            xVelocity /= crouchSpeedDivisor;
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
    }
    //角色朝向
    void FlipDirction()
    {
        if (xVelocity < 0)
            transform.localScale = new Vector2(-1, 1);
        if (xVelocity > 0)
            transform.localScale = new Vector2(1, 1);
    }
    //角色下蹲
    void Crouch()
    {
        isCrouch = true;

        coll.offset = collCrouchoff;
        coll.size = collCrouchsize;
    }
    //角色站立
    void StandUp()
    {
        isCrouch = false;

        coll.offset = collStandoff;
        coll.size = collStandsize;
    }
    //角色跳跃
    void MidMovement()
    {
        //按下跳跃
        if (isOnGround && jumpPressed && !isJump)
        {
            //蹲下跳跃增加高度
            if (isCrouch && isOnGround)
            {
                StandUp();
                rb.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }
            isOnGround = false;
            isJump = true;

            jumpTime = Time.time + jumpHoldDuration;

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // 添加力  第二个参数表示突然产生的力
        }
        //长按跳跃增加高度
        else if (isJump)
        {
            if (jumpHeld)
                rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            if (jumpTime < Time.time)
                isJump = false;
        }
    }

}
