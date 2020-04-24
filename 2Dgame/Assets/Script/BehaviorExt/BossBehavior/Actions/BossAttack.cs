using ARPGDemo.Character;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : Action
{
    public SharedInt skillPrepare;
    public Animator anim;
    public PlayerStatus playerStatus;
    public override void OnStart()
    {
        playerStatus = GetComponent<PlayerStatus>();
        anim = GetComponent<Animator>();
        anim.SetBool("isAttack", true);
        if(!(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
            skillPrepare.Value++;
    }
    public override TaskStatus OnUpdate()
    {   
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime>= 1.0f))
        {
            anim.SetBool("isAttack", false); 
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
    //public void AttackAnimComplete()
    //{
    //    anim.SetBool("isAttack", false);
    //}
}
