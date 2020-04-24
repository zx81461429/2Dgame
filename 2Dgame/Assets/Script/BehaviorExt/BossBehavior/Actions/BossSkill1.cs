using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill1 : Action
{
    public SharedInt prepareSkill;
    private Animator anim;
    public override void OnStart()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isAttack", false);
        anim.SetBool("isSkill1DisCharge", true);
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            prepareSkill.Value++;
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack4") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.SetBool("isSkill1DisCharge", false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
