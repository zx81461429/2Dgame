using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2 : Action
{
    public SharedInt prepareSkill;
    private Animator anim;
    public override void OnStart()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isAttack", false);
        anim.SetBool("isSkill2DisCharge", true);
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            prepareSkill.Value = 0;
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.SetBool("isSkill2DisCharge", false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
