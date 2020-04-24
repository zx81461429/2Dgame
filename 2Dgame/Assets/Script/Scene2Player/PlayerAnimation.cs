using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    PlayerAttack attack;
    PlayerMovement movement;
    Rigidbody2D rb;

    void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
        attack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(movement.xVelocity));
        anim.SetBool("isOnGround", movement.isOnGround);
        //anim.SetBool("isCrouching", movement.isCrouch);
        anim.SetFloat("verticalVelocity", Mathf.Abs(rb.velocity.y));
        anim.SetBool("isAttacking", attack.isAttack);
    }
    //走路音效播放
    public void StepAudio()
    {
        AudioManager.PlayFootstepAudio();
    }
}
