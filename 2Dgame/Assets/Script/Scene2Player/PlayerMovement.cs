using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region 人物移动各种参数
    //人物组件
    [Header("人物组件")]
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("移动参数")]
    [Tooltip("移动速度")]
    public float speed = 8f;              //移动速度
    [Tooltip("下蹲移动时除以的倍数")]
    public float crouchSpeedDivisor = 3f; //下蹲移动时除以的倍数

    [Header("跳跃参数")]
    [Tooltip("跳跃的力")]
    public float jumpForce = 6.3f;        //跳跃的力
    [Tooltip("长按跳跃额外力的加成")]
    public float jumpHoldForce = 1.9f;    //长按跳跃额外力的加成
    [Tooltip("允许长按跳跃时间")]
    public float jumpHoldDuration = 0.1f; //允许长按跳跃时间
    [Tooltip("下蹲状态下额外力的加成")]
    public float crouchJumpBoost = 2.5f;  //下蹲状态下额外力的加成
    [Tooltip("跳跃时间")]
    public float jumpDuration = 0.2f;     //跳跃时间

    float jumpHoldTime;                    //长按跳跃结束时间
    float jumpStopTime;                    //跳跃结束时间

    [Header("状态参数")]
    [Tooltip("下蹲状态")]
    public bool isCrouch;                 //下蹲状态
    [Tooltip("地面状态")]
    public bool isOnGround;               //地面状态
    [Tooltip("长按跳跃状态")]
    public bool isJump;                   //跳跃状态
    [Tooltip("攻击状态")]
    public bool isAttack;                 //攻击状态

    [Header("环境检测")]
    [Tooltip("环境检测的参照物")]
    public Transform referenceTS;
    [Tooltip("地面layer")]
    public LayerMask groundLayer;         //地面layer
    [Tooltip("脚下与人物位置的偏移x轴")]
    public float footOffset = 0.4f;       //脚下与人物位置的偏移x轴
    [Tooltip("脚下与人物位置的偏移y轴")]
    public float footOffsety = 1f;        //脚下与人物位置的偏移y轴
    [Tooltip("射线检测的距离")]
    public float groundDistance = 0.35f;  //射线检测的距离
    [Tooltip("用于检测碰撞体y轴  不同的角色 位置中心不一样 需要调整碰撞体 缩减后的值")]
    public float colloffSet = 0.6f;        //用于检测碰撞体y轴  不同的角色 位置中心不一样 需要调整碰撞体 缩减后的值

    //动作按键是否按下
    bool jumpPressed;  //单次跳跃
    bool jumpHeld;     //长按跳跃
    bool crouchHeld;   //长按下蹲

    //碰撞体参数
    Vector2 collStandoff;     //站立时的碰撞体偏移
    Vector2 collStandsize;    //站立时碰撞体大小
    Vector2 collCrouchoff;    //下蹲时碰撞体偏移
    Vector2 collCrouchsize;   //下蹲时碰撞体大小

    public float xVelocity;   //垂直轴的输入 0-1

    #endregion

    #region 初始化参数Start
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        collStandoff = coll.offset;
        collStandsize = coll.size;
        collCrouchsize = new Vector2(coll.size.x, coll.size.y / 2);
        collCrouchoff = new Vector2(coll.offset.x, coll.offset.y / 2 - colloffSet);
    }
    #endregion

    #region 帧检测各种行为动作
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
            jumpPressed = true;
        if (Input.GetButton("Jump") && !isJump)
            jumpHeld = true;
        //crouchHeld = Input.GetButton("Crouch");
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovement();
        //跳跃
        MidMovement();
        jumpPressed = false;//在fixupdate里进行 bool值改变
    }
    #endregion

    #region 射线检测 地面
    //射线检测
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        //Vector2 pos = transform.position;
        Vector2 pos = referenceTS.position;

        RaycastHit2D ray = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Debug.DrawRay(pos + offset, rayDiraction * length,Color.blue);

        return ray;
    }
    //环境判断
    void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, footOffsety), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, footOffsety), Vector2.down, groundDistance, groundLayer);
        if (leftCheck || rightCheck)
            isOnGround = true;
        else
            isOnGround = false;
    }
    #endregion

    #region 行为动作
    //角色移动
    void GroundMovement()
    {
        //下蹲
        if (crouchHeld && !isCrouch && isOnGround)  //在地面上 长按下蹲 不在下蹲状态 
            Crouch();
        else if (!crouchHeld && isCrouch)           //没按下蹲 在下蹲状态 起立
            StandUp();
        else if (!isOnGround && !isCrouch)          //在空中  不是下蹲的状态 也要起立
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
            transform.localScale = new Vector3(1, 1, 1);
        if (xVelocity > 0)
            transform.localScale = new Vector3(-1, 1, 1);
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
        if (isOnGround && jumpPressed && !isJump && jumpStopTime < Time.time)
        {
            //蹲下跳跃增加高度
            if (isCrouch)
            {
                StandUp();
                rb.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }
            //isOnGround = false;
            isJump = true;

            jumpHoldTime = Time.time + jumpHoldDuration; //跳跃开始时间 = 当前现实时间+跳跃时间
            jumpStopTime = Time.time + jumpDuration;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);//添加力  第二个参数表示突然产生的力
            Debug.Log("长按跳跃");

            jumpPressed = false;//在fixupdate里进行 bool值改变
            //AudioManager.PlayJumpAudio();
        }
        //长按跳跃增加高度
        else if (isJump)
        {
            if (jumpHeld)
            { 
                rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
                //Debug.Log("长按跳跃");
            }
                
            if (jumpHoldTime < Time.time)//当跳跃开始时间 小于现实时间时 结束跳跃
                isJump = false;
        }
    }
    #endregion
}
