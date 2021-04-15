using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] float deathForce = 10f;
    [SerializeField] float deathForceMinAngle = 90f;
    [SerializeField] float deathForceMaxAngle = 180f;

    bool isAlive = true;
    bool isClimbing = false;

    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Rigidbody2D rBody;
    Animator animator;
    float gravityScale;

    void Start()
    {
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
        gravityScale = rBody.gravityScale;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            Climb();
            Jump();
            SetAnim();
            Die();
        }
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        rBody.velocity = new Vector2(controlThrow * runSpeed, rBody.velocity.y);
        SetFacing();
    }

    private void SetFacing()
    {
        if (HasHorizontalSpeed()
            && Mathf.Sign(rBody.velocity.x) != Mathf.Sign(transform.localScale.x))
        {
            transform.localScale = new Vector2(-1, 1) * transform.localScale;
        }
    }

    private bool HasHorizontalSpeed()
    {
        return Mathf.Abs(rBody.velocity.x) > Mathf.Epsilon;
    }

    private void SetAnim()
    {
        if (HasHorizontalSpeed())
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
        if (CanClimb() && HasVerticalSpeed())
        {
            animator.SetBool("climb", true);
        }
        else
        {
            animator.SetBool("climb", false);
        }
    }

    private void Climb()
    {
        if (CanClimb())
        {
            float controlThrow = Input.GetAxis("Vertical");
            if (isClimbing || Mathf.Abs(controlThrow) > Mathf.Epsilon)
            {
                isClimbing = true;
                rBody.gravityScale = 0;
                rBody.velocity = new Vector2(rBody.velocity.x, controlThrow * climbSpeed);
            }
        }
        else
        {
            isClimbing = false;
            rBody.gravityScale = gravityScale;
        }
    }

    private bool CanClimb()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("ladders"));
    }

    private bool HasVerticalSpeed()
    {
        return Mathf.Abs(rBody.velocity.y) > Mathf.Epsilon;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump")
            && feetCollider.IsTouchingLayers(LayerMask.GetMask("foreground")))
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
        }
    }

    private void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("enemy", "hazards")))
        {
            isAlive = false;
            animator.SetTrigger("die");
            var projectionForce = Quaternion.Euler(0, 0, UnityEngine.Random.Range(deathForceMinAngle, deathForceMaxAngle))
                        * new Vector3(Mathf.Sign(transform.localScale.x), 0, 0) * deathForce;
            rBody.AddForce(projectionForce);
            FindObjectOfType<GameSession>()?.HandlePlayerDeath();
        }
    }
}
