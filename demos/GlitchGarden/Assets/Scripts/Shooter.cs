using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const string PROJECTILE_PARENT_NAME = "Projectiles";

    [SerializeField] GameObject projectileSpawnPoint;
    [SerializeField] GameObject projectile;

    Animator animator;
    AttackerSpawner laneSpawner;
    GameObject projectileParent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CreateProjectileParent();
        SetLaneSpawner();
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    private void SetLaneSpawner()
    {
        int y = Mathf.RoundToInt(transform.position.y);
        var spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (var spawner in spawners)
        {
            int spawnerY = Mathf.RoundToInt(spawner.transform.position.y);
            if (spawnerY == y)
            {
                laneSpawner = spawner;
                return;
            }
        }
        Debug.LogWarning("No spawner found for shooter with y-coord: " + y.ToString(), this);
    }

    private void Update()
    {
        if (laneSpawner.AttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(this.projectile,
            projectileSpawnPoint.transform.position,
            transform.rotation);
        projectile.transform.parent = projectileParent.transform;
    }
}
