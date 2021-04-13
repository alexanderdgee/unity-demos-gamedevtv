using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Range(0f, 4f)] [SerializeField] float walkSpeed = 1f;
    [SerializeField] float attack = 3f;

    bool canMove = false;
    Animator animator;
    LevelController levelController;
    GameObject currentTarget;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.AttackerSpawned();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.left * Time.deltaTime * walkSpeed);
        }
        UpdateAnimationState();
    }

    private void OnDestroy()
    {
        if (levelController)
        {
            levelController.AttackerKilled();
        }
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void ToggleMovement(int canMove)
    {
        this.canMove = canMove != 0;
    }

    public void Attack(GameObject target)
    {
        animator.SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void StrikeTarget()
    {
        if (!currentTarget)
        {
            return;
        }
        Health hp = currentTarget.GetComponent<Health>();
        if (hp)
        {
            hp.DealDamage(attack);
        }
    }
}
