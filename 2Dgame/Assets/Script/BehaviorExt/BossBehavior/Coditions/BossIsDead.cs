using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIsDead : Conditional
{
    EnemyHealth enemyHealth;
    public override void OnStart()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    public override TaskStatus OnUpdate()
    {
        if (enemyHealth.EnemyBlood <= 0)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
