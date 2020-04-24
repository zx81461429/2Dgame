using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanFindPlayer : Conditional
{
    /// <summary>
    /// 与主角的距离
    /// </summary>
    public float fromDistance = 1;
    private Transform CompareTS;
    public override void OnStart()
    {
        //找到主角的Transform信息
        CompareTS = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override TaskStatus OnUpdate()
    {
        if (Mathf.Abs(CompareTS.position.x - transform.position.x) < fromDistance)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
