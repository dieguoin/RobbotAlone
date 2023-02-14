using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator { get; private set; }
    private PlayerMovement playerMovement;
    private Vector2 scale;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        scale = transform.localScale;
    }
    public void Move(float speed)
    {
        Vector3 myPosition = transform.position;
        if (speed > 0)
        {
            transform.position = new Vector3(0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position -= myPosition;
        }
        if (speed < 0)
        {
            transform.position = new Vector3(0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position += myPosition;
        }
        animator.SetFloat("Speed", speed);
    }
    public void EnableJump(bool jump)
    {
        animator.SetBool("Jump", jump);
    }
    public void SetGrounded(bool grounded)
    {
        animator.SetBool("IsGrounded", grounded);
    }
    public void Jump()
    {
        playerMovement.Jump();
        EnableJump(false);
    }
    public void StopRightAttack()
    {
        animator.SetBool("RightHit", false);
    }
    public void StopLeftAttack()
    {
        animator.SetBool("LeftHit", false);
    }
}
