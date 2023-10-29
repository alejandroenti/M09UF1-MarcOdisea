using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    
    private Movement movementScript;
    private Jump jumpScript;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        movementScript = GetComponent<Movement>();
        jumpScript = GetComponent<Jump>();
    }

    private void Update()
    {
        animator.SetBool("IsGrounded", controller.isGrounded);

        animator.SetFloat("HorizontalVelocity", movementScript.GetVelocityXZ());
        animator.SetFloat("VerticalVelocity", jumpScript.GetYVelocity());

        if (jumpScript.GetIsJumping())
        {
            animator.SetTrigger("IsJumping");
        }

        animator.SetInteger("Jump", jumpScript.GetJumpCount());
    }
}
