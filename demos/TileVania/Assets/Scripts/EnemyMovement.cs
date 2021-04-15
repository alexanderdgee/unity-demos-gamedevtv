using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var facing = Mathf.Sign(transform.localScale.x);
        rBody.velocity = new Vector2(facing * moveSpeed, rBody.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = transform.localScale * new Vector2(-1f, 1f);
    }
}
