using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakingStatus : Action
{
    public Animator anim;
    public override TaskStatus OnUpdate()
    {
        //判断动作的标准化时间 0 - 1 是否超过1 超过1时刻表示完成动画
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("wake"))
            return TaskStatus.Success;
        return TaskStatus.Running;
    }
}
