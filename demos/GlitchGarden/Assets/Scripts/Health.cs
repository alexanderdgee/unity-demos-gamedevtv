using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 10f;
    [SerializeField] GameObject deathVfxTemplate;

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        if (deathVfxTemplate)
        {
            var deathVfx = Instantiate(
                deathVfxTemplate, transform.position, Quaternion.identity);
            Destroy(deathVfx, 2f);
        }
        Destroy(gameObject);
    }
}
