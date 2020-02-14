using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    PlaerMovement movement;
    Rigidbody2D rb;

    void Start()
    {
        movement = GetComponentInParent<PlaerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(movement.xVelocity));
        anim.SetBool("isOnGround", movement.isOnGround);
        anim.SetBool("isCrouching", movement.isCrouch);
        anim.SetFloat("verticalVelocity", rb.velocity.y);
    }
    //走路音效播放
    public void StepAudio()
    {
        AudioManager.PlayFootstepAudio();
    }
}
