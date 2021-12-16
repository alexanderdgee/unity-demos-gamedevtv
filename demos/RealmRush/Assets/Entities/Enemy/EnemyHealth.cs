using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 2;
    [Tooltip("Adds to maxHitPoints when enemy dies.")]
    [SerializeField] float difficultyRamp = 2f;
    [Tooltip("Adds to difficultyRamp when difficulty increases.")]
    [SerializeField] float difficultyRampIncrease = 0.5f;

    Enemy enemy;
    int currentHitPoints = 0;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        enemy?.Kill();
        maxHitPoints = Mathf.FloorToInt(maxHitPoints + difficultyRamp);
        difficultyRamp += difficultyRampIncrease;
        gameObject.SetActive(false);
    }
}
