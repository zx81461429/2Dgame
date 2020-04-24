using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : Action
{
    public Transform[] patrolPoint = new Transform[2];
    //距离目标点最小距离
    public float minDistance = 0.1f;
    public float moveSpeed = 2;
    int index = 0;
    public override TaskStatus OnUpdate()
    {
        CheckFlip();
        if (patrolPoint[0] == null || patrolPoint[1] == null) return TaskStatus.Failure;

        if (Mathf.Abs(transform.position.x - patrolPoint[index].position.x) >= minDistance)
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(patrolPoint[index].position.x, transform.position.y), moveSpeed * Time.deltaTime);
        else index++;
        if (index == 2)
            index = 0;
        return TaskStatus.Running;
    }

    public void CheckFlip()
    {
        if (index == 0)
            transform.localScale = new Vector2(1, transform.localScale.y);
        else if (index == 1)
            transform.localScale = new Vector2(-1, transform.localScale.y);
    }
}
