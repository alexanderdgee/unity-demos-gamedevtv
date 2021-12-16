using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] Transform weapon;

    [SerializeField] float range = 15f;

    Enemy target;

    void Start()
    {
        Attack(false);
    }

    void Update()
    {
        if (target == null)
        {
            FindClosestTarget();
        }
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closest = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < maxDistance)
            {
                maxDistance = distance;
                closest = enemy;
            }
        }
        target = closest ? closest : null;
    }

    private bool TargetIsInRange()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= range;
    }

    private void AimWeapon()
    {
        if (target == null || !target.isActiveAndEnabled || !TargetIsInRange())
        {
            Attack(false);
            target = null;
            return;
        }
        Attack(true);
        weapon.LookAt(target.transform);
    }

    private void Attack(bool active)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = active;
    }
}
