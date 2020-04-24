using ARPGDemo.Character;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BatAttack : Action
{
    public GameObject player;
    private PlayerStatus playerStatus;
    private Animator anim;
    public override void OnStart()
    {
        playerStatus = GetComponent<PlayerStatus>();
        anim = GetComponent<Animator>();
        anim.SetBool("isAttack", true);
    }
    public void AttackAnimComplete()
    {
        anim.SetBool("isAttack", false);
    }
}
