using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //武器攻击的碰撞体
    public GameObject coll;
    public PlaerMovement player;

    [Header("攻击参数")]

    bool attackPressed;//单次攻击 J键

    public bool isAttack;

    public float attackTimeDuration = 0.15f;      //攻击时间

    float AttackTime;                     //攻击开始时间
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
        if (attackPressed && player.isOnGround && player.xVelocity == 0)
        {
            Debug.Log("11"); 
            isAttack = true;
            AttackTime = Time.time + attackTimeDuration;
        }
        if (AttackTime <= Time.time)
        {
            isAttack = false;
        }
    }
}
