using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 10f;
    [SerializeField] int score = 10;
    [SerializeField] float defaultSpeed = 3f;

    [Header("Enemy Stats")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosion;
    float shotCounter;
    [SerializeField] float shotMinDelay = 1f;
    [SerializeField] float shotMaxDelay = 3f;
    
    [Header("SFX")]
    [SerializeField] AudioClip deathCry;
    [Range(0, 0.8f)] [SerializeField] float deathCryVolume = 0.5f;
    [SerializeField] AudioClip laserEmit;
    [Range(0, 0.8f)] [SerializeField] float laserEmitVolume = 0.2f;

    void Start()
    {
        ResetShotCounter();
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(shotMinDelay, shotMaxDelay);
    }

    void Update()
    {
        CountDownToShot();
    }

    private void CountDownToShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter < 0)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void Fire()
    {
        Instantiate(projectile, this.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(laserEmit,
            Camera.main.transform.position, laserEmitVolume);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer == null)
        {
            return;
        }
        ProcessHit(damageDealer);
        damageDealer.Hit();
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(score);
        Instantiate(explosion, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathCry,
            Camera.main.transform.position, deathCryVolume);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FindObjectOfType<SpawnManager>()?.HandleEnemyDeath();
    }

    public float GetDefaultSpeed()
    {
        return defaultSpeed;
    }
}
