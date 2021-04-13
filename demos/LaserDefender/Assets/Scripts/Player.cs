using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] AudioClip deathCry;
    [Range(0, 0.8f)] [SerializeField] float deathCryVolume = 0.5f;
    [SerializeField] float maxVelocity = 10f;
    [SerializeField] Vector3 minPos;
    [SerializeField] Vector3 maxPos;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip laserEmit;
    [Range(0, 0.8f)] [SerializeField] float laserEmitVolume = 0.2f;
    [Range(0.01f, 1f)] [SerializeField] float firingDelay = 0.2f;
    [SerializeField] float health = 100f;

    Coroutine firingCoroutine = null;

    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        CreateBoundaries();
    }

    void CreateBoundaries()
    {
        var gameCamera = Camera.main;
        minPos = gameCamera.ViewportToWorldPoint(new Vector3(0, 0));
        maxPos = gameCamera.ViewportToWorldPoint(new Vector3(1, 1));
        var spriteRect = GetComponent<SpriteRenderer>()?.sprite?.bounds.size;
        var halfWidth = spriteRect?.x / 2 ?? 0;
        var halfHeight = spriteRect?.y / 2 ?? 0;
        minPos.x += halfWidth;
        minPos.y += halfHeight;
        maxPos.x -= halfWidth;
        maxPos.y -= halfHeight;
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        offset.z = 0;
        var scale = Time.deltaTime * maxVelocity;
        var delta = offset.magnitude < scale
            ? offset : scale * offset.normalized;
        var target = transform.position + delta;
        Clamp(ref target, minPos, maxPos);
        transform.position = target;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(SpawnLaserShot());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator SpawnLaserShot()
    {
        while (true)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(laserEmit,
                Camera.main.transform.position, laserEmitVolume);
            yield return new WaitForSeconds(firingDelay);
        }
    }

    private static void Clamp(ref Vector3 target, Vector3 min, Vector3 max)
    {
        if (target.x < min.x)
        {
            target.x = min.x;
        }
        else if (target.x > max.x)
        {
            target.x = max.x;
        }
        if (target.y < min.y)
        {
            target.y = min.y;
        }
        else if (target.y > max.y)
        {
            target.y = max.y;
        }
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
        AudioSource.PlayClipAtPoint(deathCry,
            Camera.main.transform.position, deathCryVolume);
        Destroy(gameObject);
        FindObjectOfType<Level>().LoadGameOver();
    }

    public float GetHealth()
    {
        return health;
    }
}
