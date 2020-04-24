using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //武器攻击的碰撞体
    public GameObject coll;
    public PlayerMovement player;

    [Header("攻击参数")]

    bool attackPressed;//单次攻击 J键

    public bool isAttack;

    public float attackTimeDuration = 0.3f;      //攻击时间

    private float AttackStopTime = 0;                     //攻击结束时间=攻击开始时间+攻击时间
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackPressed = Input.GetKeyDown(KeyCode.J);
        Attack();
    }

    //角色攻击
    void Attack()
    {
        if (attackPressed && AttackStopTime < Time.time && !isAttack && player.xVelocity == 0 && player.isOnGround)
        {
            isAttack = true;
            AttackStopTime = Time.time + attackTimeDuration;
        }
        if (AttackStopTime <= Time.time)
        {
            isAttack = false;
        }
    }
}
