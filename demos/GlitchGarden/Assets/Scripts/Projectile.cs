using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float damage = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var entityWithHealth = collision.GetComponent<Health>();
        var attacker = collision.GetComponent<Attacker>();
        if (attacker && entityWithHealth)
        {
            entityWithHealth.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}
