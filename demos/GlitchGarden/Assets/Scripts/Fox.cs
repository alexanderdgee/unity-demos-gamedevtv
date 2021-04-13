using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Animator animator;
    Attacker attacker;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attacker = GetComponent<Attacker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Gravestone>())
        {
            animator.SetTrigger("jumpTrigger");
        }
        else if (collision.GetComponent<Defender>())
        {
            attacker.Attack(collision.gameObject);
        }
    }
}
