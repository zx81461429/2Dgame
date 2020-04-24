using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSeek : Action
{
    public GameObject player;
    public float moveSpeed = 0.2f;
    public float attackDistance = 4;


    private Animator anim;
    public override void OnStart()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isAttack", false);
    }
    public override TaskStatus OnUpdate()
    {
        CheckFlip();
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x,transform.position.y), moveSpeed * Time.deltaTime);
        
        //if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
        //    return TaskStatus.Success;
        return TaskStatus.Running;
    }

    public void CheckFlip()
    {
        if ((transform.position.x - player.transform.position.x) < 0)
            transform.localScale = new Vector2(1, transform.localScale.y);
        else
            transform.localScale = new Vector2(-1, transform.localScale.y);
    }
}
