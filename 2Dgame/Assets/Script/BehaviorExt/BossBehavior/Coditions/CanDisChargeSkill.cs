using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDisChargeSkill : Conditional
{
    public SharedInt prepareSkill;
    public int normalAttack;
    public override TaskStatus OnUpdate()
    {
        if (prepareSkill.Value == normalAttack)
            return TaskStatus.Success;
        else 
            return TaskStatus.Failure;
    }
}
